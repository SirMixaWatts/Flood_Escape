using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    public GameObject levelCompleteScreen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Show complete screen
            levelCompleteScreen.SetActive(true);

            // Pause game
            Time.timeScale = 0f;

            // Award skill point
            SkillPointManager.Instance.AddSkillPoint();
        }
    }

    public void ContinueToNextLevel(string nextSceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        levelCompleteScreen.SetActive(false);
    }
}
