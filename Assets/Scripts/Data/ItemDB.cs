using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB //: ScriptableObjectDB<ItemBase>
{
    static Dictionary<string, ItemBase> objects;

    public static void Init()
    {
        objects = new Dictionary<string, ItemBase>();

        var objectArray = Resources.LoadAll<ItemBase>("");
        foreach (var obj in objectArray)
        {
            if (objects.ContainsKey(obj.name))
            {
                Debug.LogError($"ItemBasehere are two items with the name {obj.name}");
                continue;
            }
            objects[obj.name] = obj;
        }
    }

    public static ItemBase GetObjectByName(string name)
    {
        if (!objects.ContainsKey(name))
        {
            Debug.LogError($"Item with name {name} not found in the database");
            return null;
        }
        return objects[name];
    }

}

