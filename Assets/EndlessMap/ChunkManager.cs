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
        if (!GameManager.Instance.IsGameFail && CheckIfDoesntExist(true))
        {
            Instantiate(chunk, transform.position + new Vector3(0, chunkOffset, 0), quaternion.identity);
        }
    }

    public void OnLoadBottom()
    {
        if (!GameManager.Instance.IsGameFail && CheckIfDoesntExist(false))
        {
            Instantiate(chunk, transform.position + new Vector3(0, -chunkOffset, 0), quaternion.identity);
        }
    }

    private bool CheckIfDoesntExist(bool isTop)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(0, isTop ? chunkOffset : -chunkOffset, 0), 0.1f);

        return colliders.Length == 0;
    }
}