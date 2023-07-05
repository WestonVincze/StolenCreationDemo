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
    }
  }
}