using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public enum GameState {FreeRoam, Paused, Dialog, Menu, Bag, Map, MenuAction}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] InventoryUI inventoryUI;
   



    GameState state;
    GameState prevState;

    public GameObject DifficultyToggles;

    public SceneDetails CurrentScene { get; private set; }
    public SceneDetails PrevScene { get; private set; }


    public static GameController Instance { get; private set; }

    
    MenuController menuController;

    private void Awake()
    {
        Instance = this;

        menuController = GetComponent<MenuController>();

        ItemDB.Init();
        QuestDB.Init();
        
    }

    private void Start()
    {   //changing State when dialog is shown to stop player from moving
        DialogManager.Instance.OnShowDialog += () =>
        {
            prevState = state;
            state = GameState.Dialog;

        };

        DialogManager.Instance.OnCloseDialog += () =>
        {
            if (state == GameState.Dialog)
                state = prevState;
        };

        menuController.onBack += () =>
        {
            state = GameState.FreeRoam;
        };

      // menuController.onMenuSelected +=  OnMenuSelected;
    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();

            //for menu ofcourse
            /*this is for backup controls in pc
            if (Input.GetKeyDown(KeyCode.Return))
            {
                menuController.OpenMenu();
                state = GameState.Menu;
                menuController.HandleUpdate();
            }*/
            if (CrossPlatformInputManager.GetButtonDown("menu-open"))
            {
                menuController.OpenMenu();
                state = GameState.Menu;
                menuController.HandleUpdate();
               
            }
        }

        //Dialog State
        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }

        else if (state == GameState.Menu)
        {
            menuController.HandleUpdate();

        }
        //inventory UI State
        else if (state == GameState.Bag)
        {
            Action onBack = () =>
            {
                inventoryUI.gameObject.SetActive(false); 
                state = GameState.FreeRoam;
            };

            inventoryUI.HandleUpdate(onBack);
        }

       
    }

    public void SetCurrentScene(SceneDetails currScene)
    {
        PrevScene = CurrentScene;
        CurrentScene = currScene;
    }
    


    public void PauseGame(bool pause)
    {
        if (pause)
        {
            prevState = state;
            state = GameState.Paused;
        }
        else
        {
            state = prevState;
        }
    }

     /*void OnMenuSelected(int selectedItem)
     {


         //inventory
         if (selectedItem ==0)
         {
             inventoryUI.gameObject.SetActive(true);
             state = GameState.Bag;
         }

         //save
         else if (selectedItem == 1)
         {
             SavingSystem.i.Save("saveSlot1");

         }

         //load
         else if (selectedItem == 2)
         {
             SavingSystem.i.Load("saveSlot1");
         }

         //map
         else if (selectedItem == 3)
         {

             Map.gameObject.SetActive(true);
             menuButtonClose.SetActive(true);
             state = GameState.Map;
         }



         //quit game back to screen
         else if (selectedItem == 4)
         {   
             SceneManager.LoadScene("Main Menu");
             Destroy(GameObject.Find("EssentialObjects"));
         }

     }*/


    public GameState State => state;
}


