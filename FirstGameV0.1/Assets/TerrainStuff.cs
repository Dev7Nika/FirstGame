using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainStuff : MonoBehaviour
{
    public GameObject rockPrefab;
    public GameObject treePrefab;
    public Terrain terrain;
    public int rockCount = 100;
    public int treeCount = 50;

    void Start()
    {
        GenerateRocks();
        GenerateTrees();
    }

    void GenerateRocks()
    {
        for (int i = 0; i < rockCount; i++)
        {
            float x = Random.Range(0f, 1f) * terrain.terrainData.size.x;
            float z = Random.Range(0f, 1f) * terrain.terrainData.size.z;
            float y = terrain.SampleHeight(new Vector3(x, 0, z));
            Vector3 position = new Vector3(x, y, z);
            Instantiate(rockPrefab, position, Quaternion.identity);
        }
    }

    void GenerateTrees()
    {
        for (int i = 0; i < treeCount; i++)
        {
            float x = Random.Range(0f, 1f) * terrain.terrainData.size.x;
            float z = Random.Range(0f, 1f) * terrain.terrainData.size.z;
            float y = terrain.SampleHeight(new Vector3(x, 0, z));
            Vector3 position = new Vector3(x, y, z);
            Instantiate(treePrefab, position, Quaternion.identity);
        }
    }
}