using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;


public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject menuButtonClose;
    [SerializeField] GameObject menuButtonOpen;
    [SerializeField] GameObject Joystickx;
    [SerializeField] GameObject interactbuttonx;
    

    //*Color highlightedColor = GlobalSettings.i.HighlightedColor;
   
    public event Action<int> onMenuSelected;
    public event Action onBack;

    List<Text> menuItems;

    int selectedItem = 0;

    GameState state;
    private void Awake()
    {
        menuItems = menu.GetComponentsInChildren<Text>().ToList();   
    }

    public void OpenMenu()
    {
        menuButtonOpen.SetActive(false);
        menu.SetActive(true);
        menuButtonClose.SetActive(true);
        
        //UpdateItemSelection();

    }
    public void CloseMenu()
    {
        menu.SetActive(false);
        menuButtonClose.SetActive(false);
        menuButtonOpen.SetActive(true);
    }

  


    public void HandleUpdate()
    {

        if (CrossPlatformInputManager.GetButtonDown("menu-pocket"))
        {
           
            inventoryUI.gameObject.SetActive(true);
            menuButtonClose.SetActive(true);
            state = GameState.Bag;
        }


        if (CrossPlatformInputManager.GetButtonDown("menu-save"))
        {
            SavingSystem.i.Save("saveSlot1");
        }


        if (CrossPlatformInputManager.GetButtonDown("menu-load"))
        {
            SavingSystem.i.Load("saveSlot1");
        }

        if (CrossPlatformInputManager.GetButtonDown("menu-quit"))
        {
            SceneManager.LoadScene("Main Menu");
            Destroy(GameObject.Find("EssentialObjects"));
            SceneManager.UnloadSceneAsync("Gameplay");
        }

        if (CrossPlatformInputManager.GetButtonDown("menu-close"))
        {
            inventoryUI.gameObject.SetActive(false);
            onBack?.Invoke();
            CloseMenu();

        }

        /*
        int prevSelection = selectedItem;


        if (Input.GetKeyDown(KeyCode.DownArrow))
            ++selectedItem;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            --selectedItem;

        selectedItem = Mathf.Clamp(selectedItem, 0, menuItems.Count - 1);

        if (prevSelection != selectedItem)
            UpdateItemSelection();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            onMenuSelected?.Invoke(selectedItem);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            onBack?.Invoke();
            CloseMenu();
        }*/



    }
   /* void UpdateItemSelection()
    {
        for (int i = 0; i < menuItems.Count; i++)
        {
            if (i == selectedItem)
                menuItems[i].color = GlobalSettings.i.HighlightedColor;
            else
                menuItems[i].color = Color.black;
        }
    }*/

}
