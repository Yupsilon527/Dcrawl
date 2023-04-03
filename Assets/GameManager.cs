using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;
    private void Awake()
    {
        main = this;
        enemies = new List<EnemyMob>();
    }
    public ObjectPool effectPool;
    public GameObject PlayEffect(GameObject prefab)
    {
        if (effectPool!=null)
        {
            return effectPool.PoolItem(prefab);
        }
        Debug.LogError("Effect pool not set up!");
        return null;
    }
    public List<EnemyMob> enemies;
    public void RegisterEnemy(EnemyMob other)
    {
        enemies.Add(other);
    }
    public void UnregisterEnemy(EnemyMob other)
    {
        enemies.Remove(other);
    }
    int cTurn = 0;
    private void FixedUpdate()
    {
        ForwardTurn(1);
    }
    public int ActionPointsPerTurn = 30;
    public void ForwardTurn(int ap)
    {
        cTurn += ap;
        if (cTurn> ActionPointsPerTurn)
        { OnTurnEnd();
            cTurn -= ActionPointsPerTurn;
        }
    }
    void OnTurnEnd()
    {
        foreach (EnemyMob e in enemies)

        {
                e.ai?.ResolveTurn();
        }
    }
}
