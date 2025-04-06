using UnityEngine;

[CreateAssetMenu(fileName = "NewJumpOrb", menuName = "JumpOrb")]
public class JumpOrbSO : ScriptableObject
{
    public float jumpMultiplier = 1.5f;
    public Color orbColor = Color.yellow;
}
