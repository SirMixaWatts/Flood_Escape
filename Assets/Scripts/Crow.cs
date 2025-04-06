using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    public List<Transform> patrolPoints;
    public float speed = 2f;

    private int currentTargetIndex = 0;

    void Update()
    {
        if (patrolPoints == null || patrolPoints.Count == 0) return;

        Transform targetPoint = patrolPoints[currentTargetIndex];
        Vector3 direction = (targetPoint.position - transform.position).normalized;

        // Move the crow toward the target point
        transform.position += direction * speed * Time.deltaTime;

        // Check if the crow is close enough to the target to switch to the next
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentTargetIndex = (currentTargetIndex + 1) % patrolPoints.Count;
        }
    }
}
