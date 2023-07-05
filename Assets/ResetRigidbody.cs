using UnityEngine;

public class ResetRigidbody : MonoBehaviour
{
  private Rigidbody rb;
  private Vector3 initialPosition;
  private Quaternion initialRotation;

  void Start()
  {
    // Get the Rigidbody component of the object
    rb = GetComponent<Rigidbody>();

    // Store the initial position and rotation
    initialPosition = transform.position;
    initialRotation = transform.rotation;
  }

  void Update()
  {
    // Check if the "R" key is pressed
    if (Input.GetKeyDown(KeyCode.R))
    {
      ResetObject();
    }
  }

  private void ResetObject()
  {
    // Reset the position and rotation of the object
    rb.velocity = Vector3.zero;
    rb.angularVelocity = Vector3.zero;
    transform.position = initialPosition;
    transform.rotation = initialRotation;
  }
}
