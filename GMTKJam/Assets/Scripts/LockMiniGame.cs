using UnityEngine;

public class LockMiniGame : MiniGameBase, IInteractable
{
    public override void EndGame()
    {
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public override void Start()
    {
        // any set up code for the mini-game can go here
    }

    // can be called by playterinteraction script when player interacts with the lock
    public override void StartGame()
    {
        OnStartGame();
    }

    public override void Update()
    {
    }
}
