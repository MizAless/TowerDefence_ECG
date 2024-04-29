using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;

    [SerializeField] private int _poolSize = 100;

    private List<GameObject> _pooledProjectiles = new List<GameObject>();

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
            projectile.SetActive(false);
            _pooledProjectiles.Add(projectile);
        }
    }

    public GameObject GetProjectile()
    {
        foreach (GameObject projectile in _pooledProjectiles)
        {
            if (!projectile.activeInHierarchy)
            {
                projectile.SetActive(true);
                return projectile;
            }
        }

        return null;
    }

    public void ReturnProjectile(GameObject projectile)
    {
        projectile.SetActive(false);
    }
}