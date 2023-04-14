using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [Range(0, 1)]
    public float roughness = 0.5f;
    public float scale = 1f;
    public int octaves = 3;
    public float persistence = 0.5f;
    public float lacunarity = 2f;

    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        TerrainData terrainData = terrain.terrainData;
        int resolution = terrainData.heightmapResolution;

        float[,] heights = terrainData.GetHeights(0, 0, resolution, resolution);

        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float noise = 0f;
                float frequency = 1f / scale;
                float amplitude = 1f;

                for (int i = 0; i < octaves; i++)
                {
                    float xCoord = (float)x / resolution * scale * frequency;
                    float yCoord = (float)y / resolution * scale * frequency;

                    noise += Mathf.PerlinNoise(xCoord, yCoord) * amplitude;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                }

                heights[x, y] = noise * roughness;
            }
        }

        terrainData.SetHeights(0, 0, heights);
    }
}
