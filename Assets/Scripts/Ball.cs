using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _ballSpeed;
    [SerializeField] private Rigidbody _rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * _ballSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
