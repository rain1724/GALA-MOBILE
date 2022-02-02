using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  
    public void PlayGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void BacktoScreen()
    {
        SceneManager.LoadScene("Main Menu");
        Destroy(GameObject.Find("EssentialObjects"));
        SceneManager.UnloadSceneAsync("Gameplay");
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
