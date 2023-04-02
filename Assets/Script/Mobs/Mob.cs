using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public Movement movement;
    public Animator animator;
    public AudioSource audio;

    protected virtual void Awake()
    {
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        foreach (BaseComponent c in GetComponents<BaseComponent>())
        {
            c.OnSpawn();
        }
    }
    public virtual bool SanityCheck()
    {
        return (gameObject.activeSelf && gameObject.activeInHierarchy);

    }
}
