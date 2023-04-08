using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepySoundTrigger : MonoBehaviour
{
    public AudioClip audioClip; // Audio clip to play when player collides with the trigger
    private AudioSource audioSource;

    private void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        // Set the audio clip and volume on the AudioSource component
        audioSource.clip = audioClip;
        Invoke("PlayAudio", 7f);

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Play the audio clip when anything collides with the trigger
        Invoke("PlayAudio",0f);
    }

    private void PlayAudio()
    {
        // Play the audio clip
        audioSource.Play();
    }
}
