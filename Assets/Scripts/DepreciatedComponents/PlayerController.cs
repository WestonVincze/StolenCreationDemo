/*
using System;
using System.Collections.Generic;
using UnityEngine;
//using Weapon;

// manages player state and acts as a bridge between the behaviour scripts and the player animator
// this is the only script that should be able to change the player state
namespace Player
{
  public class PlayerController : MonoBehaviour
  {
    private AnimationManager _animationManager;
    private PlayerMovement _playerMovement;
    private PlayerWeapon _playerWeapon;

    // Actions
    public Action OnMovementStateChanged;
    public Action OnAttackStateChanged;
    public Action OnGatheringStateChanged;
    public Action OnDodgeStateChanged;

    private bool _isMoving = false;
    private bool _isAttacking = false;
    private bool _isGathering = false;
    private bool _isDodging = false;

    // Properties
    public bool isMoving
    {
      get { return _isMoving; }
      set { SetState(ref _isMoving, value, OnMovementStateChanged); }
    }
    public bool isAttacking
    {
      get { return _isAttacking; }
      set { SetState(ref _isAttacking, value, OnAttackStateChanged); }
    }
    public bool isGathering
    {
      get { return isGathering; }
      set { SetState(ref _isGathering, value, OnGatheringStateChanged); }
    }
    public bool isDodging
    {
      get { return _isDodging; }
      set { SetState(ref _isDodging, value, OnDodgeStateChanged); }
    }

    void Awake()
    {
      _animationManager = GetComponentInChildren<AnimationManager>();
      _playerWeapon = GetComponent<PlayerWeapon>();
      _playerMovement = GetComponent<PlayerMovement>();

      List<(Component c, Type t)> requiredComponents = new List<(Component c, Type t)> {
        (_animationManager, typeof(AnimationManager)),
        (_playerWeapon, typeof(PlayerWeapon)),
        (_playerMovement, typeof(PlayerMovement)),
      };
      Helpers.ValidateComponents(GetType().Name, requiredComponents);

      // consider moving this to the PlayerAnimator
      OnMovementStateChanged += () => _animationManager.SetAnimationState("isMoving", _isMoving);
      OnAttackStateChanged += () => _animationManager.SetAnimationState("isAttacking", _isAttacking);
      OnGatheringStateChanged += () => _animationManager.SetAnimationState("isGathering", _isGathering);
      OnDodgeStateChanged += () => _animationManager.SetAnimationState("isDodging", _isDodging);
    }

    void Update()
    {
      HandleAttack();
      handleDodge();
    }
    
    void FixedUpdate()
    {
     // if (!_isAttacking) 
      handleMove();
    }

    private void handleDodge()
    {
      if (isDodging) return;

      if (Input.GetKeyDown(KeyCode.LeftShift))
      {
        isDodging = true;
        _animationManager.AddActionOnAnimationEnd(PlayerAnimations.Dodge, () => isDodging = false);
      }
    }

    private void handleMove()
    {
      float moveHorizontal = Input.GetAxis("Horizontal");
      float moveVertical = Input.GetAxis("Vertical");
      isMoving = _playerMovement.Move(moveHorizontal, moveVertical);
    }

    private void HandleAttack()
    {
      if (isAttacking) return;

      if (Input.GetMouseButtonDown(0))
      {
        isAttacking = true;
        _playerWeapon.Attack();
        _animationManager.AddActionOnAnimationEnd(PlayerAnimations.Attack, () => isAttacking = false);
      }
    }

    private void ResetStateValue(ref bool stateValue)
    {
      stateValue = false;
    }

    private void SetState<T>(ref T currentState, T newState, Action OnStateChanged)
    {
      if (!EqualityComparer<T>.Default.Equals(currentState, newState))
      {
        currentState = newState;
        OnStateChanged?.Invoke();
      }
    }

    private void OnDestroy() 
    {
      OnMovementStateChanged = null;
      OnAttackStateChanged = null;
      OnGatheringStateChanged = null;
      OnDodgeStateChanged = null;
    }
  }
}
*/