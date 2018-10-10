using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour {

    private Entity e;

    [SerializeField]
    private RectTransform lifeBar;
    private RectTransform cursor;

    private void Start()
    {
        e = transform.root.GetComponent<Entity>();
        cursor = lifeBar.GetChild(0).GetComponent<RectTransform>();

        cursor.sizeDelta = lifeBar.sizeDelta;

    }

    // Update is called once per frame
    void Update () {
        transform.LookAt(Camera.main.transform);

        cursor.sizeDelta = new Vector2(lifeBar.sizeDelta.x * e.life / e.maxLife, lifeBar.sizeDelta.y);

	}
}
