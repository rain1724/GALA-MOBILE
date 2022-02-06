using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  
    public void PlayGame()
    {
        SceneManager.UnloadScene("Main Menu");
    }

    public void BacktoScreen()
    {
        SceneManager.LoadScene("Main Menu");
        Destroy(GameObject.Find("EssentialObjects"));
    }

    public void LoadGame()
    {
        SavingSystem.i.Load("saveSlot1");
        SceneManager.UnloadScene("Main Menu");

        Debug.Log("Clicked");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
