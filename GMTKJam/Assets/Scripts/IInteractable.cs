using UnityEngine;

public interface IInteractable
{
    // interface method to be implemented by any class that wants to be interactable
    public bool LockCamera {  get; set; }
    void Interact();
}
