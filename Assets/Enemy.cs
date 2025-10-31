using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        if (_target == null)
        {
            _target = GameObject.FindGameObjectWithTag("Player");
        }
        _navMeshAgent.SetDestination(_target.transform.position);
    }
}
