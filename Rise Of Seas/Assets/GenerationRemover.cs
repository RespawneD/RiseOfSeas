using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationRemover : MonoBehaviour {

    public bool canDestroy;

    public bool isKept;

    private void Start()
    {
        GetComponent<MeshCollider>().convex = true;
        GetComponent<MeshCollider> ().isTrigger = true;
        

    }

    IEnumerator dest()
    {
        while(true)
        {
            yield return new WaitForSeconds(3f);
            break;
        }

        canDestroy = true;

    }

    private void OnTriggerStay(Collider other)
    {

        if (!canDestroy)
            return;

        if (other.CompareTag("Island") || other.CompareTag("Water"))
        {
            GetComponent<MeshCollider>().isTrigger = false;
            GetComponent<MeshCollider>().convex = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
