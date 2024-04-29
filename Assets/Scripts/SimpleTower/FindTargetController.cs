using UnityEngine;

public class FindTargetController : MonoBehaviour
{
    [HideInInspector] public Monster Target;

    public void FindMonster(float range)
    {
        foreach (var monster in FindObjectsOfType<Monster>())
        {
            float distance = Vector3.Distance(transform.position, monster.transform.position);

            if (distance < range)
                Target = monster;
        }
    }
}
