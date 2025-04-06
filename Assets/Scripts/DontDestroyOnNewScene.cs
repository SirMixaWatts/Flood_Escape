using UnityEngine;

public class DontDestroyOnNewScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
