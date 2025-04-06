using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class TechTreeUIManager : MonoBehaviour
{
    public RectTransform techTreeArea;
    public GameObject nodeButtonPrefab;
    public TechNode[] techNodes;

    private Dictionary<TechNode, TechNodeButtonUI> buttons = new Dictionary<TechNode, TechNodeButtonUI>();

    public GameObject linePrefab;
    private Dictionary<TechNode, RectTransform> nodePositions = new Dictionary<TechNode, RectTransform>();

    void Start()
    {
        GenerateTechTree();
    }

    void GenerateTechTree()
    {
        int index = 0;

        foreach (TechNode node in techNodes)
        {
            GameObject buttonObj = Instantiate(nodeButtonPrefab, techTreeArea);
            RectTransform buttonRect = buttonObj.GetComponent<RectTransform>();
            buttonRect.anchoredPosition = node.uiPosition;

            TechNodeButtonUI buttonUI = buttonObj.GetComponent<TechNodeButtonUI>();
            buttonUI.Initialize(node);

            buttons[node] = buttonUI;
            nodePositions[node] = buttonRect;

            index++;
        }

        DrawAllLines();
    }

    void DrawAllLines()
    {
        foreach (var node in techNodes)
        {
            foreach (var prereq in node.prerequisites)
            {
                if (nodePositions.ContainsKey(node) && nodePositions.ContainsKey(prereq))
                {
                    DrawLine(nodePositions[prereq], nodePositions[node]);
                }
            }
        }
    }

    void DrawLine(RectTransform from, RectTransform to)
    {
        GameObject lineObj = Instantiate(linePrefab, techTreeArea);
        RectTransform lineRect = lineObj.GetComponent<RectTransform>();

        Vector2 startPos = from.anchoredPosition;
        Vector2 endPos = to.anchoredPosition;
        Vector2 difference = endPos - startPos;

        lineRect.sizeDelta = new Vector2(difference.magnitude, 4f); // Line width = 4
        lineRect.pivot = new Vector2(0, 0.5f);
        lineRect.anchoredPosition = startPos;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        lineRect.rotation = Quaternion.Euler(0, 0, angle);

        lineObj.transform.parent = techTreeArea.GetChild(0).transform;
    }

}
