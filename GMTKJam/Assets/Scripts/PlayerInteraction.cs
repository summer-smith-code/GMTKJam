using JetBrains.Annotations;
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

    private float _interactionRange = .7f; // range within which the player can interact with objects

    // handles all player interactions with objects
    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _interactAction = _input.actions["Interact"];
        _clickAction = _input.actions["Attack"];
    }

    private void OnEnable()
    {
        _input = GetComponent<PlayerInput>();
        _interactAction = _input.actions["Interact"];
        _clickAction = _input.actions["Attack"];
        if (_interactAction == null)
        {
            Debug.LogError("Interact action not found in PlayerInput actions.");
        }
        else
        {
            _interactAction.performed += OnInteract;

            _interactAction?.Enable();
        }
        if (_clickAction == null)
        {
            Debug.LogError("Click action not found in PlayerInput actions.");
        }
        else
        {
            _clickAction.performed += OnClick;

            _clickAction?.Enable();
        }
    }

    private void OnDisable()
    {
        _interactAction.performed -= OnInteract;
        _interactAction?.Disable();
        _clickAction.performed -= OnClick;
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
                interactable.Interact();
                return; // Exit after interacting with the first interactable object
            }
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Click action performed");

        if (_last != null)
        {
            _last.Click();
            return; // Exit after interacting with the first interactable object
        }
    }

    void Update()
    {
    }
}
