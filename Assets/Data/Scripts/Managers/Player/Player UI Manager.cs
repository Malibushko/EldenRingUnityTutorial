using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager Singleton;
    
    [Header("NETWORK JOIN")]
    [SerializeField] bool StartGameAsClient;
    
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

    private void Update()
    {
        if (StartGameAsClient)
        {
            StartGameAsClient = false; 
            
            NetworkManager.Singleton.Shutdown();
            NetworkManager.Singleton.StartClient();
        }
    }
}
