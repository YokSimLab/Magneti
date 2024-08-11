using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkDeath : MonoBehaviour
{
    [SerializeField] private ChunkManager chunkManager;

    private void OnCollisionEnter2D(Collision2D other)
    {
        chunkManager.OnTouchBoundaries();
    }
}