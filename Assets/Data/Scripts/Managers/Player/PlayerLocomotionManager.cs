using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionManager : MonoBehaviour
{
    private PlayerManager _player;

    private Vector3 _moveDirection;
    private Vector3 _targetRotateDirection;
    
    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _rotationSpeed;
    
    public void HandleAllMovements()
    {
        HandleGroundedMovement();
        HandleRotation();
        HandleAerialMovement();
        
    }

    public void SetPlayer(PlayerManager player)
    {
        _player = player;    
    }
    
    private void HandleGroundedMovement()
    {
        float horizontalMovement = PlayerInputManager.Singleton.Movement.x;
        float verticalMovement = PlayerInputManager.Singleton.Movement.y;

        _moveDirection = horizontalMovement * PlayerCamera.Singleton.transform.right + verticalMovement * PlayerCamera.Singleton.transform.forward;
        _moveDirection.Normalize();
        _moveDirection.y = 0;

        float moveCoefficient = _normalSpeed;
        
        if (PlayerInputManager.Singleton.MoveAmount > 1)
            moveCoefficient = _sprintSpeed;
        
        _player.GetCharacterController().Move(_moveDirection * (moveCoefficient * Time.deltaTime));
        
        if (_moveDirection == Vector3.zero)
            _moveDirection = PlayerCamera.Singleton.transform.forward;
    }

    private void HandleRotation()
    {
        _targetRotateDirection = Vector3.zero;
        _targetRotateDirection = PlayerCamera.Singleton.transform.forward * PlayerInputManager.Singleton.Movement.y;
        _targetRotateDirection += PlayerCamera.Singleton.transform.right * PlayerInputManager.Singleton.Movement.x;
        _targetRotateDirection.Normalize();
        _targetRotateDirection.y = 0;
        
        if (_targetRotateDirection == Vector3.zero)
            _targetRotateDirection = transform.forward;
        
        Quaternion newRotation = Quaternion.LookRotation(_targetRotateDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * _rotationSpeed);
        
        transform.rotation = targetRotation;
    }
    
    private void HandleAerialMovement()
    {
        
    }
}
