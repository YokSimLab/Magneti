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
                if (other.GetComponent<Rigidbody2D>().velocity.y > 0)
                    chunkManager.OnLoadChunk(transform.position + new Vector3(0, ChunkManager.chunkOffset, 0));
                else if (GameManager.Instance.isGameFail)
                    chunkManager.OnLoadChunk(transform.position + new Vector3(0, ChunkManager.chunkOffset, 0));
                break;

            case ChunkManager.Direction.Right:
                if (other.GetComponent<Rigidbody2D>().velocity.x > 0)
                    chunkManager.OnLoadChunk(transform.position + new Vector3(ChunkManager.chunkOffset, 0, 0));
                else if (GameManager.Instance.isGameFail)
                    chunkManager.OnLoadChunk(transform.position + new Vector3(ChunkManager.chunkOffset, 0, 0));
                break;

            case ChunkManager.Direction.Left:
                if (other.GetComponent<Rigidbody2D>().velocity.x < 0)
                    chunkManager.OnLoadChunk(transform.position + new Vector3(-ChunkManager.chunkOffset, 0, 0));
                else if (GameManager.Instance.isGameFail)
                    chunkManager.OnLoadChunk(transform.position + new Vector3(-ChunkManager.chunkOffset, 0, 0));
                break;

            case ChunkManager.Direction.Bottom:
                if (other.GetComponent<Rigidbody2D>().velocity.y < 0)
                    chunkManager.OnLoadChunk(transform.position + new Vector3(0, -ChunkManager.chunkOffset, 0));
                else if (GameManager.Instance.isGameFail)
                    chunkManager.OnLoadChunk(transform.position + new Vector3(0, -ChunkManager.chunkOffset, 0));
                break;
        }
    }
}