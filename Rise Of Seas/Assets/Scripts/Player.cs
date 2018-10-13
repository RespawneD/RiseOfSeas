using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {
    public List<AudioClip> grunts;

    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject indItem;
    public GameObject selectedItem;


    private Animator am;

    public new void Start()
    {
        base.Start();
        am = GetComponent<Animator>();
        Instantiate(ui, transform);
    }

    private void OnCollisionEnter(Collision collision)
    {

        Weapon w;

        if ((w = collision.collider.GetComponent<Weapon>()) != null && !w.isOnGround)
        {

            ScriptableWeapon weaponData = (ScriptableWeapon)w.data;

            TakeDamage(this, weaponData.damage);

            if (!GetComponent<AudioSource> ().isPlaying)
                GetComponent<AudioSource>().PlayOneShot(grunts[Random.Range(0, grunts.Count -1)]);

            DamageIndicatorItem i = Instantiate(indItem, transform.Find("DamageIndicator")).GetComponent<DamageIndicatorItem>();
            i.damage = (int)weaponData.damage;
            i.c = Color.red;
            i.speed = Random.Range(1f, 2f);

            am.SetTrigger("hit");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Item i;
        if ((i = other.GetComponent<Item> ()) != null && i.isOnGround && selectedItem == null)
        {
            selectedItem = i.gameObject;
            i.DisplayItemInfos();
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
