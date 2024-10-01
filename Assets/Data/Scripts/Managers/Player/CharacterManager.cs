using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterManager : NetworkBehaviour
{
    public CharacterController CharacterController;
    
    private PlayerNetworkManager _networkManager;

    private void OnEnable()
    {
        _networkManager = GetComponent<PlayerNetworkManager>();
    }

    protected virtual void Awake()
    {
       DontDestroyOnLoad(this);
    }

    protected virtual void Update()
    {
        UpdateNetworkPosition();
    }

    private void UpdateNetworkPosition()
    {
        if (!_networkManager)
            return;
        
        if (IsOwner)
        {
            _networkManager.networkPosition.Value = CharacterController.transform.position;
            _networkManager.networkRotation.Value = CharacterController.transform.rotation;
        }
        else
        {
            CharacterController.transform.position = Vector3.SmoothDamp(
                CharacterController.transform.position,
                _networkManager.networkPosition.Value,
                ref _networkManager.networkPositionVelocity,
                _networkManager.networkPositionSmoothTime
                );

            CharacterController.transform.rotation = Quaternion.Slerp(
                CharacterController.transform.rotation,
                _networkManager.networkRotation.Value,
                _networkManager.networkRotationSmoothTime
            );
        }
    }
}
