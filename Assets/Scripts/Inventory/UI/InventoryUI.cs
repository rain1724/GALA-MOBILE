using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;


public enum InventoryUIState { ItemSelection, Busy}
public class InventoryUI : MonoBehaviour
{

    [SerializeField] GameObject itemList;
    [SerializeField] ItemSlotUI itemSlotUI;

    [SerializeField] Text categoryText;
    [SerializeField] Image itemIcon;
    [SerializeField] Text itemDescription;

    int selectedItem = 0;
    int selectedCategory = 0;

    InventoryUIState state;

    List<ItemSlotUI> slotUIList;

    Inventory inventory;

    private void Awake()
    {
        inventory = Inventory.GetInventory();
    }


    private void Start()
    {
        UpdateItemList();
        inventory.OnUpdated += UpdateItemList;
    }


    void UpdateItemList()
    {
        //clear all the existing items
        foreach (Transform child in itemList.transform)
            Destroy(child.gameObject);

        slotUIList = new List<ItemSlotUI>();

        foreach (var itemSlot in inventory.GetSlotsByCategory(selectedCategory))
        {
            var slotUIObj = Instantiate(itemSlotUI, itemList.transform);
            slotUIObj.SetData(itemSlot);

            slotUIList.Add(slotUIObj);
        }

        UpdateItemSelection();
    }
    public void HandleUpdate(Action onBack)
    {
        int prevSelection = selectedItem;

       

        if (Input.GetKeyDown(KeyCode.DownArrow))
            ++selectedItem;

        else if (Input.GetKeyDown(KeyCode.UpArrow))
            --selectedItem;

        

        selectedItem = Mathf.Clamp(selectedItem, 0, inventory.GetSlotsByCategory(selectedCategory).Count - 1);

        if (prevSelection != selectedItem)
            UpdateItemSelection();
        

        if (CrossPlatformInputManager.GetButtonDown("menu-inventory"))
        {
            onBack?.Invoke();

           
        }
        if (CrossPlatformInputManager.GetButtonDown("menu-button-close"))
            onBack?.Invoke();
    }
    void UpdateItemSelection()
    {
        var slots = inventory.GetSlotsByCategory(selectedCategory);

        for (int i = 0; i < slotUIList.Count; i++)
        {
            if (i == selectedItem)
                slotUIList[i].NameText.color = GlobalSettings.i.HighlightedColor;
            else
                slotUIList[i].NameText.color = Color.black;
        }
        if (slots.Count > 0)
        {
            var item = slots[selectedItem].Item;
            itemIcon.sprite = item.Icon;
            itemDescription.text = item.Description;
        }
    }
}
