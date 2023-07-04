using UnityEngine;
using System;

public class PlayerCamera : MonoBehaviour 
{
  // this script must have a reference to the main camera and be attached to the player so that the player can easily access camera controls
  [SerializeField]
  private Transform _camera;
  private bool _isRotating;
  public float rotationSpeed;

  private void FixedUpdate() 
  {
    // testing out a crude "camera lock" system
    if (Input.GetMouseButton(1)) {
      transform.rotation = Quaternion.Slerp(transform.rotation, CameraDirection(), 5f * Time.deltaTime);
    }
  }

  // Returns the quaternion rotation of the camera
  public Quaternion CameraDirection()
  {
    if (!_camera) {
      Debug.Log("The PlayerCamera needs a reference to the main camera. Cannot return a camera direction.");
      return new Quaternion();
    }

    return Quaternion.AngleAxis(_camera.rotation.eulerAngles.y, Vector3.up);
  }

  // sets the rotation of a transform to the rotation of the camera
  public void SetToCameraRotation(Transform t)
  {
    // this is how Valheim works... but it feels a bit snappy right now. Not a fan.
    //t.rotation = CameraDirection();
  }
}