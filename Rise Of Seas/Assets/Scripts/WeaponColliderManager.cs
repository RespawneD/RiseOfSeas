using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderManager : MonoBehaviour {



    private TransformsShortcut ts;

    private void Start()
    {
        ts = GetComponent<TransformsShortcut>();
    }

    public void DisableCollider()
    {
       ts.GetItem("Weapon_R").GetChild(0).GetComponentInChildren<Collider>().enabled = false;
    }

    public void EnableCollider()
    {
        ts.GetItem("Weapon_R").GetChild(0).GetComponentInChildren<Collider>().enabled = true;
    }

}
