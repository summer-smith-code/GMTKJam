using System;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{

    PlayerInput _input;
    InputAction _interactAction;
    InputAction _clickAction;

    private float _offset = 1.5f;
    private bool _isLooking;

    private IInteractable _last;
    private GameObject _lastObj;
    private Outline _lastOutline;

    public float highlightWidth = 3f;
    public Color highlightColor;

    private float _interactionRange = .7f; // range within which the player can interact with objects

    // handles all player interactions with objects
    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _interactAction = _input.actions["Interact"];
        _clickAction = _input.actions["Click"];
    }

    private void OnEnable()
    {
        _input = GetComponent<PlayerInput>();
        _interactAction = _input.actions["Interact"];
        _clickAction = _input.actions["Click"];
        if (_interactAction == null)
        {
            Debug.LogError("Interact action not found in PlayerInput actions.");
        }
        if (_clickAction == null)
        {
            Debug.LogError("Click action not found in PlayerInput actions.");
        }
        _interactAction.performed += OnInteract;
        _clickAction.performed += OnClick;

        _interactAction?.Enable();
        _clickAction?.Enable();
    }

    private void OnDisable()
    {
        _interactAction.performed -= OnInteract;
        _clickAction.performed -= OnClick;
        _interactAction?.Disable();
        _clickAction?.Disable();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {

        if (!context.started) return;
        if (_isLooking)
        {
            // if out of range, can cancel
            GameManager.Instance._cameraMovement.LockCamera(false);
            _isLooking = false;
            _last.Interact();
        }
        Debug.Log("Interact action performed");
        Vector3 pos = this.gameObject.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(Player._instance._RaycastPivot.transform.position, Player._instance._RaycastPivot.transform.forward, out hit, _interactionRange))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                Vector3 targetPosition = hit.collider.transform.position - (hit.collider.transform.forward * _offset);
                targetPosition.y = this.gameObject.transform.position.y;
                if (interactable.LockCamera)
                {
                    this.gameObject.transform.position = targetPosition;
                    GameManager.Instance._cameraMovement.LookAtObject(hit.collider.gameObject);
                    _isLooking = true;
                    _last = interactable;
                }

                _lastObj = null;
                interactable.Interact();
                return; // Exit after interacting with the first interactable object
            }
        }
        else
        {
            Debug.Log(pos);
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Clicked");
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Debug.Log(ray.direction);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                GameManager.Instance._cameraMovement.LookAtObject(hitInfo.collider.gameObject);

                // this.gameObject.transform.position = hitInfo.collider.transform.position + Vector3.forward;
                interactable.Interact();
            }
        }
    }

    void Update()
    {
        ProcessHighlightVisuals();
    }

    void OnSwapLast()
    {
        if (_lastOutline != null)
        {
            Destroy(_lastOutline);
            _lastOutline = null;
        }
    }

    void ProcessHighlightVisuals()
    {
        RaycastHit hit;
        if (Physics.Raycast(Player._instance._RaycastPivot.transform.position, Player._instance._RaycastPivot.transform.forward, out hit, _interactionRange))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                _lastObj = hit.collider.gameObject;
            }
                
        }
        else
        {
            if(_lastOutline != null)
            {
                Destroy(_lastOutline);
                _lastOutline = null;
            }
            
            _lastObj = null;
        }

        if (_lastObj == null)
            return;

        if (_lastObj.GetComponent<Outline>() == null)
        {
            if(_lastOutline != null)
            {
                Destroy(_lastOutline);
                _lastOutline = null;
            }

            _lastOutline = _lastObj.AddComponent<Outline>();
            _lastOutline.OutlineWidth = highlightWidth;
            _lastOutline.OutlineColor = highlightColor;
        }
    }
}
