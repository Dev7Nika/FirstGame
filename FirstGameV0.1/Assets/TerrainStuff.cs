using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainStuff : MonoBehaviour
{
    public GameObject rockPrefab;
    public GameObject treePrefab;
    public GameObject grassPrefab;
    public Terrain terrain;
    public int rockCount = 100;
    public int treeCount = 50;
    public float grassDensity = 0.1f; // density of grass per square meter
    public int minPatchSize = 10;
    public int maxPatchSize = 30;
    public int minGrassPerPatch = 10;
    public int maxGrassPerPatch = 50;

    void Start()
    {
        GenerateRocks();
        GenerateTrees();
        GenerateGrass();
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

    void GenerateGrass()
    {
        List<Vector3> positions = new List<Vector3>();
        float terrainSize = terrain.terrainData.size.x * terrain.terrainData.size.z;
        int totalGrassCount = (int)(grassDensity * terrainSize);
        int grassPerPatch = Mathf.RoundToInt(totalGrassCount / (float)(treeCount + rockCount));
        while (positions.Count < totalGrassCount)
        {
            int patchSize = Random.Range(minPatchSize, maxPatchSize + 1);
            float x = Random.Range(0f, 1f) * terrain.terrainData.size.x;
            float z = Random.Range(0f, 1f) * terrain.terrainData.size.z;
            float y = terrain.SampleHeight(new Vector3(x, 0, z));
            Vector3 patchCenter = new Vector3(x, y, z);
            for (int i = 0; i < grassPerPatch; i++)
            {
                Vector3 position = Vector3.zero;
                bool positionFound = false;
                int tries = 0;
                while (!positionFound && tries < 20)
                {
                    float offsetX = Random.Range(-1f, 1f) * patchSize / 2f;
                    float offsetZ = Random.Range(-1f, 1f) * patchSize / 2f;
                    position = patchCenter + new Vector3(offsetX, 0f, offsetZ);
                    if (!positions.Contains(position))
                    {
                        Vector3 terrainPos = new Vector3(position.x, terrain.transform.position.y, position.z);
                        float terrainHeight = terrain.SampleHeight(terrainPos);
                        float slopeAngle = terrain.terrainData.GetSteepness(terrainPos.x, terrainPos.y);
                        if (slopeAngle < 30f)
                        {
                            float noise = Mathf.PerlinNoise(position.x / 10f, position.z / 10f);
                            if (noise > 0.5f)
                            {
                                positionFound = true;
                            }
                        }
                    }
                    tries++;
                }
                if (positionFound)
                {
                    positions.Add(position);
                    Vector3 terrainPos = new Vector3(position.x, terrain.transform.position.y, position.z);
                    float terrainHeight = terrain.SampleHeight(terrainPos);
                    Vector3 grassPos = new Vector3(position.x, terrainHeight, position.z);
                    Quaternion rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                    Instantiate(grassPrefab, grassPos, rotation);
                }
            }
        }
    }
}