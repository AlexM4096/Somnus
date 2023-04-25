using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> Items;

    private void Awake()
    {
        Items = new();
    }

    private void OnEnable()
    {
        InventoryChannel.ItemAddEvent += AddItem;
        InventoryChannel.ItemRemoveEvent += RemoveItem;
        InventoryChannel.ItemUseEvent += UseItem;
    }

    private void OnDisable()
    {
        InventoryChannel.ItemAddEvent -= AddItem;
        InventoryChannel.ItemRemoveEvent -= RemoveItem;
        InventoryChannel.ItemUseEvent -= UseItem;
    }

    private void AddItem(Item item)
    {
        Items.Add(item);
    }

    private void RemoveItem(Item item)
    {
        Items.Remove(item);
    }

    private void UseItem(Item item)
    {
        if (Items.Contains(item))
            RemoveItem(item);
    }
}
