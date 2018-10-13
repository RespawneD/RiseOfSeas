using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour {

    public List<Inventory> invs;

    public void Start()
    {
        invs = new List<Inventory>();
        invs.Add(new Inventory(200));
    }

    public bool AddInInventory(Item i)
    {
        foreach (Inventory inv in invs)
            if (inv.AddItem(i)) return true;
        return false;
    }


}
