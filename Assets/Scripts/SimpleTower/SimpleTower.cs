using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;

public class SimpleTower : MonoBehaviour
{
    private FindTargetController _findTargetController;

    private GuidedTowerShootingController _guidedTowerShootingController;

    private void Awake()
    {
        _findTargetController = GetComponent<FindTargetController>();
        _guidedTowerShootingController = GetComponent<GuidedTowerShootingController>();
    }

    void Update()
    {
        _findTargetController.FindMonster(_guidedTowerShootingController.Range);
        _guidedTowerShootingController.TryShoot(_findTargetController.Target);
    }
}
