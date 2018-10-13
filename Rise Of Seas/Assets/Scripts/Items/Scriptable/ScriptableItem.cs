using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "Game/Item", order = 1)]
public class ScriptableItem : ScriptableObject {

    public int id;
    public new string name;

    public GameObject item;

    public float weight;
    public Texture pic;

}
