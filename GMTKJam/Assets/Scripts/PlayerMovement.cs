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

    public float footstepFreq = 1f;
    float currentFootstepTimer;
    public AudioSource footstepSource;
    public AudioClip[] footstepSounds;

    float currentCurveEval;
    float curveLength = 1f;
    public AnimationCurve limpCurve;
    public float limpIntensity;

    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _moveAction = _input.actions["Move"];
        _rigidbody = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;

        currentFootstepTimer = footstepFreq;
    }

    private void Update()
    {
        currentCurveEval += Time.deltaTime;

        if (currentCurveEval >= curveLength)
            currentCurveEval = 0f;

        limpIntensity = GameManager.Instance.GetDifficultyValue();
    }

    void FixedUpdate()
    {
        if (isSelected) 
        {
            MovePlayer();
            SnapToFloor();
        }
    }

    private void MovePlayer()
    {
        //result = normal + (curve - normal) × percent

        _moveInput = _input.actions["Move"].ReadValue<Vector2>();
        Vector3 move = (_cameraTransform.forward * _moveInput.y + _cameraTransform.right * _moveInput.x);
        move.y = 0f; // Prevent vertical movement

        if(move.magnitude > 0f)
        {
            currentFootstepTimer -= Time.deltaTime;

            if(currentFootstepTimer < 0f)
            {
                footstepSource.pitch = Random.Range(0.9f, 1.1f);
                footstepSource.PlayOneShot(footstepSounds[Random.Range(0, footstepSounds.Length)]);

                currentFootstepTimer = footstepFreq;
            }
        }
        //result = Mathf.Lerp(normal, curve, percent);
        float currentSpeed = Mathf.Lerp(_speed, limpCurve.Evaluate(currentCurveEval) * (_speed * 0.8f), limpIntensity); //_speed + (limpCurve.Evaluate(currentCurveEval) - _speed) * limpIntensity;

        if (OnSlope()) // Slope detected... Move as if on slope
        {
            Debug.Log("Slope detected");
            Vector3 slopeMoveDirection = GetSlopeMoveDirection(move);
            _rigidbody.AddForce(slopeMoveDirection * currentSpeed, ForceMode.VelocityChange);
        }
        else // No slope detected, move as if on flat ground
            _rigidbody.AddForce(move.normalized * currentSpeed, ForceMode.VelocityChange);
    }

    private void SnapToFloor()
    {
        RaycastHit hit;
        Physics.Raycast(groundCheck.position, Vector3.down, out hit, 0.4f, groundLayer);

        if(hit.transform == null && !OnSlope())
        {
            _rigidbody.AddForce(Vector3.down * 9.81f * 8f);
        }
    }

    private bool OnSlope()
    {
        // Cast a ray down from the center of the player
        if (Physics.Raycast(groundCheck.position, Vector3.down, out currentSlopeHit, .5f, groundLayer))
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
