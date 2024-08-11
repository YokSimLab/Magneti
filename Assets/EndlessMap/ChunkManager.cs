using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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

    public void OnLoadTop()
    {
        if (!GameManager.Instance.isGameFail && CheckIfDoesntExist(Direction.Top))
        {
            Instantiate(chunk, transform.position + new Vector3(0, chunkOffset, 0), quaternion.identity);
        }
    }

    public void OnLoadRight()
    {
        if (!GameManager.Instance.isGameFail && CheckIfDoesntExist(Direction.Right))
        {
            Instantiate(chunk, transform.position + new Vector3(chunkOffset, 0, 0), quaternion.identity);
        }
    }

    public void OnLoadLeft()
    {
        if (!GameManager.Instance.isGameFail && CheckIfDoesntExist(Direction.Left))
        {
            Instantiate(chunk, transform.position + new Vector3(-chunkOffset, 0, 0), quaternion.identity);
        }
    }

    public void OnLoadBottom()
    {
        if (!GameManager.Instance.isGameFail && CheckIfDoesntExist(Direction.Bottom))
        {
            Instantiate(chunk, transform.position + new Vector3(0, -chunkOffset, 0), quaternion.identity);
        }
    }

    private bool CheckIfDoesntExist(Direction direction)
    {
        Vector3 chunkVectorDirection;
        switch (direction)
        {
            case Direction.Top:
                chunkVectorDirection = new Vector3(0, chunkOffset, 0);
                break;
            case Direction.Right:
                chunkVectorDirection = new Vector3(chunkOffset, 0, 0);
                break;
            case Direction.Left:
                chunkVectorDirection = new Vector3(-chunkOffset, 0, 0);
                break;
            case Direction.Bottom:
                chunkVectorDirection = new Vector3(0, -chunkOffset, 0);
                break;
            default:
                return false;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + chunkVectorDirection, 0.1f);

        return colliders.Length == 0;
    }
}