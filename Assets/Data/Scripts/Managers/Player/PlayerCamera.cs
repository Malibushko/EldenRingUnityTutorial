using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Singleton;

    public Camera cameraObject;
    
    private void Awake()
    {
        if (Singleton == null)
            Singleton = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
