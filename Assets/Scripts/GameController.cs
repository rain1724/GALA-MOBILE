using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public enum GameState {FreeRoam, Paused, Dialog, Menu}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Camera worldCamera;


    GameState state;
    GameState stateBeforePause;

    public static GameController Instance { get; private set; }

    MenuController menuController;

    private void Awake()
    {
        Instance = this;

        menuController = GetComponent<MenuController>();      
    }

    private void Start()
    {   //changing State when dialog is shown to stop player from moving
        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;

        };

        DialogManager.Instance.OnCloseDialog += () =>
        {
            if(state == GameState.Dialog)
            state = GameState.FreeRoam;

        };

        menuController.onBack += () =>
        {
            state = GameState.FreeRoam;
        };
    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();

            if (CrossPlatformInputManager.GetButtonDown("menu-button"))
            {
                menuController.OpenMenu();
                state = GameState.Menu;
            }   
        }

        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }

        else if (state == GameState.Menu)
        {
            menuController.HandleUpdate();
        }
    }


    public void PauseGame(bool pause)
    {
        if (pause)
        {
            stateBeforePause = state;
            state = GameState.Paused;
        }
        else
        {
            state = stateBeforePause;
        }
    }
}
