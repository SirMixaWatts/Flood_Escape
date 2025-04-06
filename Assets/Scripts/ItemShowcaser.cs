using UnityEngine;
using System.Collections.Generic;

public class ItemShowcaser : MonoBehaviour
{
    private Dictionary<ItemSO, int> acquiredItems = new Dictionary<ItemSO, int>();
    private Dictionary<ItemSO, ItemSlotUI> itemSlotCache = new Dictionary<ItemSO, ItemSlotUI>();

    [Header("UI References")]
    public Transform itemsGrid;
    public GameObject itemSlotPrefab;

    public void AcquireItem(ItemSO newItem)
    {
        // Update item quantity
        if (acquiredItems.ContainsKey(newItem))
        {
            acquiredItems[newItem]++;
            itemSlotCache[newItem].SetItem(newItem, acquiredItems[newItem]);
        }
        else
        {
            acquiredItems[newItem] = 1;

            // Create new UI element
            GameObject itemSlot = Instantiate(itemSlotPrefab, itemsGrid);
            ItemSlotUI slotUI = itemSlot.GetComponent<ItemSlotUI>();
            slotUI.SetItem(newItem, 1);

            itemSlotCache[newItem] = slotUI;
        }
    }
}
