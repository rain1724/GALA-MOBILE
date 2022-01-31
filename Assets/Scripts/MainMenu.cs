using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
