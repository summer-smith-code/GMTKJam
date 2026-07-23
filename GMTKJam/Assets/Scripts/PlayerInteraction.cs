using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // handles all player interactions with objects
    void Start()
    {
        
    }

    void Update()
    {
        // add a mouse click event to interact with objects
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();
            if (interactable != null && Input.GetMouseButtonDown(0))
            {
                interactable.Interact();
            }
        }
    }
}
