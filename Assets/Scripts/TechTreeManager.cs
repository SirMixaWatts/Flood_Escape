using UnityEngine;
using UnityEngine.UI;

public class TechTreeManager : MonoBehaviour
{
    public static TechTreeManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public bool ArePrerequisitesMet(TechNode node)
    {
        foreach (var pre in node.prerequisites)
        {
            if (!pre.unlocked) return false;
        }
        return true;
    }

    public TechNode[] allNodes;

    public PlayerController player;
    public HealthSystem healthSystem;
    public DashSystem dashSystem;

    public void UnlockNode(TechNode node)
    {
        if (node.unlocked)
        {
            Debug.Log($"{node.nodeName} already unlocked.");
            return;
        }

        // Check prerequisites
        foreach (var pre in node.prerequisites)
        {
            if (!pre.unlocked)
            {
                Debug.Log($"Can't unlock {node.nodeName}, missing prerequisite: {pre.nodeName}");
                return;
            }
        }

        if (SkillPointManager.Instance.SpendSkillPoints(node.cost))
        {
            node.unlocked = true;
            ApplyTech(node);
            
            Debug.Log($"Unlocked {node.nodeName}!");
        }
        else
        {
            Debug.Log("Not enough skill points!");
        }
    }

    void ApplyTech(TechNode node)
    {
        switch (node.effectType)
        {
            case TechEffectType.MoveSpeed:
                player.moveSpeed += node.effectValue;
                break;

            case TechEffectType.JumpForce:
                player.jumpForce += node.effectValue;
                break;

            case TechEffectType.MaxHealth:
                healthSystem.maxHealth += Mathf.RoundToInt(node.effectValue);
                healthSystem.currentHealth = healthSystem.maxHealth;
                healthSystem.InitializeHearts();
                healthSystem.UpdateHearts();
                break;

            case TechEffectType.DashCharges:
                dashSystem.maxDashCharges += Mathf.RoundToInt(node.effectValue);

                dashSystem.InitializeBolts();
                dashSystem.UpdateBolts();

                break;
            case TechEffectType.DashRecharge:
                dashSystem.dashRechargeTime -= node.effectValue;
                break;
            case TechEffectType.ActivateDoubleJump:
                player.hasDoubleJump = true;
                break;
            case TechEffectType.None:
            default:
                Debug.LogWarning($"TechNode '{node.nodeName}' has no effect assigned.");
                break;
        }
    }


}
