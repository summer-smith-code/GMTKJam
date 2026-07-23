using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region public resources
    public Camera _camera;
    public CameraMovement _cameraMovement;
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
