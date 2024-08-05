using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject ToFollow;
    [SerializeField] private float SmoothAmount;

    // Update is called once per frame
    void Update()
    {
        if (ToFollow)
        {
            transform.position = Vector2.Lerp(transform.position, ToFollow.transform.position, SmoothAmount);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }
    }
}
