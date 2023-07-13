using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public bool Launched = false;
    private float _conveyorSpeed = 0.5f;
    private Rigidbody _rigidbody;
    private Vector3 _rbPosition;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!Launched)
            return;

        _rbPosition = _rigidbody.position;
        _rigidbody.position -= _conveyorSpeed * Time.fixedDeltaTime * _rigidbody.transform.right;
        _rigidbody.MovePosition(_rbPosition);
    }
}
