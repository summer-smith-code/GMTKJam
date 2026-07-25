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
    private float _speed = 1f;

    void Start()
    {
        _original = this.transform.localPosition;
        Debug.Log(_original);
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

        if (_cameraTransform == null)
            _cameraTransform = GameManager.Instance._fpCamera.transform;
        if (_input == null)
        {
            _input = GameManager.Instance._playerInput;
            _moveAction = _input.actions["Move"];
        }
        _moveInput = _input.actions["Move"].ReadValue<Vector2>();
        Debug.Log($"{_cameraForward.right}");
        Vector3 move = _cameraForward.up * _moveInput.y + _cameraForward.right * _moveInput.x;

        transform.position += move * _speed * Time.deltaTime;
    }

    public void ResetObject()
    {
        Debug.Log("reset");
        this.gameObject.transform.localPosition = _original;
    }
}
