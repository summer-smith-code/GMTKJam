using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AntidoteSpawner : MonoBehaviour
{

    public GameObject[] _mintSpawnPoints;
    public GameObject[] _magnesiumSpawnPoints;
    public GameObject[] _limeSpawnPoints;
    public GameObject[] _bottleSpawnPoints;

    public GameObject _mint;
    public GameObject _magnesium;
    public GameObject _lime;
    public GameObject _bottle;

    public Dictionary<GameObject, GameObject[]> antidotes = new Dictionary<GameObject, GameObject[]>();



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        antidotes.Add(_lime, _limeSpawnPoints);
        antidotes.Add(_bottle, _bottleSpawnPoints);
        antidotes.Add(_magnesium, _magnesiumSpawnPoints);
        antidotes.Add(_mint, _mintSpawnPoints);

        foreach (KeyValuePair<GameObject, GameObject[]> key in antidotes)
        {
            Spawn(key.Key, key.Value);
        }
    }


    public void Spawn(GameObject antidote, GameObject[] spawnPoints)
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        GameObject _antidote = Instantiate(antidote, spawnPoints[randomIndex].transform.position, Quaternion.identity);
        _antidote.transform.parent = spawnPoints[randomIndex].transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
