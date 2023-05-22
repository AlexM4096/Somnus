using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "ScriptableObject/Task")]
public class Task : ScriptableObject
{
    public bool IsCompleted = false;
    public List<GameObject> ObjectList;

    private List<InteractableObject> Interactables;
    private int Count;

    [ContextMenu("Update")]
    private void Update()
    {
        Interactables.Clear();
        foreach (var _object in ObjectList)
        {
            if (_object.TryGetComponent(out  InteractableObject interactable))
                Interactables.Add(interactable);
        }
        Count = Interactables.Count;
    }

    private void OnEnable()
    {
        InteractChannel.InteractFinishEvent += CheckInteractable;
    }

    private void OnDisable()
    {
        InteractChannel.InteractFinishEvent -= CheckInteractable;
    }

    private void CheckInteractable(InteractableObject interactable)
    {
        foreach (var a in Interactables) 
        {
            if (a.Equals(interactable)) continue;           
            Count--;
        }
        if (Count <= 0) IsCompleted = true;
    }
}
