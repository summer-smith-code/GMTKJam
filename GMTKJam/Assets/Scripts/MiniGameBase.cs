using UnityEngine;

public abstract class MiniGameBase : MonoBehaviour
{
    // base abstract class for all mini-games
    protected void OnStartGame()
    {
        GameManager.Instance._cameraMovement.LockCamera(true);
    }

    public abstract void Start();

    public abstract void Update();

    protected bool isGameActive = false;
    public abstract void StartGame();
    public abstract void EndGame();
}
