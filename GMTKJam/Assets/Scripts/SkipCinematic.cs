using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SkipCinematic : MonoBehaviour
{
    public bool skipActive;
    public TMP_Text skipText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (!skipActive)
            {
                skipActive = true;
                skipText.text = "Press 'Space' to skip...";
            }
            else
            {
                SceneManager.LoadScene("House");
            }
        }
    }
}
