using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class AI : Entity {

    public List<Faction> ennemy;
    public List<Faction> neutral;
    public List<Faction> friends;

    public bool isWalkingRandomly = true;
    public Transform target;
    public GameObject weapon;


    protected Animator am;
    protected NavMeshAgent navAgent;
    protected Transform gCenter;
    protected TransformsShortcut ts;

    private Vector3 GetRandomPoint(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, radius, 1);
        return hit.position;
    }
    private IEnumerator WalkingRandomly()
    {
        while (true)
        {

            if (!isWalkingRandomly)
                yield return null;

            float time = Random.Range(2f, 6f);
            yield return new WaitForSeconds(time);

            // Decide if move or stay in place ?
            if (Random.Range(0, 2) != 0) // 0 is WAITING
            {
                navAgent.SetDestination(GetRandomPoint(5f));
                am.SetFloat("speed", 1);
            }




        }
    }

    public void StartWalkingRandomly()
    {
        StartCoroutine(WalkingRandomly());
    }
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
   
    protected bool IsValidePath()
    {
        return navAgent.pathStatus == NavMeshPathStatus.PathPartial;
    }
    protected bool IsTargetInRange(float distance)
    {
        return target != null && Vector3.Distance(transform.position, target.position) < distance;
    }
    protected Vector3 GetNearestAvailablePointFromTarget()
    {
        if (target == null)
            return Vector3.zero;

        NavMeshHit hit;
        NavMesh.SamplePosition(target.position, out hit, 1f, 1);
        return hit.position;
    }
    protected float ReachedTarget()
    {
        if (!navAgent.pathPending)
        {
            if (navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                return 0;
            }
        }
        return 1; // target not reached
    }
    protected void AIStart()
    {
        EntityStart();
        gCenter = transform.Find("GravityCenter");
        am = GetComponent<Animator>();
        ts = GetComponent<TransformsShortcut>();

        navAgent = GetComponent<NavMeshAgent>();

        Transform g = Instantiate(weapon, ts.GetItem("Sabre")).transform;
        g.localPosition = Vector3.zero;
        g.localRotation = Quaternion.identity;
    }
    protected List<Entity> GetEntitysInRadius(float radius)
    {
        List<Entity> transforms = new List<Entity>();
        Entity e;
        foreach (Collider c in Physics.OverlapSphere(transform.position, radius))
            if ((e = c.transform.root.GetComponent<Entity>()) != null && !transforms.Contains(e) && e.transform != transform && !e.isDead && !e.isGod)
                transforms.Add(e);

        return transforms;
        
    }
    protected void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }
    protected List<Entity> GetEnnemysInRadius(float radius)
    {
        List<Entity> entitys = GetEntitysInRadius(radius);
        List<Entity> res = new List<Entity>();
        foreach (Entity e in entitys)
            if (e.state == EntityState.Aggressive && !friends.Contains(e.faction) || ennemy.Contains(e.faction))
                res.Add(e);

        return res;
    }
    protected Entity GetNearestEnnemyInRadius(float radius)
    {
        List<Entity> entitys = GetEnnemysInRadius(radius);
        Entity entity = null;
        float minDist = 0;
        if (entitys.Count == 0)
            return null;

        entity = entitys[0];
        minDist = Vector3.Distance(entity.transform.position, transform.position);
        foreach(Entity e in entitys)
            if(Vector3.Distance(e.transform.position, transform.position) < minDist)
            {
                minDist = Vector3.Distance(e.transform.position, transform.position);
                entity = e;
            }
        return entity;

    }
    protected Transform GetNearestEnnemyInRadius(float radius, Transform w)
    {
        List<Entity> entitys = GetEnnemysInRadius(radius);
        Transform t = null;
        float minDist = 0;
        if (entitys.Count == 0)
            return null;

        t = entitys[0].transform;
        minDist = Vector3.Distance(t.position, transform.position);
        foreach (Entity e in entitys)
            if (Vector3.Distance(e.transform.position, transform.position) < minDist)
            {
                minDist = Vector3.Distance(e.transform.position, transform.position);
                t = e.transform;
            }
        return t;

    }


    private void OnDrawGizmosSelected()
    {
        if (navAgent == null)
            return;
        if (navAgent.destination != Vector3.zero)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(navAgent.destination, Vector3.one * .5f);

        }
            
    }
}
