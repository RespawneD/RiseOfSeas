using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Inventory{

    public List<InventoryItem> items;

    public float maxWeight;
    public float weight;


    public Inventory(float maxWeight)
    {
        items = new List<InventoryItem>();
        this.maxWeight = maxWeight;
    }

    private void TotalWeight()
    {
        weight = 0;
        foreach(InventoryItem i in items)
            weight += i.totalWeight;
    }
    private bool CheckBeforeAdd(Item i){ return (weight + i.data.weight) <= maxWeight; }

    private InventoryItem Find(Item i)
    {
        foreach(InventoryItem item in items)
            if (item.id == i.data.id)
                return item;
        return null;
    }

    public void CreateInventoryItem(Item i)
    {
        items.Add(new InventoryItem(i));
    }

    public bool AddItem(Item i)
    {
        InventoryItem inventoryItem;
        if (CheckBeforeAdd(i))
        {
            if ((inventoryItem = Find(i)) != null) inventoryItem.Add(i);
            else CreateInventoryItem(i);
            return true;
        }
        return false;
            



    }



}
