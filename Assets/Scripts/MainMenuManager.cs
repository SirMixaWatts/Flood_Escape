using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject levelList;
    public GameObject levelListParent;

    public GameObject levelButtonPrefab;

    public GameObject menuButtons;

    private bool hasGeneratedButtons;

    private void Start()
    {
        menuButtons.SetActive(true);
        levelListParent.SetActive(false);
    }

    public void StartButton()
    {
        levelListParent.SetActive(true);
        menuButtons.SetActive(false);
        if (!hasGeneratedButtons)
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                if (i != 0)
                {
                    GameObject newLevelButton = Instantiate(levelButtonPrefab, levelList.transform.position, Quaternion.identity);
                    newLevelButton.transform.SetParent(levelList.transform, true);
                    newLevelButton.GetComponent<LevelButton>().levelIndexText.text = (i).ToString();
                    newLevelButton.GetComponent<LevelButton>().levelIndex = i;
                    newLevelButton.GetComponent<LevelButton>().Initialize();
                }
            }
            hasGeneratedButtons = true;
        }

    }

    public void BackButton()
    {
        menuButtons.SetActive(true);
        levelListParent.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
