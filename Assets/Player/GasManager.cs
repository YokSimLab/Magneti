using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasManager : MonoBehaviour
{
    public float maxGas;
    public float gasRemoveRate;
    [SerializeField] float currentGas;
    [SerializeField] Slider gasSlider;

    public float CurrentGas
    {
        get => currentGas;
        set => currentGas = value;
    }

    void Update()
    {
        // RemoveGas();
    }

    public void AddGas(float gasAmount)
    {
        currentGas += gasAmount;
    }

    private void RemoveGas()
    {
        currentGas = Mathf.Clamp(currentGas - (gasRemoveRate * Time.deltaTime), 0, maxGas);
        gasSlider.value = currentGas / maxGas;

        if (currentGas <= 0)
        {
            foreach (Magnet magnet in FindObjectsOfType<Magnet>())
            {
                magnet.PulledMagnetsInField.Remove(GetComponent<Magnet>());
            }

            GameManager.Instance.FailGame();
        }
    }
}