using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasCommands : MonoBehaviour
{
    public GameObject levelListParent;

    public GameObject levelButtonPrefab;

    public void StartButton()
    {
        foreach (Scene level in SceneManager.GetAllScenes())
        {
            if (level.buildIndex != 0)
            {
                GameObject newLevelButton = Instantiate(levelButtonPrefab, levelListParent.transform.position, Quaternion.identity);
                newLevelButton.transform.SetParent(levelListParent.transform, true);
                newLevelButton.GetComponent<LevelButton>().levelIndexText.text = (level.buildIndex).ToString();
                newLevelButton.GetComponent<LevelButton>().levelIndex = level.buildIndex;
            }
        }
    }
}
