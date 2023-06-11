using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
  [SerializeField]
  public GameObject target;

  [SerializeField]
  private int _yOffset = 5;
  [SerializeField]
  private int _zOffset = -20;

  void Start()
  {
    if (target == null)
    {
      Debug.LogWarning("FollowCamera: target not set, defaulting to Player");
      target = GameObject.Find("Player");
    }
  }

  void FixedUpdate()
  {
    FollowTarget();
  }

  void FollowTarget()
  {
    Vector3 targetPosition = target.transform.position;
    transform.position = targetPosition + new Vector3(0, _yOffset, _zOffset);
  }
}
