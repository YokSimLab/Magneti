using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkDeath : MonoBehaviour
{
    [SerializeField] private ChunkManager _chunkManager;

    private void OnCollisionEnter2D(Collision2D other)
    {
        _chunkManager.OnTouchBoundaries();
    }
}