using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem {



    private GameObject itemGameObject;

    public string name;
    public int id;

    public Item item;
    public float totalWeight;
    public int quantity;

    public InventoryItem(Item i)
    {

        id = i.data.id;
        item = i;
        itemGameObject = i.data.item;
        name = i.data.name;
        Add(i);
        
    }

    public void Add(Item i)
    {
        totalWeight += i.data.weight;
        quantity++;
    }

    public GameObject Remove(Item i)
    {
        quantity--;
        totalWeight -= i.data.weight;
        return itemGameObject;
    }



}
