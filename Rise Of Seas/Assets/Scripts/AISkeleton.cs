using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISkeleton : Entity {

    public Transform target;

    private Transform gCenter;

    private Vector3 dir;
    private Animator am;

    public int isWaiting;

    private new void Start()
    {
        base.Start();
        gCenter = transform.Find("GravityCenter");
        am = GetComponent<Animator>();
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
        
        transform.LookAt(target);
        dir = transform.forward;
        am.SetFloat("speed", 1);

        if(Vector3.Distance(transform.position, target.position) < 1f)
        {
            am.SetBool("isAttacking", true);
        }else
        {
            am.SetBool("isAttacking", false);
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
        Destroy(gameObject, 3f);
    }

    new void Update () {

        if (isDead)
        {
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
           
        AvoidGoingWater();
	}

    private void OnCollisionEnter(Collision collision)
    {

        Weapon w;

        if(w = collision.collider.GetComponent<Weapon>())
        {
            TakeDamage(this, w.damage);
            am.SetTrigger("hit");
        }
    }

}
