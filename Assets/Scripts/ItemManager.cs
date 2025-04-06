using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<ItemSO> inventory = new List<ItemSO>();
    public List<ItemSO> availableItems; // List of all possible items

    public ItemShowcaser showcaser;

    private PlayerController playerController;
    private Flood flood;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        flood = FindObjectOfType<Flood>();
    }

    // Add item to the player's inventory
    public void AddItem(ItemSO item)
    {
        inventory.Add(item);
        ApplyItemEffect(item);
        showcaser.AcquireItem(item);
    }

    // Apply the effect of a specific item
    public void ApplyItemEffect(ItemSO item)
    {
        switch (item.itemType)
        {
            case ItemType.SpeedBoost:
                ApplySpeedBoost(item);
                break;
            case ItemType.JumpBoost:
                ApplyJumpBoost(item);
                break;
            case ItemType.ItemMagnet:
                ApplyItemMagnet(item);
                break;
            case ItemType.DoubleJump:
                ApplyDoubleJump(item);
                break;
            case ItemType.DebuffSpeed:
                ApplyDebuffSpeed(item);
                break;
            case ItemType.SlowFlood:
                ApplySlowFlood(item);
                break;
        }
    }

    // Implement specific effects
    private void ApplySpeedBoost(ItemSO item)
    {
        playerController.moveSpeed *= (1 + item.effectValue);
    }

    private void ApplyJumpBoost(ItemSO item)
    {
        playerController.jumpForce += item.effectValue;
    }

    private void ApplyItemMagnet(ItemSO item)
    {
        // Implement magnet effect: Attract items towards player
        // This would need a separate system for items to get attracted to the player
    }

    private void ApplyDoubleJump(ItemSO item)
    {
        if (!playerController.hasDoubleJump)
        {
            playerController.doubleJumpCooldown = item.duration;
            playerController.hasDoubleJump = true;
        }
    }

    private IEnumerator ApplyDebuffSpeed(ItemSO item)
    {
        float originalSpeed = playerController.moveSpeed;
        playerController.moveSpeed /= (1 + item.effectValue);

        yield return new WaitForSeconds(item.duration);

        playerController.moveSpeed = originalSpeed;
        
    }

    private void ApplySlowFlood(ItemSO item)
    {
        StartCoroutine(SlowFloodEffect(item));
    }

    private IEnumerator SlowFloodEffect(ItemSO item)
    {
        float originalSpeed = flood.riseSpeed;
        flood.riseSpeed /= (1 + item.effectValue);

        yield return new WaitForSeconds(item.duration);

        flood.riseSpeed = originalSpeed;
    }

    // Get a random item from the list
    public ItemSO GetRandomItem()
    {
        int randomIndex = Random.Range(0, availableItems.Count);
        return availableItems[randomIndex];
    }
}
