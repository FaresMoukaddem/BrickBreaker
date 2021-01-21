using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Assets/Level")]
public class LevelScriptableObject : ScriptableObject
{
    public int rows, columns, lives, speed;
}
