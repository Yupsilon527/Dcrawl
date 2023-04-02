using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;
    private void Awake()
    {
        main = this;
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
}
