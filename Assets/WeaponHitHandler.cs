using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitHandler : MonoBehaviour
{
  [SerializeField]
  public float powerMultiplier = 1; //This is to be animated. Value will be multiplied (and rounded for ints) on hit.

  [SerializeField]
  public int damage = 20;

  [SerializeField]
  public float knockbackForce = 20;

  // time duration before the object can be hit again
  [SerializeField]
  private float _hitCooldown = 0.25f;

  private List<HitRecord> _hitRecords = new List<HitRecord>();

  private class HitRecord
  {
    public GameObject hitObject;
    public float hitTime;

    public HitRecord(GameObject hitObject, float hitTime)
    {
      this.hitObject = hitObject;
      this.hitTime = hitTime;
    }
  }

  void Update()
  {
    for (int i = _hitRecords.Count - 1; i >= 0; i--)
    {
      if (Time.time - _hitRecords[i].hitTime > _hitCooldown)
      {
        _hitRecords.RemoveAt(i);
      }
    }
  }

  //This is called by child collision handling/checking behaviors
  public void HandleHit(GameObject pHitObject, Vector3 pDirection)
  {
    // check if object is in the hit records
    foreach (HitRecord record in _hitRecords)
    {
      if (record.hitObject == pHitObject)
      {
        // the object is still in cooldown, skip the hit
        return;
      }
    }

    // if not, add it to the records
    _hitRecords.Add(new HitRecord(pHitObject, Time.time));

    // Search for Health component and call Hurt function
    Health healthComponent = pHitObject.GetComponent<Health>();
    if (healthComponent != null)
    {
      healthComponent.Hurt(Mathf.RoundToInt(damage * powerMultiplier));
    }

    // Search for KnockbackHandler component and call ApplyKnockback function
    KnockbackHandler knockbackComponent = pHitObject.GetComponent<KnockbackHandler>();
    if (knockbackComponent != null)
    {
      knockbackComponent.ApplyKnockback(pDirection, knockbackForce * powerMultiplier);
    }

    // continue for other components...
  }

  //This should be called in a StateMachineBehavior's OnStateExit code
  //Refer to this post: https://forum.unity.com/threads/call-a-function-on-completion-of-an-animation-state.396058/
  public void ClearHitRecords()
  {
    _hitRecords.Clear();
  }
}
