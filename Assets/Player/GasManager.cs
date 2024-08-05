using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasManager : MonoBehaviour
{
    [SerializeField] float maxGas;
    [SerializeField] float currentGas;
    [SerializeField] float gasRemoveRate;
    public float CurrentGas { get => currentGas; set => currentGas = value; }

    // Update is called once per frame
    void Update()
    {
        RemoveGas();
    }

    public void AddGas(float gasAmount)
    {
        currentGas += gasAmount;
    }

    private void RemoveGas()
    {
        currentGas = Mathf.Clamp(currentGas - (gasRemoveRate * Time.deltaTime), 0, maxGas);
        print(currentGas);

        if (currentGas <= 0)
        {
            foreach (Magnet magnet in FindObjectsOfType<Magnet>())
            {
                magnet.PulledMagnetsInField.Remove(GetComponent<Magnet>());
            }
        }
    }
}
