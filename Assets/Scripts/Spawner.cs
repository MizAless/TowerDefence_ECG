using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float m_interval = 3;
    [SerializeField] private GameObject m_moveTarget;

    private float m_lastSpawn = -1;

    void Update()
    {
        if (Time.time > m_lastSpawn + m_interval)
        {
            SpawnMonster();
        }
    }

    private void SpawnMonster()
    {
        var newMonster = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        var rigidbody = newMonster.AddComponent<Rigidbody>();

        rigidbody.useGravity = false;
        newMonster.transform.position = transform.position;
        var monsterBeh = newMonster.AddComponent<Monster>();
        monsterBeh.M_moveTarget = m_moveTarget;

        m_lastSpawn = Time.time;
    }
}
