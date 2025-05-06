using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundOnAwake : MonoBehaviour
{
    public AudioClip[] sounds; // Array to hold audio clips
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on this GameObject.");
            return;
        }

        if (sounds.Length > 0)
        {
            int randomIndex = Random.Range(0, sounds.Length); // Get a random index
            audioSource.clip = sounds[randomIndex]; // Assign the random clip
            audioSource.Play(); // Play the sound on Awake
        }
        else
        {
            Debug.LogWarning("No audio clips assigned to the 'sounds' array.");
        }
    }
}
