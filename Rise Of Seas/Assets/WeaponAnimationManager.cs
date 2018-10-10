using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationManager : MonoBehaviour
{

    TransformsShortcut ts;

    public void Start()
    {
       ts = GetComponent<TransformsShortcut>();
    }

    public void DrawPrimary()
    {
        
        ts.GetItem("Sabre").GetChild(0).SetParent(ts.GetItem("Weapon_R"));
        ts.GetItem("Weapon_R").GetChild(0).localPosition = Vector3.zero;
        ts.GetItem("Weapon_R").GetChild(0).localRotation = Quaternion.Euler(97, -145, 50);
    }

    public void SheatPrimary()
    {

        ts.GetItem("Weapon_R").GetChild(0).SetParent(ts.GetItem("Sabre"));
        ts.GetItem("Sabre").GetChild(0).localPosition = Vector3.zero;
        ts.GetItem("Sabre").GetChild(0).localRotation = Quaternion.identity;
    }

}
