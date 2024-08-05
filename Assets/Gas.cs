using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    [SerializeField] float gasAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("ENTER");
        GasManager gasManager = collision.GetComponent<GasManager>();
        if (gasManager)
        {
            print("DESTORY");
            gasManager.AddGas(gasAmount);
            Destroy(gameObject);
        }
    }
}
