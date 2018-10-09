using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderManager : MonoBehaviour {

	public void DisableCollider()
    {
        GetComponent<Player>().toolT.GetComponentInChildren<Collider>().enabled = false;
    }

    public void EnableCollider()
    {
        GetComponent<Player>().toolT.GetComponentInChildren<Collider>().enabled = true;
    }

}
