using UnityEngine;

public class Chest : MonoBehaviour
{
    public ItemManager itemManager; // Reference to the ItemManager
    private bool isNearChest = false;

    void Update()
    {
        // Check if the player is near the chest and presses the interaction button (e.g., "E" key)
        if (isNearChest && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearChest = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearChest = false;
        }
    }

    void OpenChest()
    {
        ItemSO randomItem = itemManager.GetRandomItem();
        Debug.Log("You got: " + randomItem.itemName);

        // Now apply the item effect to the player
        ApplyItemEffect(randomItem);
    }

    void ApplyItemEffect(ItemSO item)
    {
        PlayerController playerController = FindObjectOfType<PlayerController>(); // Find the player in the scene

        itemManager.AddItem(item);
    }
}
