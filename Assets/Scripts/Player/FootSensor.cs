using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSensor: MonoBehaviour
{
  // I think this design is a bit awkward. Ideally we wouldn't need a reference to an animator for every sensor
  [SerializeField]
  private Animator _animator;
  
  private void Start() 
  {
    if (!_animator) Debug.Log("Foot Sensor needs a manual reference to the Player Animator.");
  }

  private void OnTriggerEnter(Collider other) 
  {
    if (other.gameObject.tag == "Ground")
    {
      _animator?.SetBool("isGrounded", true);
    }
  }
  private void OnTriggerExit(Collider other)
  {
    if (other.gameObject.tag == "Ground")
    {
      _animator?.SetBool("isGrounded", false);
    }
  }
}
