using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gasCan1;
    [SerializeField] GameObject gasCan2;
    [SerializeField] GameObject gasCan3;
    [SerializeField] GameObject winText;

    private void Update()
    {
        if (!(gasCan1 || gasCan2 || gasCan3))
        {
            winText.SetActive(true);
        }
    }
}
