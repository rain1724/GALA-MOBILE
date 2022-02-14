using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ChoiceBox : MonoBehaviour
{
    [SerializeField] ChoiceText choiceTextPrefab;

    List<ChoiceText> choiceTexts;
    int currentChoice;

    bool choiceSelected = false;
   public IEnumerator ShowChoices(List<string> choices, Action<int> onchoiceSelected)
    {
        choiceSelected = false;
        currentChoice = 0;
        gameObject.SetActive(true);

        foreach (Transform child in transform)
            Destroy(child.gameObject);

        foreach (var choice in choices)
        {
            var choiceTextObj = Instantiate(choiceTextPrefab, transform);
            choiceTextObj.TextField.text = choice;
            choiceTexts.Add(choiceTextObj);
        }

        yield return new WaitUntil(() => choiceSelected == true);
        onchoiceSelected?.Invoke(currentChoice);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("left"))
            ++currentChoice;
        if (CrossPlatformInputManager.GetButtonDown("right"))
            --currentChoice;

        currentChoice = Mathf.Clamp(currentChoice, 0, choiceTexts.Count - 1);

        for (int i = 0; i < choiceTexts.Count; i++)
        {
            choiceTexts[i].SetSelected(i == currentChoice);
        }
        if (CrossPlatformInputManager.GetButtonDown("interact"))
            choiceSelected = true;

    }
}
