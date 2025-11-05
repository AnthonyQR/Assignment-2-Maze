using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private Vector3 _startingPosition;

    private void Awake()
    {
        _startingPosition = transform.position;
    }

    private void Start()
    {
        if (_target == null)
        {
            _target = GameObject.FindGameObjectWithTag("Player");
        }
    }
    private void FixedUpdate()
    {
        _navMeshAgent.SetDestination(_target.transform.position);
    }

    public void ResetPosition()
    {
        _navMeshAgent.Warp(_startingPosition);
    }
}
