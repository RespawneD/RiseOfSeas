using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class HumanoidAI : AI {

    public List<AudioClip> grunts;

    [SerializeField] private GameObject indItem;
    [SerializeField] private Collider frontDetector;

    private Vector3 dir;

    private void OnAnimatorMove()
    {
        navAgent.velocity = am.deltaPosition / Time.deltaTime;
    }
    public override void OnDead()
    {
        am.SetBool("isDead", isDead);
        am.SetInteger("deadSelector", Random.Range(0, 4));
        am.SetBool("isAttacking", false);
        am.SetBool("drawSword", false);
        am.SetBool("sheatSword", false);
        //Persistant
        // DROP BAG //

        Destroy(gameObject, 600);
        

        //Set all collider to nonTrigger Then destroy it 
        Destroy(GetComponent<Rigidbody>());
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            Destroy(c, 2f);
        }
        StopAllCoroutines();

        GetComponent<NavMeshAgent>().enabled = false;

        Instantiate(bloodOcean, transform.position, Quaternion.identity);

        //foreach (Collider c in GetComponentsInChildren<Collider>())
        //   c.enabled = false;


    }
    private void OnTriggerEnter(Collider collider)
    {

        Weapon w;

        if (w = collider.GetComponent<Weapon>())
        {

            //Get Entity

            if (w.transform.root.GetComponent<Entity>().faction == faction)
                return;

            ScriptableWeapon weaponData = (ScriptableWeapon)w.data;

            TakeDamage(this, weaponData.damage);
            am.SetTrigger("hit");
            Instantiate(bloodSplash, collider.transform.position, Quaternion.identity);
            target = collider.transform.root;
            w.GetComponent<Collider>().enabled = false;


            DamageIndicatorItem i = Instantiate(indItem, transform.Find("DamageIndicator")).GetComponent<DamageIndicatorItem>();
            i.damage = (int)weaponData.damage;
            i.c = Color.red;
            i.speed = Random.Range(1f, 2f);

            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().PlayOneShot(grunts[Random.Range(0, grunts.Count - 1)]);


        }
    }

    private void Start()
    {
        AIStart();
        StartWalkingRandomly();
    }

    private void Update () {

        CheckIfDead();
        if (isDead)
            return;


        isWalkingRandomly = (target == null);

        /*if (target != null)
            transform.LookAt(target);*/

        if (job == EntityJob.Guard)
        {

            Entity e = null;

            if (target != null) // Try get Entity...
                e = target.GetComponent<Entity>();


            am.SetBool("drawSword", e != null && ts.GetItem("Weapon_R").childCount == 0);
            am.SetBool("sheatSword", e == null && ts.GetItem("Weapon_R").childCount != 0);


            if (target != null) // Walk to target...
                if (IsValidePath())
                    navAgent.SetDestination(target.position);
                else
                    navAgent.SetDestination(GetNearestAvailablePointFromTarget());


            if (target == null) // Check if IA can do something...
            {
                target = GetNearestEnnemyInRadius(10f, null);
            }




            if (e != null) // Target is an entity...
            {
                if (IsTargetInRange(3f) && ts.GetItem("Weapon_R").childCount != 0)
                {
                    am.SetBool("isAttacking", true);
                }
                else
                    am.SetBool("isAttacking", false);


                //Stop attacking if entity is not Agressive and hes not in ennemy faction
                if (e.state != EntityState.Aggressive && !ennemy.Contains(e.faction))
                    target = null;


                if (e.isDead)
                {
                    target = null;

                    //Try to get Another target
                    target = GetNearestEnnemyInRadius(10f, null);

                    am.SetBool("isAttacking", false);
                }

                




            }



            
            am.SetFloat("speed", ReachedTarget());

            


            return;
        }


	}
}
