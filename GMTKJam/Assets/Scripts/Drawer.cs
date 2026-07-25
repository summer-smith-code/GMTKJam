using System.Net;
using UnityEngine;

public class Drawer : MonoBehaviour, IInteractable
{
    private bool _hasMoved = true;
    private Vector3 _startPos;
    private Vector3 _endPos;
    private float _speed = 1f;
    private float _offset = 0.2f;

    private float _time = 0f;

    public bool LockCamera { get; set; } = false;

    public void Interact()
    {
        if (_hasMoved)
        {
            Debug.Log("Moving back!");
            _hasMoved = false;
            _time = 0f;
        }
        else
        {
            Debug.Log("Moving forward!");
            _hasMoved = true;
            _time = 0f;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPos = this.transform.position;
        _endPos = this.transform.position + -this.transform.forward * _offset;
        Debug.Log(_startPos);
        Debug.Log(_endPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasMoved)
        {
            transform.position = Vector3.Lerp(_endPos, _startPos, _time);
            
        } else
        {
            transform.position = Vector3.Lerp(_startPos, _endPos, _time);
        }
        _time += Time.deltaTime * _speed;
        _time = Mathf.Clamp01(_time);
    } 
}
