using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEndTrigger : MonoBehaviour
{
    public GameObject levelCompleteScreen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (levelCompleteScreen == null)
        {
            levelCompleteScreen = FindAnyObjectByType<Canvas>().transform.GetChild(1).transform.gameObject;
        }
        if (other.CompareTag("Player"))
        {
            // Show complete screen
            levelCompleteScreen.SetActive(true);

            // Award skill point
            SkillPointManager.Instance.AddSkillPoint();

            levelCompleteScreen.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
            levelCompleteScreen.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
            {
                ContinueToNextLevel();
            }); 

            // Pause game
            Time.timeScale = 0f;
        }
    }

    public void ContinueToNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        levelCompleteScreen.SetActive(false);
    }
}
