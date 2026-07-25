using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AntidoteSpawner : MonoBehaviour
{

    public GameObject[] _spawnPoints;

    public GameObject[] _antidoteIngredients;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<int> _used = new List<int>();

        if (_spawnPoints.Length > 0 && _antidoteIngredients.Length > 0 && _spawnPoints.Length >= _antidoteIngredients.Length)
        {
            foreach (GameObject ingredient in _antidoteIngredients)
            {
                int randomIndex = Random.Range(0, _spawnPoints.Length);
                if (_used.Contains(randomIndex))
                {
                    while (_used.Contains(randomIndex))
                    {
                        randomIndex = Random.Range(0, _spawnPoints.Length);
                    }
                }
                _used.Add(randomIndex);
                GameObject antidoteIngredient = Instantiate(ingredient, _spawnPoints[randomIndex].transform.position, Quaternion.identity);
                antidoteIngredient.transform.parent = _spawnPoints[randomIndex].transform.parent;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
