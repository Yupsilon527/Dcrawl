using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseComponent : MonoBehaviour
{
    public Mob parent;
    protected virtual void Awake()
    {
        parent = GetComponent<Mob>();
    }
    public virtual void OnSpawn()
    {

    }
    public virtual void OnDespawn()
    {

    }
    public virtual bool SanityCheck()
    {
        return (gameObject.activeSelf && gameObject.activeInHierarchy);

    }
}
