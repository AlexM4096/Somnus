using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObject/Dialog")]
public class Dialog : ScriptableObject
{
    [SerializeField] string DialogName;
    [SerializeField] public string NPCText;
    [SerializeField] public List<DialogChoice> Choices;

    public void Start() { DialogChannel.StartDialog(this); }
    public void Finish() { DialogChannel.FinishDialog(this); }
}
