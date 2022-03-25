using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class CharacterSelectionMenu : MonoBehaviour
{
    
    public GameObject[] playerObjects;
    public GameObject DifficultyToggles;
    public int selectedCharacter = 0;

    public InputField playername;
    public GameObject NameValidation;
    public Text ProfanityValidation;
    public TextAsset textAssetBlockList;
    [SerializeField] string[] strBlockList;

    private string selectedCharacterDataName = "SelectedCharacter";

    void Start()
    {
        HideAllCharacters();
        
        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterDataName, 0);

        playerObjects[selectedCharacter].SetActive(true);

        DifficultyToggles.transform.GetChild((int)GameControl.Difficulty).GetComponent<Toggle>().isOn = true;
        strBlockList = textAssetBlockList.text.Split(new string[] { ",", "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
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
    string ProfanityCheck(string strToCheck)
    {
        for (int i = 0; i < strBlockList.Length; i++)
        {
            string profanity = strBlockList[i];
            System.Text.RegularExpressions.Regex word = new Regex("" + profanity + "");
            if (strBlockList[i].Contains(profanity))
            {
                string temp = word.Replace(strToCheck, "Bad Words/Names not Allowed");
                strToCheck = temp;
            }
        }
        return strToCheck;
    }

    public void Checkinput()
    {
        ProfanityValidation.text = ProfanityCheck(playername.text);
        
        if (ProfanityValidation.text.Contains("Bad Words/Names not Allowed"))
        {
            playername.text = "";
        }

        else
        {
            ProfanityValidation.text = "";
            StartGame();
        }
    }


    public void  StartGame()
    {

        string Playername = playername.text;
        if (!string.IsNullOrWhiteSpace(Playername)) 
        {
            NameValidation.SetActive(false);
            switch (GameControl.Difficulty)
            {

                case GameControl.Difficulties.Easy:
                    PlayerPrefs.SetInt(selectedCharacterDataName, selectedCharacter);
                    PlayerController.playername = playername.text;
                    bl_SceneLoaderManager.LoadScene("Gameplay2");
                    break;

                case GameControl.Difficulties.Medium:
                    PlayerPrefs.SetInt(selectedCharacterDataName, selectedCharacter);
                    PlayerController.playername = playername.text;
                    bl_SceneLoaderManager.LoadScene("Gameplay3");
                    break;

                case GameControl.Difficulties.Hard:
                    PlayerPrefs.SetInt(selectedCharacterDataName, selectedCharacter);
                    PlayerController.playername = playername.text;
                    bl_SceneLoaderManager.LoadScene("Gameplay4");
                    break;
            }
        }
        else
        {
            NameValidation.SetActive(true);
        }


    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");

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
