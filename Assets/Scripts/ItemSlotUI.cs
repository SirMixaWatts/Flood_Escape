using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Image itemImage; // Image to display the item's icon
    public TextMeshProUGUI itemNameText; // Text to display the item name
    public TextMeshProUGUI itemQuantityText; // Text to display the item quantity

    // Set up the item UI slot with an item and its quantity
    public void SetItem(ItemSO item, int quantity)
    {
        // Set the item's icon (assuming your Item class has a Sprite for the icon)
        itemImage.sprite = item.itemSprite;

        // Set the name and quantity text
        itemNameText.text = item.itemName;
        itemQuantityText.text = "x" + quantity.ToString();
    }
}
