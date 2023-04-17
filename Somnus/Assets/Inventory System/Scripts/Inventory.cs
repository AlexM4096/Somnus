using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> Items;

    private void Awake()
    {
        Items = new List<Item>();
    }
    private void OnEnable()
    {
        InventoryChannel.ItemAddEvent += AddItem;
        InventoryChannel.ItemRemoveEvent += RemoveItem;
    }
    private void OnDisable()
    {
        InventoryChannel.ItemAddEvent -= AddItem;
        InventoryChannel.ItemRemoveEvent -= RemoveItem;
    }
    private void AddItem(Item item)
    {
        Items.Add(item);
    }
    private void RemoveItem(Item item)
    {
        Items.Remove(item);
    }
}
