using NUnit.Framework;
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
    [SerializeField] private float _disableDelay;
    [SerializeField] private GameManager _gameManagerScript;
    private int _currentHealth;

    [Header("Audio")]
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioSource _enemyAudioSource;
    

    private Vector3 _startingPosition;

    private void Awake()
    {
        _startingPosition = transform.position;
        _currentHealth = _maxHealth;
    }

    private void Start()
    {
        if (_target == null)
        {
            _target = GameObject.FindGameObjectWithTag("Player");
        }
        _gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void FixedUpdate()
    {
        _navMeshAgent.SetDestination(_target.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy caught the player! Resetting player position.");
            other.gameObject.GetComponent<PlayerMovement>().ResetPosition();
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
        _currentHealth = _maxHealth;
        _gameManagerScript.StartEnemyRespawnTimer(_respawnTimer);
        _enemyAudioSource.clip = _deathSound;
        _enemyAudioSource.Play();
        _capsuleCollider.enabled = false;
        foreach(GameObject renderer in _meshRenderers)
        {
            renderer.SetActive(false);
        }
    }

    public void Respawn(Vector3 position)
    {
        _navMeshAgent.Warp(position);
        _capsuleCollider.enabled = true;
        foreach (GameObject renderer in _meshRenderers)
        {
            renderer.SetActive(true);
        }
        _enemyAudioSource.Play();
    }
}
