using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    private Entity e;

    [SerializeField] private GameObject lifeBar;
    [SerializeField] Transform cursor;

    public GameObject inventory;
    public UnityEngine.UI.RawImage itemPreview;
    public Transform itemPreviewName;

    public RenderTexture cameraPreview;
    public Transform spawnPreview;

    [SerializeField] private Transform invGrid;
   

    private void Start()
    {
       e = transform.root.GetComponent<Entity>();
       itemPreview.texture = cameraPreview;
    }

    void UpdateLifeBar()
    {
        cursor = lifeBar.transform.Find("Cursor");
        cursor.GetComponent<RectTransform>().sizeDelta = new Vector2((lifeBar.GetComponent<RectTransform>().sizeDelta.x - 6) * e.life / e.maxLife , lifeBar.GetComponent<RectTransform>().sizeDelta.y - 6);
    }

    void UpdateInventory()
    {
        int count = 0;
        foreach(InventoryItem item in e.GetComponentInChildren<Container>().invs[0].items)
        {
            Transform current = invGrid.GetChild(count);

            current.GetComponent<InventoryCase>().item = item;


            count++;
        }

        // Update preview

        


    }

	void Update () {
        UpdateLifeBar();
        UpdateInventory();
        
	}




}
