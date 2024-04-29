using UnityEngine;

public class TargetPredictionConroller : MonoBehaviour
{
    public Monster Target;

    public Vector3 FuturePosition;

    public void TryCalculateTargetPosition(Vector3 shootPoint, float projectileSpeed)
    {
        FindTarget();

        if (Target == null)
            return;

        CalculateFuturePosition(shootPoint, projectileSpeed);
    }

    private void FindTarget()
    {
        Monster[] monsters = FindObjectsOfType<Monster>();
        float closestDistance = Mathf.Infinity;

        foreach (var monster in monsters)
        {
            float distance = Vector3.Distance(transform.position, monster.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                Target = monster;
            }
        }
    }

    private void CalculateFuturePosition(Vector3 shootPoint, float projectileSpeed)
    {
        Vector3 targetPosition = Target.transform.position;
        float timeCorrectCoefficient = 0.7f;
        float timeToTarget = Vector3.Distance(shootPoint, targetPosition) / projectileSpeed * timeCorrectCoefficient;
        FuturePosition = targetPosition + Target.m_direction * timeToTarget * Target.M_speed;
    }

    private void OnDrawGizmos()
    {
        float sphereRadius = 0.5f;
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(FuturePosition, sphereRadius);
    }
}