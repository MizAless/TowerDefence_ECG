using UnityEngine;
using System.Collections;

public class CannonProjectile : MonoBehaviour
{
    [SerializeField] private int m_damage = 10;
    
    [SerializeField] public bool m_isPhysical = false;

    [SerializeField] public float m_speed { get; private set; } = 20f;

    private Rigidbody m_rigidbody;

    void Awake()
    {
        if (m_isPhysical)
            m_rigidbody = gameObject.AddComponent<Rigidbody>();
    }

    void Start()
    {
        if (m_isPhysical)
            m_rigidbody.AddForce(transform.forward * m_speed, ForceMode.Impulse);
    }

    void Update()
    {
        if (m_isPhysical != true)
            Move();
    }

    private void Move()
    {
        var translation = Vector3.forward * m_speed * Time.deltaTime;
        transform.Translate(translation);
    }

    void OnTriggerEnter(Collider other)
    {
        var monster = other.gameObject.GetComponent<Monster>();

        if (monster == null)
            return;

        monster.TakeDamage(m_damage);

        Destroy(gameObject);
    }
}