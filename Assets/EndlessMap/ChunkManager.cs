using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private float chunkOffset;
    [SerializeField] private GameObject chunk;

    public enum Direction
    {
        Top,
        Right,
        Left,
        Bottom
    }

    private Vector3[] topChunkLocations;
    private Vector3[] rightChunkLocations;
    private Vector3[] leftChunkLocations;
    private Vector3[] bottomChunkLocations;

    private void Awake()
    {
        InitializeChunksLocations();
    }

    private void InitializeChunksLocations()
    {
        topChunkLocations = new Vector3[]
        {
            new Vector3(0, chunkOffset, 0),
            new Vector3(chunkOffset, chunkOffset, 0),
            new Vector3(-chunkOffset, chunkOffset, 0)
        };
        rightChunkLocations = new Vector3[]
        {
            new Vector3(chunkOffset, 0, 0),
            new Vector3(chunkOffset, chunkOffset, 0),
            new Vector3(chunkOffset, -chunkOffset, 0)
        };
        leftChunkLocations = new Vector3[]
        {
            new Vector3(-chunkOffset, 0, 0),
            new Vector3(-chunkOffset, chunkOffset, 0),
            new Vector3(-chunkOffset, -chunkOffset, 0)
        };
        bottomChunkLocations = new Vector3[]
        {
            new Vector3(0, -chunkOffset, 0),
            new Vector3(chunkOffset, -chunkOffset, 0),
            new Vector3(-chunkOffset, -chunkOffset, 0)
        };
    }

    public void OnLoadTop()
    {
        if (!GameManager.Instance.isGameFail)
        {
            List<Vector3> freeLocations = CheckWhatLocationsAreFree(topChunkLocations);
            if (freeLocations.Count > 0)
            {
                foreach (Vector3 freeLocation in freeLocations)
                {
                    Instantiate(chunk, transform.position + freeLocation, quaternion.identity);
                }
            }
        }
    }

    public void OnLoadRight()
    {
        if (!GameManager.Instance.isGameFail)
        {
            List<Vector3> freeLocations = CheckWhatLocationsAreFree(rightChunkLocations);
            if (freeLocations.Count > 0)
            {
                foreach (Vector3 freeLocation in freeLocations)
                {
                    Instantiate(chunk, transform.position + freeLocation, quaternion.identity);
                }
            }
        }
    }

    public void OnLoadLeft()
    {
        if (!GameManager.Instance.isGameFail)
        {
            List<Vector3> freeLocations = CheckWhatLocationsAreFree(leftChunkLocations);
            if (freeLocations.Count > 0)
            {
                foreach (Vector3 freeLocation in freeLocations)
                {
                    Instantiate(chunk, transform.position + freeLocation, quaternion.identity);
                }
            }
        }
    }

    public void OnLoadBottom()
    {
        if (!GameManager.Instance.isGameFail)
        {
            List<Vector3> freeLocations = CheckWhatLocationsAreFree(bottomChunkLocations);
            if (freeLocations.Count > 0)
            {
                foreach (Vector3 freeLocation in freeLocations)
                {
                    Instantiate(chunk, transform.position + freeLocation, quaternion.identity);
                }
            }
        }
    }

    private List<Vector3> CheckWhatLocationsAreFree(Vector3[] locations)
    {
        List<Vector3> freeLocations = new List<Vector3>();

        foreach (Vector3 location in locations)
        {
            print(transform.position + location);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + location, 0.1f);

            if (colliders.Length == 0 || !colliders.All(overlapCollider2D => overlapCollider2D.CompareTag("ChunkManager")))
            {
                freeLocations.Add(location);
            }
        }

        return freeLocations;
    }
}