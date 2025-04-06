using UnityEngine;
using UnityEngine.SceneManagement; // For restarting the level
using UnityEngine.UI; // If you're using UI for game over

public class Flood : MonoBehaviour
{
    public float riseSpeed = 1f;
    public Transform player;
    public Transform playerHead;
    public GameObject gameOverMenu;

    private bool gameOverTriggered = false;

    void Update()
    {
        // Make the flood rise
        transform.position += Vector3.up * riseSpeed * Time.deltaTime;

        // Check for collision with player's head
        if (!gameOverTriggered && GetComponent<BoxCollider2D>().bounds.max.y >= playerHead.position.y)
        {
            TriggerGameOver(); 
        }
    }

    public void TriggerGameOver()
    {
        gameOverTriggered = true;
        if (gameOverMenu != null)
            gameOverMenu.SetActive(true);
        else
            Debug.LogWarning("Game Over Menu not assigned!");
        Time.timeScale = 0f;
    }

    // Optional: Restart method for button hook-up
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (gameOverMenu != null)
            gameOverMenu.SetActive(false);
    }
}
