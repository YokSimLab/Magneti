using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private GameObject chunk;
    [SerializeField] private float checkRadius = 0.1f;

    private Vector3[] allChunkLocations;

    public static float chunkOffset = 5;

    public enum Direction
    {
        Top,
        Right,
        Left,
        Bottom
    }

    private void Awake()
    {
        InitializeChunksLocations();
    }

    private void InitializeChunksLocations()
    {
        allChunkLocations = new Vector3[]
        {
            new Vector3(0, 0, 0),

            new Vector3(0, chunkOffset, 0),
            new Vector3(0, -chunkOffset, 0),

            new Vector3(chunkOffset, 0, 0),
            new Vector3(chunkOffset, chunkOffset, 0),
            new Vector3(chunkOffset, -chunkOffset, 0),

            new Vector3(-chunkOffset, 0, 0),
            new Vector3(-chunkOffset, -chunkOffset, 0),
            new Vector3(-chunkOffset, chunkOffset, 0),
        };
    }

    public void OnLoadChunk(Vector3 chunkSpawnOrigin)
    {
        if (!GameManager.Instance.isGameFail)
        {
            List<Vector3> freeLocations = CheckWhatLocationsAreFree(chunkSpawnOrigin);
            if (freeLocations.Count > 0)
            {
                foreach (Vector3 freeLocation in freeLocations)
                {
                    Instantiate(chunk, freeLocation, quaternion.identity);
                }
            }
        }
    }

    private List<Vector3> CheckWhatLocationsAreFree(Vector3 startLocations)
    {
        List<Vector3> freeLocations = new List<Vector3>();

        foreach (Vector3 location in allChunkLocations)
        {
            Vector2 combinedLocation = startLocations + location;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(combinedLocation, checkRadius);

            if (colliders.Length == 0 || !colliders.Any(overlapCollider2D => overlapCollider2D.CompareTag("ChunkManager")))
            {
                freeLocations.Add(combinedLocation);
            }
        }

        return freeLocations;
    }
}