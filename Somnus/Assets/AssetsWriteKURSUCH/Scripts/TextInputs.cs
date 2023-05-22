using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Text Input", menuName = "Game Presets/Text Inputs")]
public class TextInputs : ScriptableObject
{
    public StringInputs[] strings;
    public string languageConvert;
}

[System.Serializable]
public class StringInputs
{
    public string text;
    public string chars;
}