using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    IEnumerator Start()
    {
        LoadGame();
        yield return null;
    }

    public void PlayGame()
    {
        Application.LoadLevel("Gameplay2");

    }

    public void BacktoScreen()
    {
        SceneManager.LoadScene("Main Menu");
        Destroy(GameObject.Find("EssentialObjects"));
    }

    public void LoadGame()
    {
        
        SavingSystem.i.Load("saveSlot1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
