using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] sfx, ambient, droplet;
    public AudioSource soundSource, ambientSource, dropletSource
        ;
    public AudioMixerGroup[] channel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayAmbient("Level Music");
        PlayDrops("Droplet");
    }

    public void PlaySfx(string name, int sourceIndex)
    {
        Sound s = Array.Find(sfx, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            soundSource.outputAudioMixerGroup = channel[sourceIndex];
            soundSource.PlayOneShot(s.clip);
        }
    }

    public void PlayAmbient(string name)
    {
        Sound s = Array.Find(ambient, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Ambient sound not found");
        }
        else
        {
            ambientSource.clip = s.clip;
            ambientSource.Play();
        }
    }

    public void PlayDrops(string name)
    {
        Sound s = Array.Find(droplet, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Droplet sound not found");
        }
        else
        {
            dropletSource.clip = s.clip;
            dropletSource.Play();
        }
    }


}
