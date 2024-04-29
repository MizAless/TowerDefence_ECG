using UnityEngine;

public class TowerShootingConroller : MonoBehaviour
{
    [SerializeField] private float _shootInterval = 0.5f;
    [SerializeField] private float _range = 4f;

    [SerializeField] private ProjectilePool _projectilePool;

    [SerializeField] public Transform ShootPoint;

    [SerializeField] public GameObject ProjectilePrefab;

    private float _lastShotTime = -0.5f;

    public void TryShoot(Vector3 futurePosition)
    {
        if (IsReadyToShoot(futurePosition))
            Shoot(futurePosition);
    }

    private bool IsReadyToShoot(Vector3 futurePosition)
    {
        if (_lastShotTime + _shootInterval > Time.time)
            return false;

        Vector3 directionToFuturePosition = futurePosition - ShootPoint.position;

        Vector3 horizontalDirectionToFuturePosition = directionToFuturePosition;
        horizontalDirectionToFuturePosition.y = 0f;

        float correctAngleDeviation = 5f;

        return Vector3.Angle(transform.forward, horizontalDirectionToFuturePosition) < correctAngleDeviation;
    }

    private void Shoot(Vector3 futurePosition)
    {
        Vector3 shootDirection = futurePosition;

        if (ProjectilePrefab.GetComponent<CannonProjectile>().m_isPhysical)
            shootDirection += Vector3.up;

        ShootPoint.LookAt(shootDirection);

        _projectilePool.GetProjectile().GetComponent<CannonProjectile>().Initialize(ShootPoint.position, ShootPoint.rotation, _projectilePool);

        _lastShotTime = Time.time;
    }
}
