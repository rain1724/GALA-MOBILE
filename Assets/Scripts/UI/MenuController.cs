using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject menux;
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] GameObject Joystickx;
    [SerializeField] GameObject interactbuttonx;
    [SerializeField] GameObject menuo;

    Color highlightedColor = GlobalSettings.i.HighlightedColor;
   
    public event Action<int> onMenuSelected;
    public event Action onBack;

    List<Button> menuItems;

    int selectedItem = 0;

    private void Awake()
    {
        menuItems = menu.GetComponentsInChildren<Button>().ToList();   
    }

    public void OpenMenu()
    {

        menu.SetActive(true);
        menux.SetActive(true);
        menuo.SetActive(false);
        UpdateItemSelection();

    }
    public void CloseAllMenu()
    {

        menu.SetActive(false);
        menuo.SetActive(true);
        menux.SetActive(false);

    }
    public void CloseMenu()
    {
        
        menu.SetActive(false);
        menuo.SetActive(false);
        menux.SetActive(true);

    }

    public void CloseMenuSaveLoad()
    {
        menu.SetActive(false);
        menuo.SetActive(true);
        menux.SetActive(false);
    }

    public void CloseInventory()
    {
        menuo.SetActive(true);
        menu.SetActive(false);
        menux.SetActive(false);
    }



    public void HandleUpdate()
    {
        int prevSelection = selectedItem;
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
            ++selectedItem;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            --selectedItem;

      

        selectedItem = Mathf.Clamp(selectedItem, 0, menuItems.Count - 1);

        if (prevSelection != selectedItem)
            UpdateItemSelection();
        
        if (CrossPlatformInputManager.GetButtonDown("menu-save"))
        {
            //save the data
            SavingSystem.i.Save("saveSlot1");
            CloseMenuSaveLoad();
            onBack?.Invoke();
            
        }

        if (CrossPlatformInputManager.GetButtonDown("menu-load"))
        {
            //load data obviosuly 
            SavingSystem.i.Load("saveSlot1");
            CloseMenuSaveLoad();
            onBack?.Invoke();
 
        }

        if (CrossPlatformInputManager.GetButtonDown("menu-inventory"))
        {
            onMenuSelected?.Invoke(selectedItem);
            CloseMenu();
            
        }

        if (CrossPlatformInputManager.GetButtonDown("menu-button-close"))
        {
            CloseAllMenu();
            onBack?.Invoke();

        }






    }
    void UpdateItemSelection()
    {
        for (int i = 0; i < menuItems.Count; i++)
        {

        }
    }

}
