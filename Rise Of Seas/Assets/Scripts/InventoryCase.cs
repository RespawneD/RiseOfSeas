using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryCase : MonoBehaviour ,IPointerEnterHandler, IPointerExitHandler {

    public InventoryItem item;

    private UIManager manager;

    private void Start()
    {
        manager = transform.root.GetComponentInChildren<UIManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (item == null)
            return;

        Debug.Log(transform.root.name);

        GetComponent<UnityEngine.UI.RawImage>().color = Color.white;
        GameObject g = Instantiate(item.item.data.item, Vector3.zero, Quaternion.identity, manager.spawnPreview);
        g.transform.localPosition = Vector3.zero;
        Destroy(g.GetComponent<Collider>());
        Destroy(g.GetComponent<Rigidbody>());
        g.layer = LayerMask.NameToLayer("3DPreview");
        g.AddComponent<PreviewRotator>();
        manager.itemPreviewName.GetComponent<UnityEngine.UI.Text>().text = item.name;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item == null || manager.GetComponentInChildren<UIManager>().spawnPreview.childCount < 1)
            return;
        Destroy(manager.GetComponentInChildren<UIManager>().spawnPreview.GetChild(0).gameObject);
        transform.GetComponent<UnityEngine.UI.RawImage>().color = Color.black;
        manager.itemPreviewName.GetComponent<UnityEngine.UI.Text>().text = "";
    }

    public void OnDisable()
    {
        OnPointerExit(null);
    }
}
