using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;


public class DamageIndicatorItem : MonoBehaviour {

    

    public int damage;
    public float speed;
    public Color c;
    TextMesh p;

    private float alpha;

    private void Start()
    {
        Destroy(gameObject, 3f);
        p = GetComponent<TextMesh>();
        p.text = damage.ToString();
        alpha = c.a;
    }

        

    void Update () {

        transform.position += Vector3.up * speed * Time.deltaTime;
        alpha -= .5f * speed * Time.deltaTime;
        c = new Color(c.r, c.g, c.b, alpha);
        p.color = c;
	}
}
