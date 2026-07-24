using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    #region public resources
    public Camera _camera;
    public Camera _displayCamera;
    public CameraMovement _cameraMovement;
    public PlayerInput _playerInput;
    public PlayerMovement _playerMovement;
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


        _cameraMovement = _camera.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
