using System;
using System.Collections;
using UnityEngine;

public class TypeScroll : MonoBehaviour
{

    [SerializeField] private float delay;
    [SerializeField] private string fullText;
    [SerializeField] private string currentText;

    TMPro.TextMeshProUGUI textMeshPro;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    IEnumerator ShowText()
    {
        Debug.Log("Working!");
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textMeshPro.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }

    private void OnEnable()
    {
        Debug.Log("Enabled!");
        textMeshPro = GetComponent<TMPro.TextMeshProUGUI>();
        StartCoroutine(ShowText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
