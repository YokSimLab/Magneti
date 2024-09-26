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

        Vector3 closestMagnet = FindNearestMagnet(transform.position).transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, (closestMagnet - transform.position).normalized);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationLerp);
    }

    Magnet FindNearestMagnet(Vector3 position)
    {
        List<Magnet> magnets = new List<Magnet>(FindObjectsOfType<Magnet>());

        if (magnets.Count == 0)
        {
            return null;
        }

        magnets.Remove(player.GetComponent<Magnet>());

        Magnet nearestObject = magnets
            .OrderBy(obj => Vector3.Distance(position, obj.transform.position))
            .First();

        return nearestObject;
    }
}
