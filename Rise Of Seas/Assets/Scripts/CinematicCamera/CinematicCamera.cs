using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCamera : MonoBehaviour {


    [System.Serializable]
    public class Key
    {
        public Vector3 position;
        public Quaternion rotation;

        public float rotationSpeed = 1f;
        public float translationSpeed = 1f;



        public Key(Vector3 position, Quaternion rotation, float rotationSpeed, float translationSpeed)
        {
            this.position = position;
            this.rotation = rotation;
            this.rotationSpeed = rotationSpeed;
            this.translationSpeed = translationSpeed;
        }


        public override string ToString()
        {
            return position.ToString() + ", " + rotation.ToString() + ", " + rotationSpeed.ToString() + ", " + translationSpeed.ToString();
        }

    }

    public Transform target;
    public List<Key> keys;

    private Key currentKey;

    [SerializeField] private bool isFinish = false;

    void OnDrawGizmosSelected()
    {

        if (keys == null)
            return;

        foreach (Key k in keys)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(k.position, .2f);
        }
    }

    private void Start()
    {


        transform.position = keys[0].position;
        transform.rotation = keys[0].rotation;

        Debug.Log("KEY 0 : " + keys[0].ToString());

        keys.RemoveAt(0);
        currentKey = keys[0];
    }

    void Update()
    {

        if (isFinish)
            return;

        transform.Translate((keys[0].position - transform.position).normalized * Time.deltaTime * keys[0].translationSpeed, Space.World);
        //transform.rotation = Quaternion.Slerp(transform.rotation, currentKey.rotation, currentKey.rotationSpeed * Time.deltaTime);
        transform.LookAt(target);
        if (Vector3.Distance(transform.position, currentKey.position) <= 10f)
        {
            keys.RemoveAt(0);
            
            if (keys.Count > 0)
            {
                currentKey = keys[0];
                Debug.Log("Key : " + keys.Count.ToString());
            }
                
            else
                isFinish = true;
        }

    }


}
