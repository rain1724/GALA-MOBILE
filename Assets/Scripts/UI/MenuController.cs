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
    [SerializeField] GameObject QuestUI;
    [SerializeField] GameObject MenuAction;
    [SerializeField] GameObject MenuYes;
    [SerializeField] GameObject MenuNo;
    [SerializeField] GameObject PopupSaved;
    [SerializeField] GameObject PopupLoaded;



    private int currentSceneIndex;
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
        UpdateItemSelection();

    }
    public void CloseMenu()
    {
        menu.SetActive(false);
        menuButtonClose.SetActive(false);
        menuButtonOpen.SetActive(true);
        QuestUI.gameObject.SetActive(false);
        PopupSaved.gameObject.SetActive(false);
        PopupLoaded.gameObject.SetActive(false);

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
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
            PopupSaved.gameObject.SetActive(true);
            
            


            //MenuAction.gameObject.SetActive(true);
            //state = GameState.MenuAction;
            //CloseMenu();



        }


        if (CrossPlatformInputManager.GetButtonDown("menu-load"))
        {
            SavingSystem.i.Load("saveSlot1");
            PopupLoaded.gameObject.SetActive(true);

            /*MenuAction.gameObject.SetActive(true);
            if (CrossPlatformInputManager.GetButtonDown("menu-yes"))
            {
                SavingSystem.i.Load("saveSlot1");
                MenuAction.gameObject.SetActive(false);
            }
            else if (CrossPlatformInputManager.GetButtonDown("menu-no"))
            {
                MenuAction.gameObject.SetActive(false);
                state = GameState.Menu;
            }*/

        }

        if (CrossPlatformInputManager.GetButtonDown("menu-quest"))
        {
            QuestUI.gameObject.SetActive(true);
            menuButtonClose.SetActive(true);
            state = GameState.Map;


        }

        if (CrossPlatformInputManager.GetButtonDown("menu-quit"))
        {
            SceneManager.LoadScene("Main Menu");
            Destroy(GameObject.Find("EssentialObjects"));
            
        }

        if (CrossPlatformInputManager.GetButtonDown("menu-close"))
        {
            inventoryUI.gameObject.SetActive(false);
            state = GameState.FreeRoam;
            onBack?.Invoke();
            CloseMenu();

        }

        
        //int prevSelection = selectedItem;


        /*if (CrossPlatformInputManager.GetButtonDown("down"))
            ++selectedItem;
        else if (CrossPlatformInputManager.GetButtonDown("up"))
            --selectedItem;

        selectedItem = Mathf.Clamp(selectedItem, 0, menuItems.Count - 1);

        if (prevSelection != selectedItem)
            UpdateItemSelection();

        /*if (CrossPlatformInputManager.GetButtonDown("interact"))
        {
            onMenuSelected?.Invoke(selectedItem);
        }*/
        else if (Input.GetKeyDown(KeyCode.X))
        {
            onBack?.Invoke();
            CloseMenu();
        }



    }
   void UpdateItemSelection()
    {
        for (int i = 0; i < menuItems.Count; i++)
        {
            if (i == selectedItem)
                menuItems[i].color = GlobalSettings.i.HighlightedColor;
            else
                menuItems[i].color = Color.black;
        }
    }

}
