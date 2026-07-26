using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // player movement script using the new input system
    PlayerInput _input;
    InputAction _moveAction;
    public bool isSelected = true;

    public float maxSlopeAngle = 45f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Vector2 _moveInput;
    private Rigidbody _rigidbody;

    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _height = 1f;

    private RaycastHit currentSlopeHit;

    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _moveAction = _input.actions["Move"];
        _rigidbody = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        if (isSelected) 
        {
            MovePlayer();
            //SnapToFloor();
        }
    }

    private void MovePlayer()
    {
        _moveInput = _input.actions["Move"].ReadValue<Vector2>();
        Vector3 move = _cameraTransform.forward * _moveInput.y + _cameraTransform.right * _moveInput.x;
        move.y = 0f; // Prevent vertical movement

        if (OnSlope()) // Slope detected... Move as if on slope
        {
            Debug.Log("Slope detected");
            Vector3 slopeMoveDirection = GetSlopeMoveDirection(move);
            _rigidbody.AddForce(slopeMoveDirection * _speed * 4f, ForceMode.VelocityChange);
        }
        else // No slope detected, move as if on flat ground
            _rigidbody.AddForce(move.normalized * _speed, ForceMode.VelocityChange);
    }

    private void SnapToFloor()
    {
        RaycastHit hit;
        Physics.Raycast(groundCheck.position, Vector3.down, out hit, 999f, groundLayer);

        if(hit.transform != null)
        {
            transform.position = new Vector3(transform.position.x, hit.transform.position.y + _height, transform.position.z);
        }
    }

    private bool OnSlope()
    {
        // Cast a ray down from the center of the player
        if (Physics.Raycast(groundCheck.position, Vector3.down, out currentSlopeHit, 0.1f, groundLayer))
        {
            float angle = Vector3.Angle(Vector3.up, currentSlopeHit.normal);
            // Returns true if surface is angled but within climbable limit
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection(Vector3 moveDirection)
    {
        // Projects movement onto the slope plane using the hit normal
        return Vector3.ProjectOnPlane(moveDirection, currentSlopeHit.normal).normalized;
    }
}
