using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObject/Dialog")]
public class Dialog : ScriptableObject
{
    [SerializeField] string DialogName;
    [SerializeField] string NPCText;
    [SerializeField] List<DialogChoice> Choices;
}
