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
    private float _speed = .03f;

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
        Vector3 move = _cameraTransform.up * _moveInput.y + _cameraTransform.right * _moveInput.x;
        move.z = 0f;
        transform.Translate(move * _speed);
    }

    public void ResetObject()
    {
        Debug.Log("reset");
        this.gameObject.transform.localPosition = _original;
    }
}
