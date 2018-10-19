using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationManager : MonoBehaviour
{

    TransformsShortcut ts;
    Entity e;
    public void Start()
    {
       ts = GetComponent<TransformsShortcut>();
        e = GetComponent<Entity>();
    }

    public void DrawPrimary()
    {
        Transform weapon = ts.GetItem("Sabre").GetChild(0);
        weapon.SetParent(ts.GetItem("Weapon_R"));
        weapon.localPosition = Vector3.zero;
        weapon.localRotation = Quaternion.Euler(97, -145, 50);


        ScriptableWeapon weaponData = (ScriptableWeapon)weapon.GetComponent<Weapon>().data;
        weapon.GetComponent<AudioSource>().PlayOneShot(weaponData.drawSound);

        e.state = EntityState.Aggressive;

    }

    public void SheatPrimary()
    {

        Transform weapon = ts.GetItem("Weapon_R").GetChild(0);

        weapon.SetParent(ts.GetItem("Sabre"));
        weapon.localPosition = Vector3.zero;
        weapon.localRotation = Quaternion.identity;

        ScriptableWeapon weaponData = (ScriptableWeapon)weapon.GetComponent<Weapon>().data;

        weapon.GetComponent<AudioSource>().PlayOneShot(weaponData.sheatSound);

        e.state = e.defaultState;

    }

    public void DisableCollider()
    {

        ts.GetItem("Weapon_R").GetChild(0).GetComponentInChildren<Collider>().enabled = false;
    }

    public void EnableCollider()
    {
        Weapon p = ts.GetItem("Weapon_R").GetChild(0).GetComponent<Weapon>();
        p.GetComponentInChildren<Collider>().enabled = true;

        ScriptableWeapon weaponData = (ScriptableWeapon)p.GetComponent<Weapon>().data;

        p.GetComponent<AudioSource>().PlayOneShot(weaponData.actionsSound[Random.Range(0, weaponData.actionsSound.Count - 1)]);
    }

}
