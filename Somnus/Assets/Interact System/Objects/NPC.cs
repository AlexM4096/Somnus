using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC : InteractableObject
{
    [SerializeField] List<Dialog> Dialogs;
    private Dialog CurrentDialog;

    public override bool CanInteract() { return Dialogs.Count > 0; }

    private void StartTalk()
    {
        CurrentDialog = Dialogs.First();
        CurrentDialog.Start();
    }

    protected override void DoInteractions()
    {
        StartTalk();
    }
}
