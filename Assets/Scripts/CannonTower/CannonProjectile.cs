using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;

public class CannonProjectile : MonoBehaviour
{
    [SerializeField] private int m_damage = 10;

    [SerializeField] public bool m_isPhysical = false;

    [SerializeField] public float _speed { get; private set; } = 20f;

    [SerializeField] private float _maxFlightTime = 10f;

    private Rigidbody m_rigidbody;

    private float _startTime;

    private ProjectilePool _pool;


    void Awake()
    {
        if (m_isPhysical)
            m_rigidbody = gameObject.AddComponent<Rigidbody>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (m_isPhysical != true)
            Move();

        DestroyForSomeTime();
    }

    public void Initialize(Vector3 position, Quaternion rotation, ProjectilePool pool)
    {
        transform.position = position;
        transform.rotation = rotation;
        _pool = pool;
        _startTime = Time.time;

        if (m_isPhysical)
            m_rigidbody.AddForce(transform.forward * _speed, ForceMode.Impulse);
    }

    private void Move()
    {
        var translation = Vector3.forward * _speed * Time.deltaTime;
        transform.Translate(translation);
    }

    private void DestroyForSomeTime()
    {
        if (Time.time - _startTime >= _maxFlightTime)
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        _pool.ReturnProjectile(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        var monster = other.gameObject.GetComponent<Monster>();

        if (monster == null)
            return;

        ReturnToPool();

        monster.TakeDamage(m_damage);
    }
}