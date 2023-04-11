using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] string Name;
    [SerializeField] List<Dialog> Dialogs;

    Queue<Dialog> DialogsQueue;

    private void Awake()
    {
        DialogsQueue = new Queue<Dialog>(Dialogs);
    }

    void StartDialog()
    {
        Dialog dialog = DialogsQueue.Dequeue();
        dialog?.StartDialog();
    }
 
    public bool CanInteract() { return DialogsQueue.Count > 0; }
    public void Interact() { StartDialog(); }
}
