using UnityEngine;

public class PlayerAttack : MonoBehaviour {
  private Animator _animator;
  private PlayerCamera _camera;

  private void Start()
  {
    _animator = GetComponentInChildren<Animator>();
    _camera = GetComponent<PlayerCamera>();
  }

  private void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      // empty function right now
      _camera.SetToCameraRotation(transform);
      _animator?.SetTrigger("attack");
      _animator?.SetTrigger("attack_slash");
    }
    else if (Input.mouseScrollDelta.y < 0)
    {
      Debug.Log("overhead");
      _animator?.SetTrigger("attack");
      _animator?.SetTrigger("attack_overhead");
    }
    else if (Input.mouseScrollDelta.y > 0)
    {
      Debug.Log("stab");
      _animator?.SetTrigger("attack");
      _animator?.SetTrigger("attack_stab");
    }
  }
}