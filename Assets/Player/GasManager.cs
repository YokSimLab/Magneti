using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasManager : MonoBehaviour
{
    [SerializeField] float maxGas;
    [SerializeField] float currentGas;
    [SerializeField] float gasRemoveRate;
    [SerializeField] Slider gasSlider;

    private GameManager _gameManager;

    public float CurrentGas
    {
        get => currentGas;
        set => currentGas = value;
    }

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