using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    private Entity e;

    [SerializeField] private GameObject lifeBar;
    [SerializeField] Transform cursor;
    private void Start()
    {
       e = transform.root.GetComponent<Entity>();
    }

    void UpdateLifeBar()
    {
        cursor = lifeBar.transform.Find("Cursor");
        cursor.GetComponent<RectTransform>().sizeDelta = new Vector2((lifeBar.GetComponent<RectTransform>().sizeDelta.x - 6) * e.life / e.maxLife , lifeBar.GetComponent<RectTransform>().sizeDelta.y - 6);
    }

	void Update () {
        UpdateLifeBar();
	}




}
