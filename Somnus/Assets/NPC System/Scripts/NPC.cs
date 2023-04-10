using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] string Name;
    [SerializeField] Sprite Sprite;
    [SerializeField] List<Dialog> Dialogs;

    Queue<Dialog> DialogsQueue;
    DialogChannel _dialogChannel;

    private void Awake()
    {
        DialogsQueue = new Queue<Dialog>();
    }

    void StartDialog()
    {

        Dialog dialog = DialogsQueue.Dequeue();
        _dialogChannel.StartDialog(dialog);
    }

    public bool HaveDialog() { return DialogsQueue.Count > 0; }
}
