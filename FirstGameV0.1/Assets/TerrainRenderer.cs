using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainRenderer : MonoBehaviour
{
    public Transform playerTransform;
    public float renderDistance = 100f;
    public int chunkSize = 10;
    public int chunkRenderDistance = 2;
    public Terrain terrain;

    private Vector2Int playerChunkPosition;
    private Vector2Int[,] chunkPositions;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerChunkPosition = GetChunkPosition(playerTransform.position);
        GenerateChunkPositions();
    }

    private void Update()
    {
        // Update the player's current chunk position
        playerChunkPosition = GetChunkPosition(playerTransform.position);

        // Disable game objects that are outside the render distance
        DisableGameObjects();

        // Only render the terrain within the chunk render distance
        RenderTerrainChunks();
    }

    private void DisableGameObjects()
    {
        // Loop through all game objects in the scene
        foreach (GameObject go in FindObjectsOfType<GameObject>())
        {
            // Check if the game object has a renderer component
            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Calculate the distance from the player to the game object
                float distance = Vector3.Distance(playerTransform.position, go.transform.position);

                // Disable the renderer if the game object is outside the render distance
                if (distance > renderDistance)
                {
                    renderer.enabled = false;
                }
                // Enable the renderer if the game object is inside the render distance
                else
                {
                    renderer.enabled = true;
                }
            }
        }
    }

    private void RenderTerrainChunks()
    {
        // Get the player's current chunk position
        Vector2Int currentChunkPosition = GetChunkPosition(playerTransform.position);

        // Only render the terrain within the chunk render distance
        for (int x = -chunkRenderDistance; x <= chunkRenderDistance; x++)
        {
            for (int z = -chunkRenderDistance; z <= chunkRenderDistance; z++)
            {
                // Get the chunk position
                Vector2Int chunkPosition = currentChunkPosition + new Vector2Int(x, z);

                // Only render the chunk if it's within the terrain bounds
                if (chunkPosition.x >= 0 && chunkPosition.x < chunkPositions.GetLength(0)
                    && chunkPosition.y >= 0 && chunkPosition.y < chunkPositions.GetLength(1))
                {
                    // Calculate the distance from the player to the chunk
                    float distance = Vector2.Distance(playerChunkPosition, chunkPosition);

                    // Only render the chunk if it's within the chunk render distance
                    if (distance <= chunkRenderDistance)
                    {
                        // Enable rendering of the terrain for this chunk
                        terrain.SetNeighbors(null, null, null, null);
                        terrain.enabled = false;
                        terrain.transform.position = new Vector3(chunkPositions[chunkPosition.x, chunkPosition.y].x, 0f, chunkPositions[chunkPosition.x, chunkPosition.y].y);
                        terrain.enabled = true;
                    }
                    // Disable rendering of the terrain for this chunk
                    else
                    {
                        terrain.enabled = false;
                    }
                }
            }
        }
    }

    private void GenerateChunkPositions()
    {
        // Get the size of the terrain in world units
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;

        // Calculate the number of chunks in each direction
        int numChunksX = Mathf.CeilToInt(terrainWidth / chunkSize);
        int numChunksZ = Mathf.CeilToInt(terrainLength / chunkSize);

        // Create a 2D array to store the chunk positions
        chunkPositions = new Vector2Int[numChunksX, numChunksZ];

        // Loop through all the chunks and calculate their positions
        for (int x = 0; x < numChunksX; x++)
        {
            for (int z = 0; z < numChunksZ; z++)
            {
                // Calculate the center position of the chunk
                float centerX = (x * chunkSize) + (chunkSize / 2f);
                float centerZ = (z * chunkSize) + (chunkSize / 2f);

                // Calculate the position of the chunk relative to the terrain
                float relativeX = centerX - (terrainWidth / 2f);
                float relativeZ = centerZ - (terrainLength / 2f);

                // Store the chunk position in the array
                chunkPositions[x, z] = new Vector2Int(Mathf.RoundToInt(relativeX), Mathf.RoundToInt(relativeZ));
            }
        }
    }
    private Vector2Int GetChunkPosition(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x / chunkSize);
        int z = Mathf.FloorToInt(position.z / chunkSize);
        return new Vector2Int(x, z);
    }
}

