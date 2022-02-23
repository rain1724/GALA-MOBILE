using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ItemCategory { Items }

public class Inventory : MonoBehaviour, ISavable
{
    [SerializeField] List<ItemSlot> slots;
    

    List<List<ItemSlot>> allSlots;

    public event Action OnUpdated;
 
    

    private void Awake()
    {
        allSlots = new List<List<ItemSlot>>() { slots };
    }

    //Categorize Items
    public static List<string> ItemCategories { get; set; } = new List<string>()
    {
        
        "ITEMS"
    };

    public List<ItemSlot> GetSlotsByCategory(int categoryIndex)
    {
        return allSlots[categoryIndex];
    }

    public ItemBase GetItem(int itemIndex, int categoryIndex)
    {
        var currenSlots = GetSlotsByCategory(categoryIndex);
        return currenSlots[itemIndex].Item;
    }

    
    public void AddItem(ItemBase item, int count=1)
    {
        int category = (int)GetCategoryFromItem(item);
        var currentSlots = GetSlotsByCategory(category);

        var itemSlot = currentSlots.FirstOrDefault(slot => slot.Item == item);
        if (itemSlot != null)
        {
            itemSlot.Count += count;
        }
        else
        {
            currentSlots.Add(new ItemSlot()
            {
                Item = item,
                Count = count

            });
        }

        OnUpdated?.Invoke();
    }

    public void RemoveItem(ItemBase item)
    {
        int category = (int)GetCategoryFromItem(item);
        var currentSlots = GetSlotsByCategory(category);

        var itemSlot = slots.First(slot => slot.Item == item);
        itemSlot.Count--;
        if (itemSlot.Count == 0)
            slots.Remove(itemSlot);

        OnUpdated?.Invoke();
    }

    public bool HasItem(ItemBase item)
    {
        int category = (int)GetCategoryFromItem(item);
        var currentSlots = GetSlotsByCategory(category);

        return currentSlots.Exists(slot => slot.Item == item);
    }

    ItemCategory GetCategoryFromItem(ItemBase item)
    {
        //if (item is RecoveryItem)
            return ItemCategory.Items;

        //else 
          //  return ItemCategory.Items;
    }

    public static Inventory GetInventory()
    {
        return FindObjectOfType<PlayerController>().GetComponent<Inventory>();
    }

    public object CaptureState()
    {
        var saveData = new InventorySaveData()
        {
            //converting list of item slot to list of item save data
            items = slots.Select(i => i.GetSaveData()).ToList(),
            

        };

        return saveData;
    }

    //restore the item
    public void RestoreState(object state)
    {
        var saveData = state as InventorySaveData;
        slots = saveData.items.Select(i => new ItemSlot(i)).ToList();
       

        allSlots = new List<List<ItemSlot>>() { slots }; //pokeballSlots, tmSlots };
        OnUpdated?.Invoke();
    }
}

[Serializable]
public class ItemSlot
{
    [SerializeField] ItemBase item;
    [SerializeField] int count;

    public ItemSlot()
    {
        //default
    }
    //restore the itemslot from the item save data
    //get the item name from game db dictionary as a key to
    //restore the values of variables
    public ItemSlot(ItemSaveData saveData)
    {
        item = ItemDB.GetObjectByName(saveData.name);
        count = saveData.count;
    }

    //get parameters of the item to save
    public ItemSaveData GetSaveData()
    {
        var saveData = new ItemSaveData();
        if (saveData != null)
        {
            saveData = new ItemSaveData()
            {
                name = item.Name,
                count = count
            };
        }
        return saveData;

       
    }
    public ItemBase Item {
        get => item;
        set => item = value;
    }

    public int Count{
        get => count;
        set => count = value;
    }
}

[Serializable]
public class ItemSaveData
{
    public string name;
    public int count;
}

[Serializable]
public class InventorySaveData
{
    public List<ItemSaveData> items;
    public List<ItemSaveData> pokeballs;
    public List<ItemSaveData> tms;

}
