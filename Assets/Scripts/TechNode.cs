using UnityEngine;

public enum TechEffectType
{
    None,
    MoveSpeed,
    JumpForce,
    DashCharges,
    MaxHealth,
    DashRecharge,
    ActivateDoubleJump
}

[CreateAssetMenu(fileName = "NewTechNode", menuName = "Tech Tree/Tech Node")]
public class TechNode : ScriptableObject
{
    public string nodeName;
    public string description;
    public int cost = 1;
    public bool unlocked = false;
    public Vector2 uiPosition;

    public TechNode[] prerequisites;

    [Header("Effect")]
    public TechEffectType effectType;
    public float effectValue; // e.g., +0.1f move speed, +1 health, etc.
}
