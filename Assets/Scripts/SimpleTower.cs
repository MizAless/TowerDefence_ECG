using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;

public class SimpleTower : MonoBehaviour
{
    [SerializeField] private float m_shootInterval = 0.5f;
    [SerializeField] private float m_range = 4f;
    [SerializeField] private GameObject m_projectilePrefab;

    private float m_lastShotTime = -0.5f;

    private Monster m_target;

    void Update()
    {
        if (m_projectilePrefab == null)
            return;

        if (m_lastShotTime + m_shootInterval > Time.time)
            return;

        FindMonster();

        if (m_target != null)
        {
            Shoot(m_target);
        }
    }

    private void FindMonster()
    {
        foreach (var monster in FindObjectsOfType<Monster>())
        {
            if (Vector3.Distance(transform.position, monster.transform.position) < m_range)
                m_target = monster;
        }
    }

    private void Shoot(Monster monster)
    {
        var projectile = Instantiate(m_projectilePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        var projectileBeh = projectile.GetComponent<GuidedProjectile>();
        projectileBeh.m_target = monster.gameObject;

        m_lastShotTime = Time.time;
    }
}
