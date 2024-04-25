using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
    const float m_reachDistance = 0.3f;

    [SerializeField] private int m_maxHP = 30;

    [SerializeField] public float M_speed = 10f;
    [SerializeField] public GameObject M_moveTarget;

    private int m_hp;

    public Vector3 m_direction;

    void Start()
    {
        m_hp = m_maxHP;
    }

    void Update()
    {
        if (M_moveTarget == null)
            return;

        if (DestroyIfCompletePath())
            return;

        Move();
    }

    public void TakeDamage(int damage)
    {
        m_hp -= damage;

        if (m_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        var translation = M_moveTarget.transform.position - transform.position;

        if (translation.magnitude > M_speed)
        {
            m_direction = translation.normalized;
            translation = m_direction * M_speed * Time.deltaTime;
        }

        transform.Translate(translation);
    }

    private bool DestroyIfCompletePath()
    {
        if (Vector3.Distance(transform.position, M_moveTarget.transform.position) <= m_reachDistance)
        {
            Destroy(gameObject);
            return true;
        }

        return false;
    }
}
