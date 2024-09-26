using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedManager : MonoBehaviour
{
    #region Singleton

    private static SeedManager _instance;

    public static SeedManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("Seed manager is null");
            }

            return _instance;
        }
    }

    #endregion

    public int seed = 0;
    public bool isSeedOn = false;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
