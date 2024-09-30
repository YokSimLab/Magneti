using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Gas : MonoBehaviour
{
    [SerializeField] float batteryAmount;
    [SerializeField] float generationRadius = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GasManager gasManager = collision.GetComponent<GasManager>();
        if (gasManager)
        {
            gasManager.AddGas(batteryAmount);
        }

        GenerateNewGas();
        Destroy(gameObject);
    }

    void GenerateNewGas()
    {
        Vector3 directionToGas = transform.position.normalized;
        float angleInRad = Mathf.Atan2(directionToGas.y, directionToGas.x);

        float arcAngle = Mathf.PI * .6f;

        Collider2D[] colliders;
        Vector3 randomPointOnArc;
        do
        {
            randomPointOnArc = GetRandomPointOnArcInDirection(angleInRad, arcAngle);
            colliders = Physics2D.OverlapCircleAll(randomPointOnArc, .2f);

        } while (colliders.Length != 0);

        Instantiate(gameObject, randomPointOnArc, new Quaternion());
    }

    Vector3 GetRandomPointOnArcInDirection(float directionInRad, float arcAngle)
    {
        float generationAngle = Random.Range(directionInRad - arcAngle / 2, arcAngle + directionInRad - arcAngle / 2);
        float x = transform.position.x + generationRadius * Mathf.Cos(generationAngle);
        float y = transform.position.y + generationRadius * Mathf.Sin(generationAngle);

        return new Vector3(x, y, 0);
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;

    //    Vector3 directionToGas = transform.position.normalized;
    //    float angleInRad = Mathf.Atan2(directionToGas.y, directionToGas.x);

    //    for (int i = 0; i < 100; i++)
    //    {
    //        float arcAngle = Mathf.PI * .9f;
    //        float generationAngle = Random.Range(angleInRad - arcAngle / 2, arcAngle + angleInRad - arcAngle / 2);
    //        float x = transform.position.x + generationRadius * Mathf.Cos(generationAngle);
    //        float y = transform.position.y + generationRadius * Mathf.Sin(generationAngle);
    //        Gizmos.DrawSphere(new Vector3(x, y, 0), 0.1f); // Draw a small sphere at each point
    //    }
    //}
}
