﻿using UnityEngine;
using System.Collections;

public class GuidedProjectile : MonoBehaviour
{
    public GameObject m_target;

    [SerializeField] private float m_speed = 0.2f;
    [SerializeField] private int m_damage = 10;

    //private float _startTime;

    private ProjectilePool _pool;

    void Update()
    {
        if (m_target == null)
        {
            ReturnToPool();
            return;
        }

        var translation = m_target.transform.position - transform.position;

        if (translation.magnitude > m_speed)
        {
            translation = translation.normalized * m_speed;
        }

        transform.Translate(translation);
    }

    public void Initialize(Vector3 position, Quaternion rotation, ProjectilePool pool, GameObject target)
    {
        transform.position = position;
        transform.rotation = rotation;
        _pool = pool;
        m_target = target;
        //_startTime = Time.time;
    }

    private void ReturnToPool()
    {
        _pool.ReturnProjectile(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        var monster = other.gameObject.GetComponent<Monster>();

        if (monster == null)
            return;

        monster.TakeDamage(m_damage);

        ReturnToPool();
    }
}
