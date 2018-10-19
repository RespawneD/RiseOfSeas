using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EntityJob
{
    Citizen,
    Swordman,
    Captain,
    Robber,
    WoodCutter,
    Miner,
    Farmer,
    Guard

}

public enum EntityState
{
    Passive,
    Neutral,
    Aggressive
}

public enum Faction
{
    Skeleton,
    Imperial,
    Dishonored
}

public class Entity : MonoBehaviour {

    public GameObject bloodSplash;
    public GameObject bloodOcean;

    public float maxLife;
    public float life;
    public bool isDead;
    public bool isGod;

    public Faction faction;
    public EntityJob job;
    public EntityState defaultState;
    public EntityState state;

    protected void TakeDamage(Entity e, float damage)
    {
        if (isGod)
            return;

        life -= damage;
        life = Mathf.Clamp(life, 0, maxLife);

    }

    public virtual void EntityStart()
    {
        life = maxLife;
    }

    public virtual void CheckIfDead()
    {
        if (life == 0 && !isDead)
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
