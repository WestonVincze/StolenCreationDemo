//using Weapon;
using System;
using UnityEngine;

namespace Player
{
  public class PlayerWeapon : MonoBehaviour
  {
    // probably not useful...
    public Action OnAttackCollision;

    void Start()
    {
    }

    void Update()
    {
    }

    public bool Attack()
    {
      return true;
      // activate hitbox for weapon
      // deactivate hitbox for weapon
    }

    private void OnCollisionEnter(Collision other)
    {
      if (other.gameObject.tag == "Enemy")
      {
        Debug.Log("Hit enemy");

        OnAttackCollision?.Invoke();
      }
    }

    // write a function to deal damage to an enemy
    // accept a weapon

    // on collision with enemy
    // deal damage to enemy

  }

}