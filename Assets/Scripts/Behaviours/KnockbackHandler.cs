using UnityEngine;

public class KnockbackHandler : MonoBehaviour
{
  private Rigidbody _rb;
  public float KnockbackVulnerability;
  private bool _isBeingKnockedBack;
  private float _closeToZeroVelocity = 0.1f; // threshold to consider velocity close to zero
  private float _stopTimer;
  private float _stopDuration = 1f; // Time in seconds for object to be considered stopped

  void Awake()
  {
    _rb = GetComponent<Rigidbody>();
    FreezeRigidbodyConstraints();
  }

  // Function to apply knockback
  public void ApplyKnockback(Vector3 direction, float strength)
  {
    _isBeingKnockedBack = true;
    UnfreezeRigidbodyConstraints();
    float finalStrength = strength * KnockbackVulnerability;
    _rb.AddForce(direction.normalized * finalStrength, ForceMode.Impulse);
  }

  void Update()
  {
    if (_isBeingKnockedBack)
    {
      if (_rb.velocity.magnitude < _closeToZeroVelocity)
      {
        _stopTimer += Time.deltaTime;
        if (_stopTimer >= _stopDuration)
        {
          _isBeingKnockedBack = false;
          ResetRotation();
          FreezeRigidbodyConstraints();
          _stopTimer = 0f;
        }
      }
      else
      {
        _stopTimer = 0f;
      }
    }
  }

  private void FreezeRigidbodyConstraints()
  {
    // Freeze all except y position
    _rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
  }

  private void UnfreezeRigidbodyConstraints()
  {
    _rb.constraints = RigidbodyConstraints.None;
  }

  private void ResetRotation()
  {
    transform.rotation = Quaternion.identity;
  }
}
