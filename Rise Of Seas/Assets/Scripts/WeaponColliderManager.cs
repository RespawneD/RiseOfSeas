using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderManager : MonoBehaviour {



    private Transform weapT;

    private void Start()
    {
        weapT = GetComponentInChildren<Weapon>().transform;
    }

    public void DisableCollider()
    {
       weapT.GetComponentInChildren<Collider>().enabled = false;
    }

    public void EnableCollider()
    {
       weapT.GetComponentInChildren<Collider>().enabled = true;
    }

}
