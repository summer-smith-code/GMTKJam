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
    public GameObject _RaycastPivot;

    // antidote items
    public bool _magnesium;
    public bool _mint;
    public bool _lime;
    public bool _antidoteBottle;

    public bool isLocked = false;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
}
