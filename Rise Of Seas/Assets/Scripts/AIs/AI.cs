using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AI : Entity {

    public List<Faction> ennemy;
    public List<Faction> neutral;
    public List<Faction> friends;

    public bool debug;


    public Transform GrabTarget(float radius)
    {
        Entity e;
        Collider[] cols = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider c in cols)
            if (e = c.transform.root.GetComponent<Entity>())
            {
                if (e.isGod || e.faction == faction)
                    continue;
                return c.transform.root;
            }
        return null;

    }
}
