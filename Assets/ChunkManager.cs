using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private GameObject chunk;
    [SerializeField] private float chunkOffset;

    public void OnTouchBoundaries()
    {
        GameManager.Instance.FailGame();
    }

    public void OnLoadTop()
    {
        Instantiate(chunk);
        chunk.transform.position = transform.position + new Vector3(0, chunkOffset, 0);
    }

    public void OnBottomTop()
    {
        Instantiate(chunk);
        chunk.transform.position = transform.position + new Vector3(0, -chunkOffset, 0);
    }
}
