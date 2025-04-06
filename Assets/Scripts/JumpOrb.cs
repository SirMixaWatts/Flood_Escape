using UnityEngine;

public class JumpOrb : MonoBehaviour
{
    public JumpOrbSO orbData;

    private bool playerInRange = false;
    private PlayerController player;

    void Update()
    {
        if (playerInRange && Input.GetButtonDown("Jump"))
        {
            player.TriggerOrbJump(orbData.jumpMultiplier);
            playerInRange = false; // prevent re-triggering until player exits/re-enters
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerController>();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (orbData != null)
        {
            Gizmos.color = orbData.orbColor;
            Gizmos.DrawSphere(transform.position, 0.2f);
        }
    }
}
