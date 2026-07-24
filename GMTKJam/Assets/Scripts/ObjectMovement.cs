using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectMovement : MonoBehaviour
{
    PlayerInput _input;
    InputAction _moveAction;

    private Vector2 _moveInput;
    private Rigidbody _rigidbody;
    public bool isSelected;

    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _speed = 5.0f;

    void Start()
    {
        _input = GameManager.Instance._playerInput;
        _moveAction = _input.actions["Move"];
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isSelected)
        {
            MoveObject();
        }
    }

    private void MoveObject()
    {
        _moveInput = _input.actions["Move"].ReadValue<Vector2>();
        Vector3 move = _cameraTransform.up * _moveInput.y + _cameraTransform.right * _moveInput.x;
        move.z = 0f;
        _rigidbody.AddForce(move.normalized * _speed, ForceMode.VelocityChange);
    }
}
