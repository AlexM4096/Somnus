using System;
using UnityEngine;

[Serializable]
public class DialogChoice
{
    [SerializeField] public string PlayerText;
    [SerializeField] ChoiceType Type;
    [SerializeField] public Dialog NextDialog;
}
