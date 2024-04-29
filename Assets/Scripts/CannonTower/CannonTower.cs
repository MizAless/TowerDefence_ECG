using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;

public class CannonTower : MonoBehaviour
{
    private TargetPredictionConroller _targetPredictionConroller;

    private TowerShootingConroller _shootingConroller;

    private TowerMovementController _movementController;

    private void Awake()
    {
        _targetPredictionConroller = GetComponent<TargetPredictionConroller>();
        _shootingConroller = GetComponent<TowerShootingConroller>();
        _movementController = GetComponent<TowerMovementController>();
    }

    void Update()
    {
        float projectileSpeed = _shootingConroller.ProjectilePrefab.GetComponent<CannonProjectile>()._speed;

        _targetPredictionConroller.TryCalculateTargetPosition(_shootingConroller.ShootPoint.position, projectileSpeed);
        _movementController.SmoothRotateTowards(_targetPredictionConroller.FuturePosition, _shootingConroller.ShootPoint.position);
        _shootingConroller.TryShoot(_targetPredictionConroller.FuturePosition);
    }
}
