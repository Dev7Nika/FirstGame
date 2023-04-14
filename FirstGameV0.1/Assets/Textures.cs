using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Textures : MonoBehaviour
{
    public int textureIndex;
    public Texture2D texture;
    public float metallic = 0.0f;
    public float smoothness = 0.5f;

    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        TerrainLayer[] layers = terrain.terrainData.terrainLayers;
        layers[textureIndex].diffuseTexture = texture;
        layers[textureIndex].metallic = metallic;
        layers[textureIndex].smoothness = smoothness;
        terrain.terrainData.terrainLayers = layers;
    }
}