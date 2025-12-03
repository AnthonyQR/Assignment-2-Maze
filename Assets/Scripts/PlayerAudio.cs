using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    // List of different walking sounds
    [SerializeField] private List<AudioClip> _walkingSounds;
    [SerializeField] private List<AudioClip> _collisionSounds;

    // Walking & collision audio sources
    [SerializeField] private AudioSource _walkingAudioSource;
    [SerializeField] private AudioSource _collisionAudioSource;

    [SerializeField] private CharacterController _controller;

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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Ignore collisions with Enemy.
        if (hit.gameObject.CompareTag("Enemy"))
        {
            return;
        }

        // Ignore collisions with floor.
        if (hit.transform.position.y < transform.position.y)
        {
            return;
        }

        // Ignore collisions if player is not moving enough.
        float playerVelocity = _controller.velocity.magnitude;
        if (playerVelocity < 0.5f)
        {
            return;
        }

        // Play collision sound otherwise.
        PlayCollisionSound();
    }

    public void PlayCollisionSound()
    {
        // Randomize walking sound if the audio source is not playing.
        if (!_collisionAudioSource.isPlaying)
        {
            int randomCollisionSound = Random.Range(0, _collisionSounds.Count);

            // Keep randomizing for a different sound clip.
            while (_collisionAudioSource.clip == _collisionSounds[randomCollisionSound])
            {
                randomCollisionSound = Random.Range(0, _collisionSounds.Count);
            }

            // Set audio clip & play.
            _collisionAudioSource.clip = _collisionSounds[randomCollisionSound];
            _collisionAudioSource.Play();
        }
    }
}
