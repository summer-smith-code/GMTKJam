using NUnit.Framework.Constraints;
using UnityEngine;

public class Shelf : MiniGameBase, IInteractable
{
    [SerializeField] private GameObject _hand;
    private ObjectMovement _obj;

    public bool LockCamera { get; set; } = true;

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
