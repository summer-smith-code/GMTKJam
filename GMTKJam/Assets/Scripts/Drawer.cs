using System.Net;
using UnityEngine;

public class Drawer : MonoBehaviour, IInteractable
{
    [SerializeField] bool _hasObject;
    private bool _hasMoved = false;
    private bool _initialMove = false;
    private Vector3 _startPos;
    private Vector3 _endPos;
    private Vector3 _currentPos;
    private float _speed = 1f;
    private float _offset = 0.2f;

    public AudioSource source;
    public AudioClip openSound;
    public AudioClip closeSound;

    private float _time = 0f;

    public bool LockCamera { get; set; } = false;

    public void Click()
    {
    }

    public void Interact()
    {
        _currentPos = transform.position;
        _hasObject = this.transform.childCount > 1;
        if (_hasMoved)
        {
            if (_hasObject && _initialMove)
            {
                Debug.Log("Destroying object!");
                Destroy(this.transform.GetChild(1).gameObject);
                _hasObject = false;
            } else
            {
                Debug.Log("Moving back!");
                _hasMoved = false;
                _time = 0f;

                if (source)
                    source.PlayOneShot(closeSound);
            }
        }
        else
        {
            if (source)
                source.PlayOneShot(openSound);

            Debug.Log("Moving forward!");
            _hasMoved = true;
            _time = 0f;
            _initialMove = true;
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
            transform.position = Vector3.Lerp(_currentPos, _endPos, _time);
            
        } else
        {
            transform.position = Vector3.Lerp(_currentPos, _startPos, _time);
        }
        _time += Time.deltaTime * _speed;
        _time = Mathf.Clamp01(_time);
    } 
}
