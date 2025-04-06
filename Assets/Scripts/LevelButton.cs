using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public TextMeshProUGUI levelIndexText;
    public Button startButton;
    [HideInInspector] public int levelIndex;

    public void Initialize()
    {
        // Add click listener to select
        startButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            GoToLevel();
        });
    }

    public void GoToLevel()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
