using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private float chunkOffset;
    [SerializeField] private GameObject chunk;

    public void OnTouchBoundaries()
    {
        GameManager.Instance.FailGame();
    }

    public void OnLoadTop()
    {
        Instantiate(chunk, transform.position + new Vector3(0, chunkOffset, 0), quaternion.identity);
    }

    public void OnLoadBottom()
    {
        Instantiate(chunk, transform.position + new Vector3(0, -chunkOffset, 0), quaternion.identity);
    }
}