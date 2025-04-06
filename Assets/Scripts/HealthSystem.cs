using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 4;
    public int currentHealth;

    [Header("UI Elements")]
    public GameObject heartContainer;
    public GameObject heartPrefab;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private List<Image> hearts = new List<Image>();

    void Start()
    {
        currentHealth = maxHealth;
        InitializeHearts();
        UpdateHearts();
    }

    public void InitializeHearts()
    {
        // Clear previous hearts if needed
        foreach (Transform child in heartContainer.transform)
        {
            Destroy(child.gameObject);
        }

        hearts.Clear();

        for (int i = 0; i < maxHealth; i++)
        {
            GameObject heartGO = Instantiate(heartPrefab, heartContainer.transform);
            Image heartImage = heartGO.GetComponent<Image>();
            hearts.Add(heartImage);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHearts();
        if(currentHealth <= 0)
        {
            FindObjectOfType<Flood>().TriggerGameOver();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHearts();
    }

    public void UpdateHearts()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Threat")
        {
            TakeDamage(1);
        }
    }
}
