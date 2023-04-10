using System;
using UnityEngine;

[Serializable]
public class DialogChoice
{
    [SerializeField] string PlayerText;
    [SerializeField] ChoiceType Type;
    [SerializeField] Dialog NextDialog;
}
