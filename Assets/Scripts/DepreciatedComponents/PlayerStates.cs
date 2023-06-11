using System;
using System.Collections.Generic;
using UnityEngine;
/** NOT USED **/

public class PlayerStates : MonoBehaviour
{
  // on state change Actions
  public Action OnMovementStateChanged;
  public Action OnAttackStateChanged;
  public Action OnGatheringStateChanged;
  public Action OnDodgeStateChanged;

  // I don't think I need isMoving
  private bool _isMoving = false;
  private bool _isAttacking = false;
  private bool _isGathering = false;
  private bool _isDodging = false;

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

  public bool isActionable()
  {
    return !isDodging && !isAttacking;
  }


  private void SetState(ref bool currentState, bool newState, Action OnStateChanged)
  {
    if (currentState != newState)
    {
      currentState = newState;
      OnStateChanged?.Invoke();
    }
  }
  // let's cut this and do bools only for now
  /*
  private void SetState<T>(ref T currentState, T newState, Action OnStateChanged)
  {
    if (!EqualityComparer<T>.Default.Equals(currentState, newState))
    {
      currentState = newState;
      OnStateChanged?.Invoke();
    }
  }
  */

  private void OnDestroy()
  {
    OnMovementStateChanged = null;
    OnAttackStateChanged = null;
    OnGatheringStateChanged = null;
    OnDodgeStateChanged = null;
  }

}