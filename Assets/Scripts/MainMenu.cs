using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject PanelHowto;
    [SerializeField] GameObject PanelAbout;
    [SerializeField] GameObject PanelCredits;

    private int sceneToContinue;

    public void PlayGame()
    {
        SceneManager.LoadScene("CharacterSelection");

    }

    public void BacktoScreen()
    {
        SceneManager.LoadScene("Main Menu");
        Destroy(GameObject.Find("EssentialObjects"));
    }

    public void LoadGame()
    {
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");

        if (sceneToContinue != null)
        {
            bl_SceneLoaderManager.LoadScene("saveSlot1");
            
            SceneManager.LoadScene(sceneToContinue);
            SavingSystem.i.Load("saveSlot1");
        }
        else
            return;
        
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void About()
    {
        PanelAbout.SetActive(true);
    }
    public void Credits()
    {
        PanelCredits.SetActive(true);
    }

    public void ClosePanel()
    {
        PanelAbout.SetActive(false);
        PanelCredits.SetActive(false);
    }
}
