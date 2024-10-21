using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityVisualizer : MonoBehaviour
{
    [SerializeField] LineRenderer line;
    [SerializeField] Material lineMaterial;
    [SerializeField] Vector2 uvAnimationRate;

    Vector2 uvOffset;
    Vector3[] linePointsPositions;


    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        if (!line)
        {
            line = gameObject.AddComponent<LineRenderer>();
        }

        linePointsPositions = new Vector3[] { Vector3.zero, Vector3.zero };
        line.material = lineMaterial;
        line.textureMode = LineTextureMode.Tile;
        line.textureScale = new Vector2(.2f, 1);
    }

    void Update()
    {
        GameObject pullingMagnet = GetComponent<PlayerMovement>().pullingMagnet.gameObject;

        if (pullingMagnet)
        {
            line.enabled = true;
            linePointsPositions[0] = transform.position;
            linePointsPositions[1] = GetComponent<PlayerMovement>().pullingMagnet.transform.position;

            line.SetPositions(linePointsPositions);

            uvOffset -= uvAnimationRate * Time.deltaTime;
            line.material.SetVector("_Offset", new Vector4(-uvOffset.x, uvOffset.y, 0, 0));

        }
        else
        {
            line.enabled = false;
        }
    }
}
