using UnityEngine;
using System.Collections.Generic;

public class Ball : MonoBehaviour
{
    [Header("Ball Stats")]
    [SerializeField] private float _ballSpeed;
    [SerializeField] private float _ballLifetime;

    [Header("Ball Components")]
    [SerializeField] private Rigidbody _rb;

    [Header("BallAudio")]
    [SerializeField] private List<AudioClip>_ballSounds;
    [SerializeField] private AudioSource _ballAudioSource;
    
    private GameManager _gameManagerScript;
    void Start()
    {
        // Move the ball forward depending on where the player is facing.
        _rb.AddForce(transform.forward * _ballSpeed);

        // Get required components
        _rb = GetComponent<Rigidbody>();
        _gameManagerScript = 
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        // Destroy ball a short duration after spawning.
        _ballLifetime -= Time.unscaledDeltaTime;
        if (_ballLifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detect collisions with the enemy.
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemyScript;
            other.gameObject.TryGetComponent<Enemy>(out enemyScript);

            // Hit the enemy if they have the script.
            if (enemyScript != null)
            {
                enemyScript.Hit();
                _gameManagerScript.HitEnemy();
                Destroy(gameObject); // Destroy the ball afterwards.
            }
        }
    }
}
