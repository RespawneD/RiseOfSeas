using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformsShortcut : MonoBehaviour {

    [System.Serializable]
    public class TransformShortcutItem
    {
        public string id;
        public Transform t;
    }

    public List<TransformShortcutItem> shortcuts;

    public Transform GetItem(string key)
    {
        foreach (TransformShortcutItem i in shortcuts)
            if (i.id == key)
                return i.t;
        return null;
    }


}



