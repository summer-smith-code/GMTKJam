using System.Collections;
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
        if (Player._instance.hasKey)
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
                GameManager.Instance._cameraMovement.LockCamera(false);
            }
        } else
        {
            Debug.Log("You need a key to interact with this lock.");
            GameManager.Instance._text.text = "You need a key to unlock this door.";
            StartCoroutine(Wait());
            GameManager.Instance._cameraMovement.LockCamera(false);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        GameManager.Instance._text.text = "";
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
            key.transform.rotation = Quaternion.LookRotation(this.gameObject.transform.right);
            key.transform.rotation = Quaternion.Euler(key.transform.rotation.x, key.transform.rotation.y+180, key.transform.rotation.z + 90);
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
                        Destroy(key);
                    }
                }
            }
    }
}
