using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectionMenu : MonoBehaviour
{
    
    public GameObject[] playerObjects;
    public GameObject DifficultyToggles;
    public int selectedCharacter = 0;

    public InputField playername;

    

    

    private string selectedCharacterDataName = "SelectedCharacter";

    
    void Start()
    {
        HideAllCharacters();

        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterDataName, 0);

        playerObjects[selectedCharacter].SetActive(true);

        DifficultyToggles.transform.GetChild((int)GameControl.Difficulty).GetComponent<Toggle>().isOn = true;
    }


    private void HideAllCharacters()
    {
        foreach (GameObject g in playerObjects)
        {
            g.SetActive(false);
        }
    }

    public void NextCharacter()
    {
        playerObjects[selectedCharacter].SetActive(false);
        selectedCharacter++;
        if (selectedCharacter >= playerObjects.Length)
        {
            selectedCharacter = 0;
        }
        playerObjects[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        playerObjects[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter = playerObjects.Length-1;
        }
        playerObjects[selectedCharacter].SetActive(true);
    }

    public void StartGame()
    {
        switch (GameControl.Difficulty)
        {
            case GameControl.Difficulties.Easy:
                PlayerPrefs.SetInt(selectedCharacterDataName, selectedCharacter);
                SceneManager.LoadScene("Gameplay2");
                PlayerController.playername = playername.text;
                break;

            case GameControl.Difficulties.Medium:
                PlayerPrefs.SetInt(selectedCharacterDataName, selectedCharacter);
                SceneManager.LoadScene("Gameplay3");
                PlayerController.playername = playername.text;
                break;

            case GameControl.Difficulties.Hard:
                PlayerPrefs.SetInt(selectedCharacterDataName, selectedCharacter);
                SceneManager.LoadScene("Gameplay4");
                PlayerController.playername = playername.text;
                break;
        }
        
        
    }

    #region Difficulty
    public void SetEasyDifficulty(bool isOn)
    {
        if (isOn)
            GameControl.Difficulty = GameControl.Difficulties.Easy;
    }
    public void SetMediumDifficulty(bool isOn)
    {
        if (isOn)
            GameControl.Difficulty = GameControl.Difficulties.Medium;
    }
    public void SetHardDifficulty(bool isOn)
    {
        if (isOn)
            GameControl.Difficulty = GameControl.Difficulties.Hard;
    }

    #endregion


}
