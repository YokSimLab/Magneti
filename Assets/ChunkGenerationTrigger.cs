using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerationTrigger : MonoBehaviour
{
    [SerializeField] private ChunkManager chunkManager;
    [SerializeField] private bool isTop = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isTop)
            {
                chunkManager.OnLoadTop();
            }
            else
            {
                chunkManager.OnLoadTop();
            }
        }
    }
}
