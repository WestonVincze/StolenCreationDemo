using UnityEngine;

public class PlayerJump : MonoBehaviour
{
  private Animator _animator;
  private Rigidbody _rb;
  private float _jumpForce = 10f;

  private void Start()
  {
    _animator = GetComponentInChildren<Animator>();
    _rb = GetComponent<Rigidbody>();
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      _animator?.SetTrigger("jump");
    }
  }

  private void FixedUpdate()
  {
    _animator?.SetFloat("yVelocity", _rb.velocity.y);
  }

  public void TriggerJump()
  {
    _rb.AddForce(new Vector3(0, _jumpForce, 0), ForceMode.Impulse);
  }

}