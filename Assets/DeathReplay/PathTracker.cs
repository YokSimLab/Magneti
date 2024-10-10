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
    [SerializeField] private Image deathImage;
    [SerializeField] private CinemachineVirtualCamera cineMachineCamera;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float zoomOutSpeed = 3f;


    private List<Vector3> pathList;

    private void Awake()
    {
        pathList = new List<Vector3>();
        deathImage.gameObject.SetActive(false);
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

    public void ShowTrackerLine()
    {
        trackerLine.SetPositions(pathList.ToArray());
        print("ShowTrackerLine");

        // StartCoroutine(ZoomOutSmooth());
        StartCoroutine(ZoommoveOutSmooth(pathList.Count));
    }

    private IEnumerator ZoomOutSmooth()
    {
        print("ZoomOutSmooth");

        cineMachineCamera.Follow = null;


        float startZoom = cineMachineCamera.m_Lens.OrthographicSize;
        float elapsedTime = 0f;

        float duration = 1f; // Duration of the zoom (in seconds)

        while (elapsedTime < duration)
        {
            // Lerp the camera's orthographic size over time for smooth zoom
            cineMachineCamera.m_Lens.OrthographicSize = Mathf.Lerp(startZoom, 100, (elapsedTime / duration));

            elapsedTime += Time.deltaTime * zoomOutSpeed;
            yield return null; // Wait until the next frame to continue the coroutine
        }
    }

    private IEnumerator ZoommoveOutSmooth(int pathListCount)
    {
        print("ZoommoveOutSmooth");

        cineMachineCamera.Follow = null;


        float startZoom = cineMachineCamera.m_Lens.OrthographicSize;
        float elapsedTime = 0f;

        float duration = 1f; // Duration of the zoom (in seconds)

        List<Vector3> subdividedPath = new List<Vector3>();

        for (int i = 0; i < pathList.Count - 2; i++)
        {
            subdividedPath.Add(pathList[i]);
            subdividedPath.Add((pathList[i] + pathList[i + 1]) / 2);
        }

        List<Vector3> subdividedPath2 = new List<Vector3>();
        for (int i = 0; i < subdividedPath.Count - 2; i++)
        {
            subdividedPath2.Add(subdividedPath[i]);
            subdividedPath2.Add((subdividedPath[i] + subdividedPath[i + 1]) / 2);
        }

        List<Vector3> subdividedPath3 = new List<Vector3>();
        for (int i = 0; i < subdividedPath2.Count - 2; i++)
        {
            subdividedPath3.Add(subdividedPath2[i]);
            subdividedPath3.Add((subdividedPath2[i] + subdividedPath2[i + 1]) / 2);
        }

        int maxPointsToJump = 64;
        int numOfPointsToJump = 1;

        print("original number of points:  " + pathList.Count);
        print("new number of points:  " + subdividedPath.Count);
        int currentIndex = subdividedPath3.Count - 1;

        while (currentIndex > 0)
        {
            currentIndex -= numOfPointsToJump;
            currentIndex = currentIndex < 0 ? 0 : currentIndex;

            cineMachineCamera.transform.position = new Vector3(subdividedPath3[currentIndex].x, subdividedPath3[currentIndex].y, -10f);

            if (currentIndex >= subdividedPath3.Count - subdividedPath3.Count / 4)
            {
                numOfPointsToJump += 1;
                numOfPointsToJump = numOfPointsToJump > maxPointsToJump ? maxPointsToJump : numOfPointsToJump;
            }
            else if (currentIndex <= subdividedPath3.Count / 4)
            {
                print("SLOWING DOWN");
                numOfPointsToJump -= 1;
                numOfPointsToJump = numOfPointsToJump < 1 ? 1 : numOfPointsToJump;
            }

            yield return null;
        }
    }

    private void ZoomOut()
    {
        cineMachineCamera.Follow = null;
        cineMachineCamera.m_Lens.OrthographicSize = Mathf.Lerp(cineMachineCamera.m_Lens.OrthographicSize * 2, pathList.Count, Time.deltaTime * 2f);

        Vector3 playerScreenPos = mainCamera.WorldToScreenPoint(playerTransform.position);

        deathImage.rectTransform.position = playerScreenPos;
        deathImage.gameObject.SetActive(true);
    }
}