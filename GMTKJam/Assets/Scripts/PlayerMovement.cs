using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // player movement script using the new input system
    PlayerInput _input;
    InputAction _moveAction;

    private Vector2 _moveInput;
    private Rigidbody _rigidbody;

    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _speed = 5.0f;

    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _moveAction = _input.actions["Move"];
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _moveInput = _input.actions["Move"].ReadValue<Vector2>();
        Vector3 move = _cameraTransform.forward * _moveInput.y + _cameraTransform.right * _moveInput.x;
        move.y = 0f; // Prevent vertical movement
        _rigidbody.AddForce(move.normalized * _speed, ForceMode.VelocityChange);
    }
}
