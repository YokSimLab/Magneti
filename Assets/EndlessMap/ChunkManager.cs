using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private float chunkOffset;
    [SerializeField] private GameObject chunk;
    [SerializeField] private Color freeLocationColor = Color.green;
    [SerializeField] private Color occupiedLocationColor = Color.red;
    [SerializeField] private float checkRadius = 0.1f;
    [SerializeField] private float gizmoDisplayDuration = 0.1f;
    [SerializeField] private float gizmoSize = 0.5f;

    public enum Direction
    {
        Top,
        Right,
        Left,
        Bottom
    }

    private Vector3[] topChunkLocations;
    private Vector3[] rightChunkLocations;
    private Vector3[] leftChunkLocations;
    private Vector3[] bottomChunkLocations;
    private Vector3[] allChunkLocations;

    private List<(Vector3 position, bool isFree, float timeStamp)> checkedLocations = new List<(Vector3, bool, float)>();

    private void Awake()
    {
        InitializeChunksLocations();
    }
    private void InitializeChunksLocations()
    {
        topChunkLocations = new Vector3[]
        {
            new Vector3(0, chunkOffset, 0),
            new Vector3(chunkOffset, chunkOffset, 0),
            new Vector3(-chunkOffset, chunkOffset, 0)
        };
        rightChunkLocations = new Vector3[]
        {
            new Vector3(chunkOffset, 0, 0),
            new Vector3(chunkOffset, chunkOffset, 0),
            new Vector3(chunkOffset, -chunkOffset, 0)
        };
        leftChunkLocations = new Vector3[]
        {
            new Vector3(-chunkOffset, 0, 0),
            new Vector3(-chunkOffset, chunkOffset, 0),
            new Vector3(-chunkOffset, -chunkOffset, 0)
        };
        bottomChunkLocations = new Vector3[]
        {
            new Vector3(0, -chunkOffset, 0),
            new Vector3(chunkOffset, -chunkOffset, 0),
            new Vector3(-chunkOffset, -chunkOffset, 0)
        };
        allChunkLocations = new Vector3[]
        {
            new Vector3(0, 0, 0),

            new Vector3(0, chunkOffset, 0),
            new Vector3(0, -chunkOffset, 0),

            new Vector3(chunkOffset, 0, 0),
            new Vector3(chunkOffset, chunkOffset, 0),
            new Vector3(chunkOffset, -chunkOffset, 0),

            new Vector3(-chunkOffset, 0, 0),
            new Vector3(-chunkOffset, -chunkOffset, 0),
            new Vector3(-chunkOffset, chunkOffset, 0),
        };
    }

    public void OnLoadTop()
    {
        if (!GameManager.Instance.isGameFail)
        {
            List<Vector3> freeLocations = CheckWhatLocationsAreFree(transform.position + new Vector3(0, chunkOffset, 0));
            if (freeLocations.Count > 0)
            {
                foreach (Vector3 freeLocation in freeLocations)
                {
                    Instantiate(chunk, freeLocation, quaternion.identity);
                }
            }
        }
    }

    public void OnLoadRight()
    {
        if (!GameManager.Instance.isGameFail)
        {
            List<Vector3> freeLocations = CheckWhatLocationsAreFree(transform.position + new Vector3(chunkOffset, 0, 0));
            if (freeLocations.Count > 0)
            {
                foreach (Vector3 freeLocation in freeLocations)
                {
                    Instantiate(chunk, freeLocation, quaternion.identity);
                }
            }
        }
    }

    public void OnLoadLeft()
    {
        if (!GameManager.Instance.isGameFail)
        {
            List<Vector3> freeLocations = CheckWhatLocationsAreFree(transform.position + new Vector3(-chunkOffset, 0, 0));
            if (freeLocations.Count > 0)
            {
                foreach (Vector3 freeLocation in freeLocations)
                {
                    Instantiate(chunk, freeLocation, quaternion.identity);
                }
            }
        }
    }

    public void OnLoadBottom()
    {
        if (!GameManager.Instance.isGameFail)
        {
            List<Vector3> freeLocations = CheckWhatLocationsAreFree(transform.position + new Vector3(0, -chunkOffset, 0));
            if (freeLocations.Count > 0)
            {
                foreach (Vector3 freeLocation in freeLocations)
                {
                    Instantiate(chunk, freeLocation, quaternion.identity);
                }
            }
        }
    }


    private List<Vector3> CheckWhatLocationsAreFree(Vector3 startLocations)
    {
        // Clear all previous debug visualizations
        checkedLocations.Clear();

        List<Vector3> freeLocations = new List<Vector3>();

        foreach (Vector3 location in allChunkLocations)
        {
            Vector2 combinedLocation = startLocations + location;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(combinedLocation, checkRadius);

            bool isFree = colliders.Length == 0 || !colliders.Any(overlapCollider2D => overlapCollider2D.CompareTag("ChunkManager"));

            checkedLocations.Add((combinedLocation, isFree, Time.time));

            if (isFree)
            {
                freeLocations.Add(combinedLocation);
            }
        }

        return freeLocations;
    }

    private void OnDrawGizmos()
    {
        float currentTime = Time.time;
        checkedLocations.RemoveAll(loc => currentTime - loc.timeStamp > 0.5);

        foreach (var (position, isFree, _) in checkedLocations)
        {
            Gizmos.color = isFree ? freeLocationColor : occupiedLocationColor;
            Gizmos.DrawSphere(position, gizmoSize);
        }
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(ChunkManager))]
    public class ChunkManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ChunkManager chunkManager = (ChunkManager)target;

            if (UnityEditor.EditorGUI.EndChangeCheck())
            {
                UnityEditor.SceneView.RepaintAll();
            }
        }
    }
#endif
}