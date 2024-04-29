using UnityEngine;

public class GuidedTowerShootingController : MonoBehaviour
{
    [SerializeField] private float m_shootInterval = 0.5f;
    
    [SerializeField] private GameObject m_projectilePrefab;

    [SerializeField] private ProjectilePool _projectilePool;

    [SerializeField] public float Range = 4f;

    private float m_lastShotTime = -0.5f;

    public void TryShoot(Monster monster)
    {
        if (monster == null || m_lastShotTime + m_shootInterval > Time.time)
            return;

        Shoot(monster);
    }

    private void Shoot(Monster monster)
    {
        Vector3 shootPoint = transform.position + Vector3.up * 1.5f;

        _projectilePool.GetProjectile().GetComponent<GuidedProjectile>().Initialize(shootPoint, Quaternion.identity, _projectilePool, monster.gameObject);

        m_lastShotTime = Time.time;
    }
}