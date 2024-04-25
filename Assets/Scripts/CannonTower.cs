using UnityEngine;
using System.Collections;

public class CannonTower : MonoBehaviour
{
    [SerializeField] private float m_shootInterval = 0.5f;
    [SerializeField] private float m_range = 4f;
    [SerializeField] private float m_rotationSpeed = 30f;
    [SerializeField] private float m_lastShotTime = -0.5f;

    [SerializeField] private GameObject m_projectilePrefab;

    [SerializeField] private Transform m_shootPoint;

    private Monster m_target;

    private Vector3 futurePosition;

    void Update()
    {
        if (m_projectilePrefab == null || m_shootPoint == null)
            return;

        FindTarget();

        if (m_target != null)
        {
            CalculateFuturePosition();
            SmoothRotateTowards();

            if (IsReadyToShoot())
                Shoot();
        }
    }

    private void CalculateFuturePosition()
    {
        Vector3 targetPosition = m_target.transform.position;
        float timeCorrectCoefficient = 0.7f;
        float timeToTarget = Vector3.Distance(m_shootPoint.position, targetPosition) / m_projectilePrefab.GetComponent<CannonProjectile>().m_speed * timeCorrectCoefficient;
        futurePosition = targetPosition + m_target.m_direction * timeToTarget * m_target.M_speed;
    }

    private void SmoothRotateTowards()
    {
        Vector3 directionToFuturePosition = futurePosition - m_shootPoint.position;
        Quaternion rotationToFuturePosition = Quaternion.LookRotation(directionToFuturePosition);
        Vector3 euler = rotationToFuturePosition.eulerAngles;

        Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, euler.y, transform.rotation.eulerAngles.z);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, m_rotationSpeed * Time.deltaTime);
    }

    private bool IsReadyToShoot() 
    {
        if (m_lastShotTime + m_shootInterval > Time.time)
            return false;

        Vector3 directionToFuturePosition = futurePosition - m_shootPoint.position;

        Vector3 horizontalDirectionToFuturePosition = directionToFuturePosition;
        horizontalDirectionToFuturePosition.y = 0f;

        float correctAngleDeviation = 5f;

        return Vector3.Angle(transform.forward, horizontalDirectionToFuturePosition) < correctAngleDeviation;
    }

    private void Shoot()
    {
        Vector3 shootDirection = futurePosition;

        if (m_projectilePrefab.GetComponent<CannonProjectile>().m_isPhysical)
            shootDirection += Vector3.up;

        m_shootPoint.LookAt(shootDirection);
        Instantiate(m_projectilePrefab, m_shootPoint.position, m_shootPoint.rotation);
        m_lastShotTime = Time.time;
    }

    private void OnDrawGizmos()
    {
        float sphereRadius = 0.5f;
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(futurePosition, sphereRadius);
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
                m_target = monster;
            }
        }
    }
}