using System;
using UnityEngine;
using System.Collections.Generic;

// maybe I could add a function to validate that all AnimationName's have a matching
// animation name in the Animator. I could log errors or warnings if a value in the 
// AnimationName enum fails to get matched with an animation in the Animator

public class AnimationManager: MonoBehaviour
{
  protected Animator _animator;
  protected Dictionary<int, Action> _animationStartHandlers = new Dictionary<int, Action>();
  protected Dictionary<int, Action> _animationEndHandlers = new Dictionary<int, Action>();

  protected int _currentAnimationHash = 0;

  protected virtual void Awake()
  {
    _animator = GetComponent<Animator>();
    //if (!_animator) //Debug.LogError("PlayerAnimator could not find Animator");
  }


  protected void Update()
  {
    AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
    int animationHash = stateInfo.shortNameHash;

    bool switched = false;
    _currentAnimationHash = animationHash;
    /*
    if (stateInfo.normalizedTime > 0){
      switched = _currentAnimationHash != animationHash;
      _currentAnimationHash =  animationHash;
    } 
    */

    // animation start
    if (stateInfo.normalizedTime <= 0)
    {
      //Debug.Log("setting current animation to: " + animationHash);

      if (_animationStartHandlers.ContainsKey(animationHash)) {
        _animationStartHandlers[animationHash]?.Invoke();
        _animationStartHandlers.Remove(animationHash);
      }
    }
    // animation end or interrupted
    else if (stateInfo.normalizedTime >= 1 || switched)
    {
      if (_animationEndHandlers.ContainsKey(animationHash)) {
        _animationEndHandlers[animationHash]?.Invoke();
        _animationEndHandlers.Remove(animationHash);
      }
    }
  }

  public void SetAnimationState<T>(string name, T state)
  {
    // easily add more types here, if needed
    if (typeof(T) == typeof(bool))
    {
      _animator.SetBool(name, Convert.ToBoolean(state));
    }
  }

  public void AddActionOnAnimationStart(Enum animation, Action action)
  {
    int animationHash = GetAnimationHash(animation);

    if (_animationStartHandlers.ContainsKey(animationHash)) return;

    _animationStartHandlers[animationHash] = action;
  }


  public void AddActionOnAnimationEnd(Enum animation, Action action)
  {
    int animationHash = GetAnimationHash(animation);

    if (_animationEndHandlers.ContainsKey(animationHash)) return;

    _animationEndHandlers[animationHash] = action;
  }

  private int GetAnimationHash(Enum animation)
  {
    int animationHash = Animator.StringToHash(animation.ToString());

    if (!_animator.HasState(0, animationHash))
    {
      //Debug.LogError($"AnimationManager: {animation.ToString()} was not found in {gameObject.name}'s animator");
    }

    return animationHash;
  }

  private void OnDestroy() 
  {
    _animationStartHandlers.Clear();
    _animationEndHandlers.Clear();
  }

  /* This doesn't make sense to have unless we change how we handle state change events 
  public void RegisterAnimationState<T>(string animation, Action<T> state)
  {
    int animationHash = Animator.StringToHash(animation);
    if (_animationStateChangeHandlers.ContainsKey(animationHash)) return;

    _animationStateChangeHandlers[animationHash] = state;
  }
  */
}