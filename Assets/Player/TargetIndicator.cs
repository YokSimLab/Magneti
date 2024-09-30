using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float rotationLerp;
    [SerializeField] private float radius;

    private void Start()
    {
        transform.GetChild(0).position = new Vector3(0, radius, 0);
    }

    void Update()
    {
        transform.position = player.transform.position;

        Vector3 closestMagnet = FindNearestBattery(transform.position).transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, (closestMagnet - transform.position).normalized);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationLerp);
    }

    Gas FindNearestBattery(Vector3 position)
    {
        List<Gas> batteries = new List<Gas>(FindObjectsOfType<Gas>());

        if (batteries.Count == 0)
        {
            return null;
        }

        Gas nearestObject = batteries
            .OrderBy(obj => Vector3.Distance(position, obj.transform.position))
            .First();

        return nearestObject;
    }
}
