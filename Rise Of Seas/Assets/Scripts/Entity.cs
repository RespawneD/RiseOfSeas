using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Faction
{
    Skeleton,
    Imperial,
    Dishonored
}

public class Entity : MonoBehaviour {

    public float maxLife;
    public float life;
    public bool isDead;
    public bool isGod;

    public Faction faction;

    protected void TakeDamage(Entity e, float damage)
    {
        if (isGod)
            return;

        life -= damage;
        life = Mathf.Clamp(life, 0, maxLife);

    }

    public virtual void Start()
    {
        life = maxLife;
    }

    public virtual void Update()
    {
        if (life == 0)
        {
            isDead = true;
            OnDead();
        }
    }


    public virtual void OnDead()
    {
        Destroy(gameObject);
    }


}
