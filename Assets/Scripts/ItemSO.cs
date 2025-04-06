using UnityEngine;

public enum ItemType
{
    SpeedBoost,
    JumpBoost,
    ItemMagnet,
    DoubleJump,
    DebuffSpeed,
    SlowFlood
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public float effectValue; // Could represent % or specific value (e.g., jump height)
    public float duration; // For effects that last a limited time
    public Sprite itemSprite;
}
