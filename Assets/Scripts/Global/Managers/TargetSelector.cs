using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSelector : MonoBehaviour, ITargetSelector
{
    public GameObject selectedEnemy;
    public bool hasTarget = false;
    public string targetTag;

    void FixedUpdate()
    {
        SelectTarget(targetTag);
    }



    public GameObject SelectTarget(string targetTag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);
        if (enemies.Length == 0)
        {
            hasTarget = false;
            selectedEnemy = null;
            return null;
        }

        // Find closest enemy in O(n) time
        float minDistance = float.MaxValue;
        GameObject closestEnemy = null;

        Vector2 currentPosition = transform.position;

        foreach (var enemy in enemies)
        {
            float distance = Vector2.Distance(currentPosition, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy;
            }
        }

        selectedEnemy = closestEnemy;
        hasTarget = selectedEnemy != null;
        return selectedEnemy;
    }
}
