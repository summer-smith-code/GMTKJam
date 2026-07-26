using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Cinematic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void OnEnable()
    {
        SceneManager.LoadScene(1);
    }
    private void OnDisable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
