using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISkeleton : MonoBehaviour {

    public Transform target;

    private Transform gCenter;

    private Vector3 dir;
    private Animator am;

    public int isWaiting;

    private void Start()
    {
        gCenter = transform.Find("GravityCenter");
        am = GetComponent<Animator>();
        StartCoroutine(RandomDir());

    }

    void AvoidGoingWater()
    {
        RaycastHit hit;
        if(Physics.Raycast(gCenter.position, Vector3.down + transform.forward, out hit))
        {
            if(hit.collider.CompareTag("Water"))
            {
                target = null;
                dir = -dir;
                transform.rotation = Quaternion.LookRotation(dir);
            }
        }
    }


    void GoToTarget()
    {
        
        transform.LookAt(target);
        dir = transform.forward;
        am.SetFloat("speed", Mathf.Clamp01(Vector3.Distance(target.position, transform.position) - 1));
    }

    IEnumerator RandomDir()
    {
        while(target == null)
        {
            isWaiting = Random.Range(0, 2);

            if(isWaiting == 0)
            {
                am.SetFloat("speed", 0);
                yield return new WaitForSeconds(2);
            }else
            {
                am.SetFloat("speed", 1f);
            }

            dir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            transform.rotation = Quaternion.LookRotation(dir);
            
            yield return new WaitForSeconds(5);
        }
    }

	void Update () {
        if(target != null)
            GoToTarget();
        AvoidGoingWater();
	}



}
