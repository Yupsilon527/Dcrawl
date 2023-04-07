using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerReverbController : MonoBehaviour
{
    //public AudioMixer mixer;
    public AudioReverbZone[] reverbZones;
    public float maxDistance = 10.0f;

    private AudioReverbZone currentZone;

    private void Start()
    {
       // mixer.SetFloat("Echo Wet", 0f); // set the reverb level to 0 initially
    }

    private void Update()
    {
        ReverbControl();
    }

    private void ReverbControl()
    {
        float nearestDistance = float.MaxValue;
        AudioReverbZone nearestZone = null;
        Transform playerTransform = transform; // assuming this script is attached to the player object

        foreach (AudioReverbZone zone in reverbZones)
        {
            float distance = Vector3.Distance(playerTransform.position, zone.transform.position);
            if (distance <= maxDistance && distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestZone = zone;
            }
        }

        if (nearestZone != null && nearestZone != currentZone)
        {
            currentZone = nearestZone;
           // mixer.FindSnapshot("SFXReverb").TransitionTo(0.5f);
        }
        else if (nearestZone == null && currentZone != null)
        {
            currentZone = null;
           // mixer.FindSnapshot("Echo Sound").TransitionTo(0.5f);
        }
    }
}


