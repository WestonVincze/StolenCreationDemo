using UnityEngine;

public class SwordColliders: MonoBehaviour 
{
  // be responsible for managing colliderData and colliderStates
  // handles collision actions
  // it could use inheritance

  // previous state for each collider
  // current state

  [SerializeField]
  private int _damage;

  private Blade _blade;

  private bool _isBladeActive;

  private void Awake()
  {
    _blade = GetComponentInChildren<Blade>();
  }

  private void Update()
  {

  }

  private void ActivateBlade()
  {

  }


  // check for behaviours, look for specific components (like HP)
  
}