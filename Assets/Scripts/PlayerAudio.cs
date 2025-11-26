using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    // List of different walking sounds
    [SerializeField] private List<AudioClip> _walkingSounds;

    // Walking & collision audio sources
    [SerializeField] private AudioSource _walkingAudioSource;
    [SerializeField] private AudioSource _collisionAudioSource;

    public void PlayWalkingSound()
    {
        // Randomize walking sound if the audio source is not playing.
        if (!_walkingAudioSource.isPlaying)
        {
            int randomWalkingSound = Random.Range(0, _walkingSounds.Count);

            // Keep randomizing for a different sound clip.
            while (_walkingAudioSource.clip == _walkingSounds[randomWalkingSound])
            {
                randomWalkingSound = Random.Range(0, _walkingSounds.Count);
            }
            
            // Set audio clip & play.
            _walkingAudioSource.clip = _walkingSounds[randomWalkingSound];
            _walkingAudioSource.Play();
        }
    }

    public void StopPlayingWalkingSound()
    {
        // Stops when the player is not moving.
        _walkingAudioSource.Stop();
    }
}
