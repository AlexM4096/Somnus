using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    [SerializeField] private bool IsOpened;
    [SerializeField] private Item Key;

    protected override void Awake()
    {
        base.Awake();
        IsOpened = false;
    }
    public override bool CanInteract() { return !IsOpened; }
    public override void StartInteract()
    {
        base.StartInteract();
        if (IsOpened) return;
        InventoryChannel.UseItem(Key);
        if (Key.Used) gameObject.SetActive(false);
    }
    public override void FinishInteract()
    {
        base.FinishInteract();
        IsOpened = false;
    }
}
