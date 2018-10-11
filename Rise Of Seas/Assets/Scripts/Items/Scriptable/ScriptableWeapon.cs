using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Game/Weapon", order = 2)]
public class ScriptableWeapon : ScriptableItem {

    public AudioClip drawSound;
    public AudioClip sheatSound;

    public List<AudioClip> actionsSound;

    public float damage;

}
