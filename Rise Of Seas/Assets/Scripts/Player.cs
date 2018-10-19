using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {
    public List<AudioClip> grunts;

    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject indItem;
    public GameObject selectedItem;


    private Animator am;

    public void Start()
    {
        EntityStart();
        am = GetComponent<Animator>();
        Instantiate(ui, transform);
    }


    private void OnTriggerEnter(Collider other)
    {
        Item i;
        if ((i = other.GetComponent<Item> ()) != null && i.isOnGround && selectedItem == null)
        {
            selectedItem = i.gameObject;
            i.DisplayItemInfos();


            return;
        }


        Weapon w;

        if ((w = other.GetComponent<Weapon>()) != null && !w.isOnGround && w.transform.root != transform)
        {

            ScriptableWeapon weaponData = (ScriptableWeapon)w.data;

            w.GetComponent<Collider>().enabled = false;
            TakeDamage(this, weaponData.damage);

            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().PlayOneShot(grunts[Random.Range(0, grunts.Count - 1)]);

            DamageIndicatorItem ind = Instantiate(indItem, transform.Find("DamageIndicator")).GetComponent<DamageIndicatorItem>();
            ind.damage = (int)weaponData.damage;
            ind.c = Color.red;
            ind.speed = Random.Range(1f, 2f);

            am.SetTrigger("hit");
        }



    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == selectedItem)
        {
            selectedItem.GetComponent<Item>().HideItemInfos();
            selectedItem = null;
        }
    }

}
