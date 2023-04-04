using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public Movement movement;
    public AudioSource audio;

    public AudioClip monsterCall;
    public ParticleSystem noiseParticle;

    protected virtual void Awake()
    {
        movement = GetComponent<Movement>();
        audio = GetComponent<AudioSource>();
    }
    protected virtual void Start()
    {
        foreach (BaseComponent c in GetComponents<BaseComponent>())
        {
            c.OnSpawn();
        }
        PostMove();
    }
    public virtual void OnPlayerEcho()
    {
        if (audio != null && monsterCall != null)
            audio.PlayOneShot(monsterCall);
        Debug.Log($"Player shouts at {name}");
    }
    public virtual void OnPlayerTouch()
    {
        Debug.Log($"Player touches at {name}");
    }
    public virtual void PostMove()
    {
        if (noiseParticle != null)
            noiseParticle.Play();
    }
    public virtual bool SanityCheck()
    {
        return (gameObject.activeSelf && gameObject.activeInHierarchy);

    }
}
