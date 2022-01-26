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
    [SerializeField] GameObject menuo;
    [SerializeField] GameObject menux;


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
 
    }

    public void CloseMenu()
    {
        
        menu.SetActive(false);
        menux.SetActive(false);



    }
    public void HandleUpdate()
    {
        if (CrossPlatformInputManager.GetButtonDown("menu-button-close"))
        {
            onBack?.Invoke();
            
            CloseMenu();
        }
      
    }
}
