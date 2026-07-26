using NUnit.Framework.Constraints;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Shelf : MiniGameBase, IInteractable
{
    [SerializeField] private GameObject _hand;
    private ObjectMovement _obj;
    private RaycastHit hit;

    public AudioSource source;
    public AudioClip pickupSound;

    public bool LockCamera { get; set; } = true;

    public void Click()
    {
        Debug.Log("Clicking on the shelf.");    
        if (isGameActive)
            if (_obj != null)
            {
                Debug.Log("Clicking on the shelf with an object.");
                RaycastHit hit;
                if (Physics.Raycast(_obj.transform.position, _obj.transform.forward, out hit))
                {
                    Debug.Log("Hit object: " + hit.collider.name);
                    if (hit.collider.CompareTag("antidoteIngredient"))
                    {
                        AntidoteIngredient ingredient = hit.collider.GetComponent<AntidoteIngredient>();
                        if (ingredient != null)
                        {
                            Destroy(ingredient.gameObject);
                            Player._instance._magnesium = true;

                            if (source)
                                source.PlayOneShot(pickupSound);
                        }
                    } else if (hit.collider.CompareTag("key"))
                        {
                        hit.collider.gameObject.transform.position = new Vector3(1000, 1000, 1000);
                        Player._instance.hasKey = true;

                        if (source)
                            source.PlayOneShot(pickupSound);
                    }
                }
            }
    }

    public override void EndGame()
    {
        isGameActive = false;
        _obj.isSelected = false;
        _obj.ResetObject();
    }

    public void Interact()
    {
        Debug.Log("Interacting with the shelf.");
        if (isGameActive)
        {
            OnEndGame();
            EndGame();
        }
        else
        {
            OnStartGame();
            StartGame();
        }
    }

    public override void Start()
    {
    }

    public override void StartGame()
    {
        isGameActive = true;
        _obj =  GameManager.Instance._rightHand.GetComponent<ObjectMovement>();
        _obj.isSelected = true;
    }

    public override void Update()
    {
    }
}
