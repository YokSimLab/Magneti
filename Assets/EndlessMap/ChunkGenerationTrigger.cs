using System;
using System.Collections;
using UnityEngine;

public class ChunkGenerationTrigger : MonoBehaviour
{
    [SerializeField] private ChunkManager chunkManager;

    [SerializeField] private ChunkManager.Direction direction;


    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (direction)
        {
            case ChunkManager.Direction.Top:
                chunkManager.OnLoadTop();
                break;
            case ChunkManager.Direction.Right:
                chunkManager.OnLoadRight();
                break;
            case ChunkManager.Direction.Left:
                chunkManager.OnLoadLeft();
                break;
            case ChunkManager.Direction.Bottom:
                chunkManager.OnLoadBottom();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}