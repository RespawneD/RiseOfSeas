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
        Transform weapon = ts.GetItem("Sabre").GetChild(0);
        weapon.SetParent(ts.GetItem("Weapon_R"));
        weapon.localPosition = Vector3.zero;
        weapon.localRotation = Quaternion.Euler(97, -145, 50);

        weapon.GetComponent<AudioSource>().PlayOneShot(weapon.GetComponent<Weapon>().data.drawSound);


    }

    public void SheatPrimary()
    {

        Transform weapon = ts.GetItem("Weapon_R").GetChild(0);

        weapon.SetParent(ts.GetItem("Sabre"));
        weapon.localPosition = Vector3.zero;
        weapon.localRotation = Quaternion.identity;
        weapon.GetComponent<AudioSource>().PlayOneShot(weapon.GetComponent<Weapon>().data.sheatSound);

    }

    public void DisableCollider()
    {

        ts.GetItem("Weapon_R").GetChild(0).GetComponentInChildren<Collider>().enabled = false;
    }

    public void EnableCollider()
    {
        Weapon p = ts.GetItem("Weapon_R").GetChild(0).GetComponent<Weapon>();
        p.GetComponentInChildren<Collider>().enabled = true;
        p.GetComponent<AudioSource>().PlayOneShot(p.data.actionsSound[Random.Range(0, p.data.actionsSound.Count - 1)]);
    }

}
