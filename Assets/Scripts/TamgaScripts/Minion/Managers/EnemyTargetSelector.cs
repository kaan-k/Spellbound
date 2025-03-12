using UnityEngine;

public class EnemyTargetSelector : ITargetSelector
{
    private string enemyTag;

    public EnemyTargetSelector(string enemyTag)
    {
        this.enemyTag = enemyTag;
    }

    public GameObject SelectTarget()
    {
        return GameObject.FindWithTag(enemyTag);
    }
}
