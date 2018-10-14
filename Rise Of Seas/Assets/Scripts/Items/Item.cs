using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour {

    public ScriptableItem data;

    public CraftList craftlist;

    public bool isOnGround;

    [SerializeField] private GameObject itemInfoPopup;

    private GameObject currentPopup;

    public void DisplayItemInfos()
    {
        GetComponent<Outline>().enabled = true;
        currentPopup = Instantiate(itemInfoPopup, Vector3.zero, Quaternion.identity, transform);
        currentPopup.GetComponent<RectTransform>().localPosition = Vector3.zero;
        currentPopup.transform.Find("ItemName").GetComponent<TextMesh>().text = data.name;
    }

    public void HideItemInfos()
    {
        GetComponent<Outline>().enabled = false;
        Destroy(currentPopup);
        currentPopup = null;
    }


    public void Update()
    {
        if(currentPopup != null)
            currentPopup.transform.LookAt(Camera.main.transform);

    }

}
