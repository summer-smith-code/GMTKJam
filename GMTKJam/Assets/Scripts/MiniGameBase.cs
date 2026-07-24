using UnityEngine;

public abstract class MiniGameBase : MonoBehaviour
{
    // base abstract class for all mini-games
    protected void OnStartGame()
    {
        GameManager.Instance._cameraMovement.LockCamera(true);
        GameManager.Instance.playerInput.actions["Move"].Disable();
        Debug.Log("Lock camera");
    }
    protected void OnEndGame()
    {
        GameManager.Instance._cameraMovement.LockCamera(false);
        GameManager.Instance.playerInput.actions["Move"].Enable();
        Debug.Log("Unlock camera");
    }

    public abstract void Start();

    public abstract void Update();

    protected bool isGameActive = false;
    public abstract void StartGame();
    public abstract void EndGame();
}
