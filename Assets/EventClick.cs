using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventClick : MonoBehaviour, IPointerClickHandler
{
    private Magnet player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Magnet>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {


        if (!CompareTag("Player"))
        {
            GetComponent<Magnet>().pulledMagnetsInField.Add(player);
        }
    }
}
