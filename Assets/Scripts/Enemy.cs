using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [Space]

    [Header("Enemy Stats")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _respawnTimer;
    [SerializeField] private GameManager _gameManagerScript;
    private int _currentHealth;

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
            _currentHealth = _maxHealth;
            _gameManagerScript.StartEnemyRespawnTimer(_respawnTimer);
            gameObject.SetActive(false);
        }
    }
}
