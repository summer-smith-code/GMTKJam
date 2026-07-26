using UnityEngine;

public class KitchenDrawer : MonoBehaviour, IInteractable
{
    public enum DrawerPivot
    {
        Left, Right, Up, Down
    }
    [SerializeField] private DrawerPivot pivot;
    private Vector3 axis;
    private Quaternion offsetRotation;
    private Quaternion newRotation;

    public bool LockCamera { get; set; } = false;

    public AudioSource source;
    public AudioClip[] interactSounds;
    public AudioClip pickupSound;

    [SerializeField] bool _hasObject;
    private bool _hasMoved = false;
    private bool _initialMove = false;
    private Quaternion _startPos;
    private Quaternion _endPos;
    private Quaternion _currentPos;
    private float _speed = 1f;
    private float _offset = 0.2f;

    private float _time = 0f;


    public void Click()
    {
    }

    public void Interact()
    {
        source.pitch = Random.Range(0.9f, 1.1f);

        _currentPos = transform.rotation;
        if (!_hasObject)
            _hasObject = this.transform.childCount > 1;
        if (_hasObject && this.transform.childCount > 1)
        {
            this.transform.GetChild(1).SetParent(this.transform.parent);
        }
        if (_hasMoved)
        {
            if (_hasObject && _initialMove)
            {
                Debug.Log("Destroying object!");
                Destroy(this.transform.parent.GetChild(1).gameObject);
                Player._instance._lime = true;
                _hasObject = false;

                if (source)
                    source.PlayOneShot(pickupSound);
            }
            else
            {
                Debug.Log("Moving back!");
                _hasMoved = false;
                _time = 0f;

                if(source)
                    source.PlayOneShot(interactSounds[Random.Range(0, interactSounds.Length)]);
            }
        }
        else
        {
            if (source)
                source.PlayOneShot(interactSounds[Random.Range(0, interactSounds.Length)]);

            Debug.Log("Moving forward!");
            _hasMoved = true;
            _time = 0f;
            _initialMove = true;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPos = this.transform.rotation;
        switch (pivot)
        {
            case DrawerPivot.Left:
                axis = Vector3.up;
                newRotation = Quaternion.AngleAxis(90f, axis) * _startPos;
                _endPos = newRotation;
                break;
            case DrawerPivot.Right:
                axis = Vector3.up;
                newRotation = Quaternion.AngleAxis(-90f, axis) * _startPos;
                _endPos = newRotation;
                break;
            case DrawerPivot.Up:
                axis = Vector3.right;
                newRotation = Quaternion.AngleAxis(90f, axis) * _startPos;
                _endPos = newRotation;
                break;
            case DrawerPivot.Down:
                axis = Vector3.back;
                newRotation = Quaternion.AngleAxis(-90f, axis) * _startPos;
                _endPos = newRotation;
                break;
        }
        Debug.Log(_startPos);
        Debug.Log(_endPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasMoved)
        {
            transform.rotation = Quaternion.Lerp(_currentPos, _endPos, _time);

        }
        else if (_currentPos != _startPos)
        {
            transform.rotation = Quaternion.Lerp(_currentPos, _startPos, _time);
        }
        _time += Time.deltaTime * _speed;
        _time = Mathf.Clamp01(_time);

    }
}
