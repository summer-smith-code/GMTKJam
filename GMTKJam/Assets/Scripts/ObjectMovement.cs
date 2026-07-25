using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectMovement : MonoBehaviour
{
    PlayerInput _input;
    InputAction _moveAction;

    private Vector2 _moveInput;
    public bool isSelected;

    private Vector3 _original;


    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _cameraForward;
    public float _speed = 1f;
    float actualSpeed;
    public float maxSpeedLoss = 0.8f;

    [Header("Boundaries")]

    public float boundary_minX = -1f;
    public float boundary_minY = -1f;
    public float boundary_maxX = .5f;
    public float boundary_maxY = .5f;

    [Header("Sway")]

    Vector2 swayDesire;
    Vector3 swayActual;

    public float maxSway;
    public float swayAcceleration;
    public float maxSwaySpeed = 0.5f;
    public float swayCalculateTime = 1f;

    void Start()
    {
        actualSpeed = _speed;
        swayDesire = Vector3.zero;
        _original = this.transform.localPosition;
        Debug.Log(_original);
        InvokeRepeating(nameof(CalculateSway), 0f, swayCalculateTime);
    }

    void FixedUpdate()
    {
        if (isSelected)
        {
            MoveObject();
            ProcessSway();
        }
    }

    private void ProcessSway()
    {
        actualSpeed = _speed * (1 - (GameManager.Instance.GetDifficultyValue() * maxSpeedLoss));

        Vector3 desire = _cameraForward.up * swayDesire.y + _cameraForward.right * swayDesire.x;
        swayActual = Vector3.Lerp(swayActual, desire, Time.deltaTime * swayAcceleration);

        transform.position += swayActual * Time.deltaTime * maxSwaySpeed * GameManager.Instance.GetDifficultyValue(); //= Vector3.Lerp(transform.position, GameManager.Instance.GetDifficultyValue() * swayActual, Time.deltaTime * maxSwaySpeed);
    }

    private void CalculateSway()
    {
        swayDesire = new Vector2(Random.Range(-maxSway, maxSway), Random.Range(-maxSway, maxSway)) * GameManager.Instance.GetDifficultyValue();
    }

    private void MoveObject()
    {
        if (_cameraTransform == null)
            _cameraTransform = GameManager.Instance._fpCamera.transform;
        if (_input == null)
        {
            _input = GameManager.Instance._playerInput;
            _moveAction = _input.actions["Move"];
        }
        _moveInput = _input.actions["Move"].ReadValue<Vector2>();
        // Debug.Log($"{_cameraForward.right}");
        Vector3 move = _cameraForward.up * _moveInput.y + _cameraForward.right * _moveInput.x;

        transform.position += move * actualSpeed * Time.deltaTime;

        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, _original.x + boundary_minX, _original.x + boundary_maxX), Mathf.Clamp(transform.localPosition.y, _original.y + boundary_minY, _original.y + boundary_maxY), transform.localPosition.z);
    }

    public void ResetObject()
    {
        Debug.Log("reset");
        this.gameObject.transform.localPosition = _original;
    }
}
