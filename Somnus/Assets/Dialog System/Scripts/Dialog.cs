using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObject/Dialog")]
public class Dialog : ScriptableObject
{
    [SerializeField] string DialogName;
    [SerializeField] public string NPCText;
    [SerializeField] public List<DialogChoice> Choices;

    public void StartDialog()
    {
        TryFinishDialog();
        DialogChannel.StartDialog(this);
    }

    public void TryFinishDialog()
    {
        if (Choices.Count > 0) return;
        FinishDialog();
    }

    public void FinishDialog()
    {
        DialogChannel.FinishDialog(this);
    }
}
