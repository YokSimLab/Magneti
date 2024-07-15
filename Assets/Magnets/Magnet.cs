using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Magnet : MonoBehaviour
{

    [SerializeField] private float gravity = 1;
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Magnetized"))
        {
            PlayerMovement playerMovement = collider.GetComponent<PlayerMovement>();
            float grabityMultibication = playerMovement ? playerMovement.GravityMultiplier : 1;
            AddGravityForce(GetComponent<Rigidbody2D>(),collider.GetComponent<Rigidbody2D>(),grabityMultibication);   
        }
    }


public void AddGravityForce(Rigidbody2D attractor, Rigidbody2D target, float PlayerGravityMultiplier)
{
    float massProduct = attractor.mass*target.mass;
    
    //You could also do
    //float distance = Vector3.Distance(attractor.position,target.position.
    Vector3 difference = attractor.position - target.position;
    float distance = difference.magnitude; // r = Mathf.Sqrt((x*x)+(y*y))

    //F = G * ((m1*m2)/r^2)
    float unScaledforceMagnitude = massProduct/Mathf.Pow(distance,2);
    float forceMagnitude = gravity * unScaledforceMagnitude * PlayerGravityMultiplier;

    Vector3 forceDirection = difference.normalized;

    Vector3 forceVector = forceDirection*forceMagnitude;

    target.AddForce(forceVector);
}
}