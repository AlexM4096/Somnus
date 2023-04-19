using System.Collections.Generic;
using UnityEngine;

public class NPC : InteractableObject
{
    [SerializeField] string Name;
    [SerializeField] List<Dialog> Dialogs;

    Queue<Dialog> DialogsQueue;

    protected override void Awake()       
    {
        base.Awake();
        DialogsQueue = new Queue<Dialog>(Dialogs);
    }
 
    public override bool CanInteract() { return DialogsQueue.Count > 0; }
    public override void StartInteract()
    { 
        base.StartInteract();
        if (!CanInteract()) return;
        StartDialog(); 
    }
    void StartDialog()
    {
        Dialog dialog = DialogsQueue?.Dequeue();
        dialog.StartDialog();
    }
}
