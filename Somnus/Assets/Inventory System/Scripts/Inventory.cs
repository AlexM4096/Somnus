using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> Items;
    private VisualElement Root;
    private void Awake()
    {
        Items = new List<Item>();
    }
    private void OnEnable()
    {
        InventoryChannel.ItemPickUpEvent += AddItem;
    }
    private void OnDisable()
    {
        InventoryChannel.ItemPickUpEvent -= AddItem;
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
