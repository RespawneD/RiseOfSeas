using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryCase : MonoBehaviour ,IPointerEnterHandler, IPointerExitHandler {

    public InventoryItem item;

	public void OnPointerEnter(PointerEventData eventData)
    {

        if (item == null)
            return;

        Debug.Log(transform.root.name);

        GetComponent<UnityEngine.UI.RawImage>().color = Color.white;
        GameObject g = Instantiate(item.item.data.item, transform.position, Quaternion.identity, transform.root.GetComponentInChildren<UIManager>().itemPreview);
        Destroy(g.GetComponent<Collider>());
        Destroy(g.GetComponent<Rigidbody>());
        transform.root.GetComponentInChildren<UIManager>().itemPreviewName.GetComponent<UnityEngine.UI.Text>().text = item.name;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item == null)
            return;
        GetComponent<UnityEngine.UI.RawImage>().color = Color.black;
    }
}
