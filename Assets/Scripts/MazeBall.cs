using UnityEngine;
using System.Collections.Generic;

public class MazeBall : MonoBehaviour
{
    [Header("Ball Stats")]
    [SerializeField] private float _ballSpeed;
    [SerializeField] private float _ballLifetime;
    [SerializeField] private float _ballDestroyDelay;
    private bool _destroyBall = false;

    [Header("Ball Components")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private MeshRenderer _meshRenderer;

    [Header("BallAudio")]
    [SerializeField] private AudioClip _ballSound;
    [SerializeField] private AudioSource _ballAudioSource;
    
    private MazeGameManager _gameManagerScript;
    void Start()
    {
        // Move the ball forward depending on where the player is facing.
        _rb.AddForce(transform.forward * _ballSpeed);

        _ballAudioSource.clip = _ballSound;

        // Get required components
        _rb = GetComponent<Rigidbody>();
        _sphereCollider = GetComponent<SphereCollider>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _gameManagerScript = 
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<MazeGameManager>();
    }

    private void Update()
    {
        // Destroy ball a short duration after spawning.
        // If ball is not being destroyed by Enemy.
        if (!_destroyBall)
        {
            _ballLifetime -= Time.unscaledDeltaTime;
            if (_ballLifetime <= 0 && !_destroyBall)
            {
                Destroy(gameObject);
            }
        }

        // Destroy ball after a delay from hitting the Enemy.
        else
        {
            _ballDestroyDelay -= Time.unscaledDeltaTime;
            if (_ballDestroyDelay <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _ballAudioSource.Play();
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
                _ballAudioSource.Play();
                PrepareBallDestroy();
            }
        }
    }
    private void PrepareBallDestroy()
    {
        // Start timer.
        _destroyBall = true;

        // Destroy most components.
        Destroy(_rb);
        Destroy(_sphereCollider);
        Destroy(_meshRenderer);
    }
}
