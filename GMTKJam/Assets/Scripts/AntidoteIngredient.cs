using UnityEngine;

public class AntidoteIngredient : MonoBehaviour, IInteractable
{

    public Transform parent;
    public bool LockCamera { get; set; } = false;

    public void Click()
    {/*
        Debug.Log("Antidote click!");
        parent = transform.parent;
        if (parent.GetComponent<IInteractable>() != null)
            parent.GetComponent<IInteractable>().Click();
        else if (parent.GetChild(0).GetComponent<IInteractable>() != null)
            parent.GetChild(0).GetComponent<IInteractable>().Click();
        */
    }

    public void Interact()
    {
        parent = transform.parent;
        if (parent.name.Contains("bookshelf"))
        {
            return;
        }
        if (parent.GetComponent<IInteractable>() != null )
        parent.GetComponent<IInteractable>().Interact();
        else if (parent.GetChild(0).GetComponent<IInteractable>() != null)
            parent.GetChild(0).GetComponent<IInteractable>().Interact();
    }

    public void OnEnable()
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
