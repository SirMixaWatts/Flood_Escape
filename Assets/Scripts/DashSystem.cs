using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashSystem : MonoBehaviour
{
    [Header("Dash Settings")]
    public float dashForce = 10f;
    public float doubleTapTime = 0.2f;
    public float dashRechargeTime = 3f;
    public int maxDashCharges = 3;

    [Header("UI Settings")]
    public GameObject boltContainer;
    public GameObject boltPrefab;
    public Sprite fullBolt;
    public Sprite emptyBolt;

    private Rigidbody2D rb;
    private float lastLeftTapTime = -999f;
    private float lastRightTapTime = -999f;
    private int currentDashCharges;

    private List<Image> bolts = new List<Image>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentDashCharges = maxDashCharges;
        InitializeBolts();
        UpdateBolts();
        InvokeRepeating(nameof(RechargeDash), dashRechargeTime, dashRechargeTime);
    }

    void Update()
    {
        HandleDoubleTapDash();
    }

    void HandleDoubleTapDash()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Time.time - lastLeftTapTime <= doubleTapTime && currentDashCharges > 0)
                Dash(Vector2.left);
            lastLeftTapTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Time.time - lastRightTapTime <= doubleTapTime && currentDashCharges > 0)
                Dash(Vector2.right);
            lastRightTapTime = Time.time;
        }
    }

    void Dash(Vector2 direction)
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // Optional: reset horizontal velocity
        rb.AddForce(direction * dashForce, ForceMode2D.Impulse);
        currentDashCharges--;
        UpdateBolts();
    }

    void RechargeDash()
    {
        if (currentDashCharges < maxDashCharges)
        {
            currentDashCharges++;
            UpdateBolts();
        }
    }

    public void InitializeBolts()
    {
        // Clear existing
        foreach (Transform child in boltContainer.transform)
        {
            Destroy(child.gameObject);
        }

        bolts.Clear();

        for (int i = 0; i < maxDashCharges; i++)
        {
            GameObject bolt = Instantiate(boltPrefab, boltContainer.transform);
            bolts.Add(bolt.GetComponent<Image>());
        }
    }

    public void UpdateBolts()
    {
        for (int i = 0; i < bolts.Count; i++)
        {
            bolts[i].sprite = i < currentDashCharges ? fullBolt : emptyBolt;
        }
    }
}
