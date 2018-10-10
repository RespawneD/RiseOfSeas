using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

    public TransformsShortcut transforms;

    public new void Start()
    {
        base.Start();
        GetComponent<TransformsShortcut>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        Weapon w;

        if (w = collision.collider.GetComponent<Weapon>())
        {
            TakeDamage(this, w.damage);
            //am.SetTrigger("hit");
        }
    }

}
