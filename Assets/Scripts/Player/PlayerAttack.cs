using UnityEngine;

public class PlayerAttack : MonoBehaviour {
  private Animator _animator;

  private void Start()
  {
    _animator = GetComponentInChildren<Animator>();
  }

  private void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      _animator?.SetTrigger("attack");
    }
  }
}