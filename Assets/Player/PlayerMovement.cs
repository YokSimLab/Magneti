using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 lastVelocityDirection = Vector3.up;

    [SerializeField] float velocityAnimationChangeMultiplier = 0.1f;

    private void Update()
    {
        //These name suck because they don't really do, but check for input and then do
        ChangePolarity();
        OnClick();
        ChangePlayerRotationToVelocity();
    }

    private void OnClick()
    {
        GasManager gas = GetComponent<GasManager>();
        if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0)) && gas && gas.CurrentGas > 0)
        {
            foreach (Magnet magnet in FindObjectsOfType<Magnet>())
            {
                magnet.PulledMagnetsInField.Remove(GetComponent<Magnet>());
            }

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            Magnet nearestMagnet = FindNearestMagnet(mousePosition);
            if (nearestMagnet != null)
            {
                nearestMagnet.PulledMagnetsInField.Add(GetComponent<Magnet>());
            }
        }
    }

    private void ChangePolarity()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Magnet myMagnet = GetComponent<Magnet>();
            if (myMagnet)
            {
                myMagnet.magneticForce *= -1.0f;
            }
        }
    }

    private void ChangePlayerRotationToVelocity()
    {
        Vector3 VelocityDirection = GetComponent<Rigidbody2D>().velocity.normalized;
        VelocityDirection = Vector3.Lerp(lastVelocityDirection, VelocityDirection, velocityAnimationChangeMultiplier);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, VelocityDirection);
        lastVelocityDirection = VelocityDirection;
    }

    Magnet FindNearestMagnet(Vector3 clickPosition)
    {
        Magnet[] magnets = GameObject.FindObjectsOfType<Magnet>();

        if (magnets.Length == 0)
        {
            return null;
        }

        Magnet nearestObject = magnets
            .OrderBy(obj => Vector3.Distance(clickPosition, obj.transform.position))
            .First();

        return nearestObject;
    }
}