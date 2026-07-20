using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class SpawnManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> spawnedObjects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

        SpawnObjects();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            int x = Random.Range(0,500);
            int y = 50;
            int z = Random.Range(0, 500);

            Vector3 position = new Vector3(x, y, z);

            Instantiate(obj, position, Quaternion.identity);
        }
    }
}