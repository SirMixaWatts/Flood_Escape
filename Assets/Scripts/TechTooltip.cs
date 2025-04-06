using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TechTooltip : MonoBehaviour
{
    public TextMeshProUGUI techNameText;
    public TextMeshProUGUI techDescText;
    public TextMeshProUGUI techCostText;
    public Button unlockButton;

    private TechNode currentNode;

    public void Show(TechNode node)
    {
        currentNode = node;

        gameObject.SetActive(true);
        techNameText.text = node.nodeName;
        techDescText.text = node.description;
        techCostText.text = $"Cost: {node.cost}";

        bool canUnlock = !node.unlocked &&
                         SkillPointManager.Instance.skillPoints >= node.cost &&
                         TechTreeManager.Instance.ArePrerequisitesMet(node);

        unlockButton.interactable = canUnlock;

        unlockButton.onClick.RemoveAllListeners();
        unlockButton.onClick.AddListener(() =>
        {
            TechTreeManager.Instance.UnlockNode(currentNode);
            Show(currentNode); // Refresh tooltip
        });
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
