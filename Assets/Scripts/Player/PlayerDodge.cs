using System.Collections;
using UnityEngine;

namespace Player
{
  public class PlayerDodge : MonoBehaviour
  {
    private Animator _animator;
    private Rigidbody _rb;
    [SerializeField] private float _dodgeCooldown = 1f;
    [SerializeField] private float _dodgeLength = (1.208f * 0.7931035f); // I really hate this
    [SerializeField] private float _dodgeForce = 20f;
    private bool _applyDodgeForce = false;
    private bool _canDodge = true;
    public bool canDodge { get { return _canDodge; } }


    void Start()
    {
      _animator = GetComponentInChildren<Animator>();
      _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
      HandleDodge();
    }

    void HandleDodge()
    {
      if (!_canDodge) return;

      // function to reset all triggers
      if (Input.GetKeyDown(KeyCode.LeftShift))
      {
        _animator?.SetTrigger("dodge");
        // ISSUE: we need to confirm that we can actually dodge before we apply dodge logic. 
        // can dodge needs to factor in whether the player is doing something that makes them unable to dodge
        // perhaps gathering up player state into a singleton and have an "isActionable" function that uses the players
        // various states to determine whether or not actions can occur? See "PlayerState" (WIP)
        StartCoroutine(DodgeCooldown());
      }
    }

    private void FixedUpdate()
    {
      if (!_applyDodgeForce) return;
      _rb.AddForce(transform.forward * _dodgeForce, ForceMode.Impulse);
    }

    IEnumerator DodgeCooldown()
    {
      _canDodge = false;
      yield return new WaitForSeconds(_dodgeLength * 0.2f);
      _applyDodgeForce = true;
      yield return new WaitForSeconds(_dodgeLength * 0.3f);
      _applyDodgeForce = false;
      yield return new WaitForSeconds(_dodgeLength);
      _canDodge = true;
    }
  }
}