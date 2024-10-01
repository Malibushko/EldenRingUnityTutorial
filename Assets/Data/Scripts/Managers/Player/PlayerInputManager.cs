using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Singleton;
    
    private PlayerInputActions PlayerInputActions;

    [SerializeField] public Vector2 Movement;
    [SerializeField] public float MoveAmount;
    
    private void Awake()
    {
        if (Singleton == null)
            Singleton = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        DontDestroyOnLoad(Singleton);
        
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        
        Singleton.enabled = false;
    }
    
    private void OnEnable()
    {
        if (PlayerInputActions == null)
        {
            PlayerInputActions = new PlayerInputActions();

            PlayerInputActions.PlayerMovement.Movement.performed += OnPlayerInput;
        }
        
        PlayerInputActions.Enable();
    }

    private void OnPlayerInput(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
        MoveAmount = Math.Clamp(Math.Abs(Movement.x) + Math.Abs(Movement.y), 0, 1);
    }
    
    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }
    
    #region Events

    void OnActiveSceneChanged(Scene oldScene, Scene newScene)
    {
        Singleton.enabled = newScene.buildIndex == WorldSavesManager.Singleton.WorldSceneIndex;
    }
    
    #endregion
}
