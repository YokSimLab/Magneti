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

    private HashSet<Magnet> pulledMagnetsInField;
    private Rigidbody2D myRigidBody2D;

    public HashSet<Magnet> PulledMagnetsInField
    {
        get => pulledMagnetsInField;
        set => pulledMagnetsInField = value;
    }

    private void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        PulledMagnetsInField = new HashSet<Magnet> { };
    }

    private void Update()
    {
        if (!isPullingMagnet) return;

        foreach (Magnet magnet in PulledMagnetsInField)
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

        //F = k * ((q1*q2) / sqrt(r))
        //See: Duppah mind
        float forceMagnitude = magneticForcesProduct / Mathf.Sqrt(distance);

        Vector3 forceDirection = difference.normalized;
        Vector3 forceVector = forceDirection * forceMagnitude;
        if (target.GetComponent<Rigidbody2D>().velocity.magnitude > 1)
        {
            forceVector = forceDirection * forceMagnitude * (Mathf.Sqrt(target.GetComponent<Rigidbody2D>().velocity.magnitude));
        }

        return forceVector;
    }

    public void ApplyMagneticForce(Vector3 magneticForceToApply)
    {
        if (myRigidBody2D)
        {
            myRigidBody2D.AddForce(magneticForceToApply);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        FindObjectOfType<GameManager>().FailGame();
    }
}