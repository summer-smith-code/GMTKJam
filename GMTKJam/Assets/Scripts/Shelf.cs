using UnityEngine;

public class Shelf : MiniGameBase, IInteractable
{
    [SerializeField] private GameObject _hand;
    public override void EndGame()
    {
        isGameActive = false;
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
    }

    public override void Update()
    {
    }
}
