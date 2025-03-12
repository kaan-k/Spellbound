using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSelector : MonoBehaviour, ITargetSelector
{
    public GameObject selectedEnemy;
    public bool hasTarget = false;
    public string targetTag;

    void Update()
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

        // Find the closest enemy
        selectedEnemy = enemies.OrderBy(enemy => Vector2.Distance(transform.position, enemy.transform.position)).FirstOrDefault();
        hasTarget = selectedEnemy != null;

        return selectedEnemy;
    }
}
