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
    [SerializeField] private CinemachineVirtualCamera mainCamera;

    private List<Vector3> pathList;

    private void Awake()
    {
        pathList = new List<Vector3>();
        deathImage.gameObject.SetActive(false);
    }

    private void Start()
    {
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
        trackerLine.positionCount = pathList.Count;
    }

    public void ShowTrackerLine()
    {
        trackerLine.SetPositions(pathList.ToArray());
        ZoomOut();
    }

    private void ZoomOut()
    {
        mainCamera.m_Lens.OrthographicSize = Mathf.Lerp(mainCamera.m_Lens.OrthographicSize, pathList.Count, Time.deltaTime * 2f);

        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(playerTransform.position);

        deathImage.rectTransform.position = playerScreenPos;
        deathImage.gameObject.SetActive(true);
    }
}