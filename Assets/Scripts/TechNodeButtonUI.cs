using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TechNodeButtonUI : MonoBehaviour
{
    public TechNode techNode;
    public TextMeshProUGUI nameText;
    public Image background;

    public Color lockedColor = Color.gray;
    public Color unlockedColor = Color.green;

    public void Initialize(TechNode node)
    {
        techNode = node;
        nameText.text = node.nodeName;
        UpdateVisual();

        // Add click listener to select
        GetComponent<Button>().onClick.AddListener(() =>
        {
            FindObjectOfType<TechTooltip>().Show(techNode);
        });
    }

    private void Update()
    {
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        background.color = techNode.unlocked ? unlockedColor : lockedColor;
    }
}
