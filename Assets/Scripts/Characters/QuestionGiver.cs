using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionGiver : MonoBehaviour
{
    public IEnumerator question(Transform player, Dialog dialog)
    {
        int selectedChoice = 0;
        yield return DialogManager.Instance.ShowDialog(dialog,
            new List<string>() { "Yes", "No" },
        (choiceIndex) => selectedChoice = choiceIndex);

        if (selectedChoice == 0)
        {
            //yes
            yield return DialogManager.Instance.ShowDialogText($"Correct");
        }

        else if (selectedChoice == 1)
        {
            //no
            yield return DialogManager.Instance.ShowDialogText($"Incorrect");
        }
    }
}
