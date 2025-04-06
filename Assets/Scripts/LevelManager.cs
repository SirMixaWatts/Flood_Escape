using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject essentials;
    public Transform startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!FindAnyObjectByType<DontDestroyOnNewScene>())
        {
            Instantiate(essentials, startPos.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
