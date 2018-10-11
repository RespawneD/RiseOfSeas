using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : AI {


    public Transform target;
    public int isWaiting;

    public List<AudioClip> grunts;

    [SerializeField] private GameObject indItem;
    [SerializeField] private GameObject weapon;

    private TransformsShortcut ts;
    private Transform gCenter;
    private Vector3 dir;
    private Animator am;

    

    private new void Start()
    {
        base.Start();
        gCenter = transform.Find("GravityCenter");
        am = GetComponent<Animator>();
        ts = GetComponent<TransformsShortcut>();

        Transform g = Instantiate(weapon, ts.GetItem("Weapon_R")).transform;
        g.localPosition = Vector3.zero;
        g.localRotation = Quaternion.identity;
        StartCoroutine(RandomDir());
        

    }

    void AvoidGoingWater()
    {
        RaycastHit hit;
        if (Physics.Raycast(gCenter.position, Vector3.down + transform.forward, out hit))
        {
            if (hit.collider.CompareTag("Water"))
            {
                target = null;
                dir = -dir;
                transform.rotation = Quaternion.LookRotation(dir);
            }
        }

        if (Physics.Raycast(gCenter.position, transform.forward, out hit, 2f))
        {
            dir = Vector3.Cross(dir, Vector3.up);
        }
    }

    void GoToTarget()
    {
        
        transform.rotation = Quaternion.LookRotation(new Vector3(
            target.position.x,
            0,
            target.position.z) - new Vector3(
                transform.position.x,
                0,
                transform.position.z), Vector3.up);

        dir = transform.forward;
        

        if(Vector3.Distance(transform.position, target.position) < 1.4f)
        {
            am.SetFloat("speed", 0);
            am.SetBool("isAttacking", true);
        }else
        {
            am.SetBool("isAttacking", false);
            am.SetFloat("speed", 1);
        }

    }

    IEnumerator RandomDir()
    {
        while (true)
        {
            if(target !=null)
            {
                yield return null;
                continue;
                
            }

            isWaiting = Random.Range(0, 2);

            if (isWaiting == 0)
            {
                am.SetFloat("speed", 0);
                yield return new WaitForSeconds(2);
            }
            else
            {
                am.SetFloat("speed", 1f);
            }

            dir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            transform.rotation = Quaternion.LookRotation(dir);

            yield return new WaitForSeconds(5);
        } 
    }

    

    public override void OnDead()
    {
        am.SetBool("isDead", isDead);
        //Destroy(gameObject, 3f);
        //Persistant

        Destroy(gameObject, 600);
        GetComponent<Rigidbody>().isKinematic = true;
        foreach (Collider c in GetComponentsInChildren<Collider>())
            c.enabled = false;


    }

    new void Update () {

        if (isDead)
        {
            am.SetBool("isAttacking", false);
            return;
        }

        base.Update();

        if(target != null)
        {
            if (target.GetComponent<Entity>().isDead)
            {
                am.SetBool("isAttacking", false);
                target = null;
            }
                
            else
                GoToTarget();

        }

        if (target == null)
            target = GrabTarget(10);

        AvoidGoingWater();
	}

    private void OnCollisionEnter(Collision collision)
    {

        Weapon w;

        if(w = collision.collider.GetComponent<Weapon>())
        {

            //Get Entity

            if (w.transform.root.GetComponent<Entity>().faction == faction)
                return;

            TakeDamage(this, w.data.damage);
            am.SetTrigger("hit");
            target = collision.collider.transform.root;
            w.GetComponent<Collider>().enabled = false;


            DamageIndicatorItem i = Instantiate(indItem, transform.Find("DamageIndicator")).GetComponent<DamageIndicatorItem> ();
            i.damage = (int)w.data.damage;
            i.c = Color.red;
            i.speed = Random.Range(1f, 2f);

            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().PlayOneShot(grunts[Random.Range(0, grunts.Count - 1)]);

        }
    }

}
