using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Navigation")]
    [SerializeField] private GameObject _target;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [Space]

    [Header("Components")]
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private List<GameObject> _meshRenderers;
    [Space]

    [Header("Enemy Stats")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _respawnTimer;
    private int _currentHealth;
    private bool _isDead;
    private float _currentRespawnTimer;

    [Header("Audio")]
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioSource _enemyAudioSource;
    
    private Vector3 _startingPosition;

    // Reference Maze Size
    private int _mazeWidth;
    private int _mazeDepth;

    private void Awake()
    {
        _startingPosition = transform.position;
        _currentHealth = _maxHealth;
        _isDead = false;
        _currentRespawnTimer = _respawnTimer;
    }

    private void Start()
    {
        if (_target == null)
        {
            _target = GameObject.FindGameObjectWithTag("Player");
        }
        // Get size of the maze.
        MazeGenerator _mazeGeneratorScript = GameObject.FindGameObjectWithTag("MazeGenerator").GetComponent<MazeGenerator>();
        (_mazeWidth, _mazeDepth) = _mazeGeneratorScript.GetMazeSize();
    }
    private void FixedUpdate()
    {
        _navMeshAgent.SetDestination(_target.transform.position);

        // Respawn enemy timer.
        if (_isDead)
        {
            _currentRespawnTimer -= Time.unscaledDeltaTime;
            if (_currentRespawnTimer <= 0)
            {
                Respawn();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy caught the player! Resetting player position.");
            other.gameObject.GetComponent<PlayerMovement>().ResetPosition();
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        _navMeshAgent.Warp(_startingPosition);
    }

    public void Hit()
    {
        _currentHealth -= 1;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Reset current health.
        _currentHealth = _maxHealth;

        // Start respawn timer.
        _currentRespawnTimer = _respawnTimer;
        _isDead = true;

        // Play audio.
        _enemyAudioSource.clip = _deathSound;
        _enemyAudioSource.Play();

        // Disable visuals & collisions.
        _capsuleCollider.enabled = false;
        foreach(GameObject renderer in _meshRenderers)
        {
            renderer.SetActive(false);
        }
    }

    public void Respawn()
    {
        // Reset Bool
        _isDead = false;

        // Reset Timer
        _currentRespawnTimer = _respawnTimer;

        // Position enemy.
        int enemyX = Random.Range(0, _mazeWidth - 1);
        int enemyZ = Random.Range(0, _mazeDepth - 1);
        Vector3 newEnemyPosition = new Vector3(enemyX, 0f, enemyZ);
        _navMeshAgent.Warp(newEnemyPosition);

        // Reenable visuals & collisions.
        _capsuleCollider.enabled = true;
        foreach (GameObject renderer in _meshRenderers)
        {
            renderer.SetActive(true);
        }

        // Play respawn sound.
        _enemyAudioSource.Play();
    }
}
