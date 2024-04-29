using UnityEngine;

public class TowerMovementController : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    public void SmoothRotateTowards(Vector3 target, Vector3 shootPoint)
    {
        Vector3 directionToFuturePosition = target - shootPoint;
        Quaternion rotationToFuturePosition = Quaternion.LookRotation(directionToFuturePosition);
        Vector3 euler = rotationToFuturePosition.eulerAngles;

        Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, euler.y, transform.rotation.eulerAngles.z);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
}
