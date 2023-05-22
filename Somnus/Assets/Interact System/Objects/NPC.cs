using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC : InteractableObject
{
    [SerializeField] private List<Dialog> Dialogs;
    private Queue<Dialog> DialogsQueue;
    private Dialog CurrentDialog;

    public object DialogQueue { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        DialogsQueue = new(Dialogs);
    }

    private void OnEnable()
    {
        DialogChannel.DialogUnFinishEvent += TryStopTalking;
    }

    private void OnDisable()
    {
        DialogChannel.DialogUnFinishEvent -= TryStopTalking;
    }

    public override bool CanInteract() { return DialogsQueue.Count > 0; }

    private void StartTalking()
    {
        //CurrentDialog = Dialogs.First();
        CurrentDialog = DialogsQueue.Dequeue();
        CurrentDialog.Start();
    }

    private void TryStopTalking(Dialog dialog)
    {
        if (dialog != CurrentDialog) return;
        Dialogs.Remove(dialog);
    }

    protected override void DoInteractions()
    {
        StartTalking();
    }
}
