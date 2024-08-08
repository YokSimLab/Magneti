using System;
using System.Collections;
using UnityEngine;

public class ChunkGenerationTrigger : MonoBehaviour
{
    [SerializeField] private ChunkManager chunkManager;
    [SerializeField] private bool isTop = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTop)
        {
            chunkManager.OnLoadTop();
        }
        else
        {
            chunkManager.OnLoadBottom();
        }
    }
}