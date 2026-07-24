using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{

    PlayerInput _input;
    InputAction _interactAction;
    InputAction _clickAction;

    [SerializeField] private float _interactionRange = 3.0f; // range within which the player can interact with objects

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
        Debug.Log("Interact action performed");
        Vector3 pos = this.gameObject.transform.position;
        if (Physics.CheckSphere(pos, _interactionRange))
        {
            Debug.Log("Checked sphere");
            Collider[] hitColliders = Physics.OverlapSphere(pos, _interactionRange);
            foreach (var hitCollider in hitColliders)
            {
                Debug.Log("Hit colliders");
                IInteractable interactable = hitCollider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                    break; // Interact with the first interactable object found
                }
            }
        } else
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
                interactable.Interact();
            }
        }
    }

    void Update()
    {
    }
}
