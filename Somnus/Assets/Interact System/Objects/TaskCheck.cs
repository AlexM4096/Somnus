using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskCheck : InteractableObject
{
    [SerializeField] private List<Task> tasks = new();
    [SerializeField] private string NextScene = "SampleScene";

    protected override void Awake()
    {
        base.Awake();

        foreach (var task in tasks)
            task.IsCompleted = false;
    }

    public override bool CanInteract()
    {
        return (tasks.All(t => t.IsCompleted) || tasks.Count == 0);
    }

    protected override void DoInteractions()
    {
        UITransitionChannel.TurnOn(NextScene);
    }
}
