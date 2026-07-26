using UnityEngine;

public class Planter : MonoBehaviour, IInteractable
{
    [SerializeField] bool _hasObject;
    public bool LockCamera { get; set; } = false;

    public AudioSource source;
    public AudioClip pickup;

    public void Click()
    {
    }

    public void Interact()
    {
        _hasObject = this.transform.childCount > 1;
        if (_hasObject)
        {
            if (source)
                source.PlayOneShot(pickup);

            Destroy(this.transform.GetChild(1).gameObject);
            Player._instance._mint = true;
            _hasObject = false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
