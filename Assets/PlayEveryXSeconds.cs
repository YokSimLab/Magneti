using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEveryXSeconds : MonoBehaviour
{
    float CurrTime = 0;
    Animator Animator;
    [SerializeField] float MaxTime = 0;
    [SerializeField] float OffsetPercentage = 0.15f;
    [SerializeField] string Animation;

    void Start()
    {
        if (!Animator)
        {
            Animator = GetComponent<Animator>();
        }

        MaxTime = Random.Range(MaxTime - MaxTime * OffsetPercentage,
                        MaxTime + MaxTime * OffsetPercentage);
    }

    void Update()
    {
        CurrTime += Time.deltaTime;

        if (CurrTime >= MaxTime)
        {
            MaxTime = Random.Range(MaxTime - MaxTime * OffsetPercentage,
                                    MaxTime + MaxTime * OffsetPercentage);
            CurrTime = 0;

            Animator.Play(Animation);
        }
    }
}
