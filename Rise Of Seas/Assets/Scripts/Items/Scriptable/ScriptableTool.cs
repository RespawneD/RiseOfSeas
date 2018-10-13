using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType
{
    Axe = 0,
    Pickaxe = 1
}

[CreateAssetMenu(fileName = "Tool", menuName = "Game/Tool", order = 1)]
public class ScriptableTool : ScriptableWeapon
{
    public ToolType type;
}

