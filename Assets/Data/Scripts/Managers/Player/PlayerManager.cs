using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    private PlayerLocomotionManager _playerLocomotionManager;
    private PlayerNetworkManager _playerNetworkManager;
    
    protected override void Awake()
    {
        base.Awake();
        
        _playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        CharacterController = GetComponent<CharacterController>();
        
        _playerLocomotionManager.SetPlayer(this);
    }

    protected override void Update()
    {
        base.Update();

        if (!IsOwner)
            return;
        
        _playerLocomotionManager.HandleAllMovements();
    }

    public CharacterController GetCharacterController()
    {
        return CharacterController;
    }
}
