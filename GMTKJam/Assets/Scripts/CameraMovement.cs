using Unity.Cinemachine;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // lock the camera movement when mini-game is active

    [SerializeField] private CinemachineInputAxisController _inputController;

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
}
