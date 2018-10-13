using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Skeleton : AI {


    public Transform target;
    public int isWaiting;

    public List<AudioClip> grunts;

    [SerializeField] private GameObject indItem;
    [SerializeField] private GameObject weapon;
    [SerializeField] private Collider frontDetector;

    private TransformsShortcut ts;
    private Transform gCenter;
    private Vector3 dir;
    private Animator am;
    private NavMeshAgent navAgent;

    

    private new void Start()
    {
        base.Start();
        gCenter = transform.Find("GravityCenter");
        am = GetComponent<Animator>();
        ts = GetComponent<TransformsShortcut>();

        navAgent = GetComponent<NavMeshAgent>();

        Transform g = Instantiate(weapon, ts.GetItem("Weapon_R")).transform;
        g.localPosition = Vector3.zero;
        g.localRotation = Quaternion.identity;

        StartCoroutine(RandomMoves());

    }


    

    public override void OnDead()
    {
        am.SetBool("isDead", isDead);
        //Destroy(gameObject, 3f);

        //Persistant
        // DROP BAG //

        Destroy(gameObject, 600);
        GetComponent<NavMeshAgent>().enabled = false;
        StopAllCoroutines();
        //foreach (Collider c in GetComponentsInChildren<Collider>())
         //   c.enabled = false;


    }


    IEnumerator RandomMoves()
    {
        while(true)
        {
            if (target != null)
            {
                yield return null;
                continue;
            }
                

            if(Random.Range(0, 2) != 0) // 0 is WAITING
            {
                navAgent.SetDestination(GetRandomPoint(5f));
                am.SetFloat("speed", 1);
            }
            yield return new WaitForSeconds(15f);


        }
    }

    Vector3 GetRandomPoint(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, radius, 1);
        return hit.position;
    }

    new void Update () {

        base.Update();

        if (isDead)
            return;

        if (target != null && navAgent.pathStatus == NavMeshPathStatus.PathComplete)
            navAgent.SetDestination(target.position);

        // target is unreachable
        if(navAgent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            NavMeshHit hit;
            NavMesh.SamplePosition(target.position, out hit, 10f, 1);
            navAgent.SetDestination(hit.position);
        }



        if (navAgent.remainingDistance < 1.4f)
            am.SetFloat("speed", 0);
        else
            am.SetFloat("speed", 1);

        Debug.Log(navAgent.remainingDistance);
        
	}

    private void OnCollisionEnter(Collision collision)
    {

        Weapon w;

        if(w = collision.collider.GetComponent<Weapon>())
        {

            //Get Entity

            if (w.transform.root.GetComponent<Entity>().faction == faction)
                return;

            ScriptableWeapon weaponData = (ScriptableWeapon)w.data;

            TakeDamage(this, weaponData.damage);
            am.SetTrigger("hit");
            target = collision.collider.transform.root;
            w.GetComponent<Collider>().enabled = false;

            
            DamageIndicatorItem i = Instantiate(indItem, transform.Find("DamageIndicator")).GetComponent<DamageIndicatorItem> ();
            i.damage = (int)weaponData.damage;
            i.c = Color.red;
            i.speed = Random.Range(1f, 2f);

            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().PlayOneShot(grunts[Random.Range(0, grunts.Count - 1)]);
                
            
        }
    }

    private void OnAnimatorMove()
    {
        navAgent.velocity = am.deltaPosition / Time.deltaTime;
    }

}
