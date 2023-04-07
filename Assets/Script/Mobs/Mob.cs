using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public Movement movement;
    public DamageableComponent damageable;
    public AudioSource audio;

    public AudioClip monsterCall;
    public ParticleSystem noiseParticle;

    protected virtual void Awake()
    {
        movement = GetComponent<Movement>();
        damageable = GetComponent<DamageableComponent>();
        audio = GetComponent<AudioSource>();
    }
    protected virtual void Start()
    {
        foreach (BaseComponent c in GetComponents<BaseComponent>())
        {
            c.OnSpawn();
        }
    }
    public virtual void OnPlayerEcho(Mob other)
    {
        if (audio != null && monsterCall != null)
            audio.PlayOneShot(monsterCall); //SFX monster call sound, that it used to reply when it is in front of the player
        Debug.Log($"Player shouts at {name}");
    }
    public virtual void OnPlayerTouch(Mob other)
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
    public virtual bool CanAct()
    {
        return movement.CanMove();
    }
    public virtual void Die()
    {
        movement.myTile.LocatedEntity = null;
        gameObject.SetActive(false);
    }
}
