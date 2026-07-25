using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player _instance;

    public PlayerInput _input;
    public PlayerMovement _movement;
    public CameraMovement _cameraMovement;
    public Shiver _camShiver;
    public Shiver _handShiver;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
}
