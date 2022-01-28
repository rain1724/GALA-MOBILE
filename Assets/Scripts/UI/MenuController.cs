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

    public void CloseMenu()
    {
        
        menu.SetActive(false);
        menuo.SetActive(false);
        menux.SetActive(true);


    }

    public void CloseInventory()
    {

        menu.SetActive(false);
        menuo.SetActive(true);
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

        else if (CrossPlatformInputManager.GetButtonDown("menu-inventory"))
        {
            onMenuSelected?.Invoke(selectedItem);
            CloseMenu();
            
            if (CrossPlatformInputManager.GetButtonDown("menu-button-close"))
            {
                onBack?.Invoke();
               
            }



        }

       




    }
    void UpdateItemSelection()
    {
        for (int i = 0; i < menuItems.Count; i++)
        {

        }
    }

}
