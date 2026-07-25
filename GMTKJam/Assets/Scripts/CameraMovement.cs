using Unity.Cinemachine;
using UnityEditor.PackageManager;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // lock the camera movement when mini-game is active

    [SerializeField] private CinemachineInputAxisController _inputController;
    [SerializeField] private CinemachinePanTilt _panTilt;

// Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LockCamera(bool isLocked)
    {
        _inputController.enabled = !isLocked;
    }

    public void LookAtObject(GameObject obj)
    {
        if (_inputController.enabled)
        {
            _inputController.enabled = false;
        }
        _panTilt.PanAxis.Value = obj.transform.eulerAngles.y;


        _panTilt.TiltAxis.Value = 0;
    }
}
