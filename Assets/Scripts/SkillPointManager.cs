using TMPro;
using UnityEngine;

public class SkillPointManager : MonoBehaviour
{
    public static SkillPointManager Instance;
    public int skillPoints = 0;

    public TextMeshProUGUI skillPointText;

    private void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    }

    public void AddSkillPoint()
    {
        skillPoints++;
        Debug.Log("Skill Point Earned! Total: " + skillPoints);
    }

    public bool SpendSkillPoints(int amount)
    {
        if (skillPoints >= amount)
        {
            skillPoints -= amount;
            return true;
        }
        return false;
    }

    private void Update()
    {
        skillPointText.text = $"Skill Points Available: {skillPoints} ";
    }
}
