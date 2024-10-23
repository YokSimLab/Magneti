using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetForUI : MonoBehaviour
{
    private Magnet magnet;
    private Rigidbody2D rb;

    private void Awake()
    {
        magnet = GetComponent<Magnet>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb != null)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward ,rb.velocity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (magnet != null &&
            collision.GetComponent<Magnet>() != null)
        {
            if (!magnet.PulledMagnetsInField.Contains(collision.GetComponent<Magnet>()))
            {
                magnet.PulledMagnetsInField.Add(collision.GetComponent<Magnet>());
            }
        }
    }
}
