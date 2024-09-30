using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasUIOnPlayer : MonoBehaviour
{
    [SerializeField] GasManager gasManager;
    [SerializeField] Slider gasSliderOnCharacter;

    void Update()
    {
        if (!gasManager || !gasSliderOnCharacter) return;

        gasSliderOnCharacter.value = gasManager.CurrentGas / gasManager.maxGas;
    }
}
