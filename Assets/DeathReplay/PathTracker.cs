using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class PathTracker : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LineRenderer trackerLine;
    [SerializeField] private float lastPathPointThreshold = .5f;
    [SerializeField] private CinemachineVirtualCamera cineMachineCamera;


    private List<Vector3> pathList;

    private void Awake()
    {
        pathList = new List<Vector3>();
    }

    private void Start()
    {
        pathList.Clear();
        pathList.Add(playerTransform.position);
        RefreshtrackerLine();
    }

    void Update()
    {
        AddToList();
    }

    private void AddToList()
    {
        Vector3 lastPathPoint = pathList[^1];

        if (Vector3.Distance(playerTransform.position, lastPathPoint) > lastPathPointThreshold)
        {
            pathList.Add(playerTransform.position);
            RefreshtrackerLine();
        }
    }

    private void RefreshtrackerLine()
    {
        if (!GameManager.Instance.isGameFail)
        {
            trackerLine.positionCount = pathList.Count;
        }
    }

    public void ShowDeathReplay()
    {
        playerTransform.GetComponent<PolygonCollider2D>().isTrigger = true;
        playerTransform.GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(false);

        trackerLine.SetPositions(pathList.ToArray());
        StartCoroutine(DeathReplaySmooth(pathList.Count));
    }

    private IEnumerator DeathReplaySmooth(int pathListCount)
    {
        // Adjust this value to make the DeathReplay faster or slower
        const int densifyTimes = 4;

        cineMachineCamera.Follow = null;
        List<Vector3> densePath = pathList;

        for (int i = 0; i < densifyTimes; i++)
        {
            densePath = DensifyPath(densePath);
        }

        float totalDistance = 0f;

        for (int i = 1; i < densePath.Count; i++)
        {
            totalDistance += Vector3.Distance(densePath[i], densePath[i - 1]);
        }

        // Adjust this values to make the DeathReplay faster or slower
        const float baseSpeed = 0.023f;
        const float speedUpPhase = 0.75f;
        const float slowDownPhase = 0.15f;
        const float maxSpeedMultiplier = 0.5f;
        const float minSpeedMultiplier = 0.05f;

        int currentIndex = densePath.Count - 1;

        float currentSpeed = baseSpeed;
        while (currentIndex > 0)
        {
            float pathProgress = (float)currentIndex / densePath.Count;

            if (pathProgress > speedUpPhase)
            {
                print("speed Up Phase  ");
                currentSpeed = baseSpeed * Mathf.Lerp(1f, maxSpeedMultiplier, (pathProgress - speedUpPhase) / (1f - speedUpPhase));
            }
            else if (pathProgress < slowDownPhase)
            {
                print("slow Down Phase  ");

                currentSpeed = baseSpeed * Mathf.Lerp(minSpeedMultiplier, 1f, pathProgress / slowDownPhase);
            }
            else
            {
                currentSpeed = baseSpeed;
            }

            int pointsToJump = Mathf.Max(1, Mathf.RoundToInt(currentSpeed * densePath.Count / totalDistance));

            currentIndex -= pointsToJump;
            currentIndex = Mathf.Max(0, currentIndex);

            cineMachineCamera.transform.position = new Vector3(
                densePath[currentIndex].x,
                densePath[currentIndex].y,
                -10f
            );

            playerTransform.position = new Vector3(densePath[currentIndex].x, densePath[currentIndex].y, -10f);
            yield return null;
        }
    }

    private List<Vector3> DensifyPath(List<Vector3> inputPath)
    {
        List<Vector3> densePoints = new List<Vector3>();
        for (int i = 0; i < inputPath.Count - 2; i++)
        {
            densePoints.Add(inputPath[i]);
            densePoints.Add((inputPath[i] + inputPath[i + 1]) / 2);
        }

        return densePoints;
    }
}