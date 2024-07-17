using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Summary:
//     Object that pulls and/or can be pulled.
//     Can be controlled via booleans
public class Magnet : MonoBehaviour
{
    public bool isPullingMagnet;
    public bool isPulledMagnet;
    public float magneticForce;

    [SerializeField] private HashSet<Magnet> pulledMagnetsInField;
    private Rigidbody2D myRigidBody2D;

    private void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        pulledMagnetsInField = new HashSet<Magnet> { };

        //Change color (JUST FOR NOW)
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            if (magneticForce > 0)
            {
                spriteRenderer.color = Color.red;

            }
            else
            {
                spriteRenderer.color = Color.blue;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPullingMagnet) return;
        print(collision.name);

        Magnet enteredMagnet = collision.GetComponent<Magnet>();
        if (enteredMagnet && enteredMagnet.isPulledMagnet)
        {
            pulledMagnetsInField.Add(enteredMagnet);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isPullingMagnet) return;

        Magnet exitedMagnet = collision.GetComponent<Magnet>();
        if (exitedMagnet && exitedMagnet.isPulledMagnet)
        {
            pulledMagnetsInField.Remove(exitedMagnet);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!isPullingMagnet) return;

        foreach (Magnet magnet in pulledMagnetsInField)
        {
            Vector3 magneticForceToApply = CalcMagneticForce(magnet, this);
            magnet.ApplyMagneticForce(magneticForceToApply);
        }
    }

    public Vector3 CalcMagneticForce(Magnet attractor, Magnet target)
    {
        float magneticForcesProduct = attractor.magneticForce * target.magneticForce;

        Vector3 difference = attractor.transform.position - target.transform.position;
        float distance = difference.magnitude;

        //F = k * ((q1*q2) / r^2)
        //See: Coulomb’s Laws of Electrostatics 
        float forceMagnitude = magneticForcesProduct / Mathf.Pow(distance, 2f);

        Vector3 forceDirection = difference.normalized;
        Vector3 forceVector = forceDirection * forceMagnitude;

        return forceVector;
    }

    public void ApplyMagneticForce(Vector3 magneticForceToApply)
    {
        if (myRigidBody2D)
        {
            myRigidBody2D.AddForce(magneticForceToApply);
        }
    }
}
