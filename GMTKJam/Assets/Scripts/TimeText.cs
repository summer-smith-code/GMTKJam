using TMPro;
using UnityEngine;

public class TimeText : MonoBehaviour
{

    TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "Your time was: " + GameManager.Instance.timeSinceStart.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
