using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    #region public resources
    [Header("Cameras")]
    public Camera _camera;
    public Camera _displayCamera;
    public CameraMovement _cameraMovement;
    public GameObject _fpCamera;
    public CinemachineInputAxisController _axisController;
    [Header("Player Input")]
    public PlayerInput _playerInput;
    public PlayerMovement _playerMovement;
    public GameObject _rightHand;
    #endregion
    public static GameManager Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
