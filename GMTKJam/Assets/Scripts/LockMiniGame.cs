using UnityEngine;

public class LockMiniGame : MiniGameBase, IInteractable
{
    public bool LockCamera { get; set; } = true;
    [SerializeField] GameObject key;
    private ObjectMovement _obj;

    public override void EndGame()
    {
        OnEndGame();
    }

    public void Interact()
    {
        if (isGameActive)
        {
            EndGame();
        } else
        {
            StartGame();
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
            key.transform.position = GameManager.Instance._rightHand.transform.position;
        } else
        {
            // player cannot play without key!
        }
            OnStartGame();
    }

    public override void Update()
    {
    }
}
