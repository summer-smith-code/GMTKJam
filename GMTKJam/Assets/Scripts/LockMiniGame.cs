using TMPro.EditorUtilities;
using UnityEngine;

public class LockMiniGame : MiniGameBase, IInteractable
{
    public bool LockCamera { get; set; } = true;
    [SerializeField] GameObject key;
    [SerializeField] private HingeJoint _hingeJoint;
    private ObjectMovement _obj;
    private bool isLocked = true;

    public override void EndGame()
    {
        OnEndGame();
        _obj.isSelected = false;
        _obj.ResetObject();
        isGameActive = false;
    }

    public void Interact()
    {
        Debug.Log("Interacted with LockMiniGame");
        if (isLocked)
        {
            if (isGameActive)
            {
                EndGame();
            }
            else
            {
                StartGame();
            }
        }
        else
        {
            Debug.Log("Unlocked!");
        }
    }

    public override void Start()
    {
        // any set up code for the mini-game can go here
    }

    // can be called by playterinteraction script when player interacts with the lock
    public override void StartGame()
    {
        if (key != null)
        {
            _obj = key.GetComponent<ObjectMovement>();
            _obj.isSelected = true;
            key.transform.position = this.gameObject.transform.position + -this.gameObject.transform.right * .5f;
            key.transform.position = new Vector3(key.transform.position.x, GameManager.Instance._fpCamera.transform.position.y, key.transform.position.z);
        } else
        {
            // player cannot play without key!
        }
            OnStartGame();
        isGameActive = true;
    }

    public override void Update()
    {
    }

    public void Click()
    {
        if (isGameActive)
        if (_obj != null)
            {
            RaycastHit hit;
                if (Physics.Raycast(_obj.transform.position, _obj.transform.forward, out hit))
                {
                    Debug.Log("Hit object: " + hit.collider.name);  
                    Lock _lock = hit.collider.GetComponent<Lock>();
                    if (_lock != null)
                    {
                        this.GetComponent<HingeJoint>().limits = _hingeJoint.limits;
                        EndGame();
                    }
                }
            }
    }
}
