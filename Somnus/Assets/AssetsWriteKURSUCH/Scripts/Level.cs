using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Game Objects/Level")]
public class Level : ScriptableObject
{
    public string levelName;
    public string levelText;
    public Difficulty difficulty;
}
