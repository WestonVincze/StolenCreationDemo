using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
  [SerializeField]
  private int _maxHealth = 100;
  [SerializeField]
  private int _currentHealth;
  private Animator _animator;

  void Awake()
  {
    _currentHealth = _maxHealth;
    _animator = GetComponentInChildren<Animator>();
  }

  // Decreases the player's health by a certain amount and checks if they should die
  public void Hurt(int damage)
  {
    if (damage < 0)
    {
      Debug.Log("Damage can't be negative.");
      return;
    }

    _currentHealth -= damage;

    if (_currentHealth > 0)
    {
      if (_animator != null)
      {
        _animator.SetTrigger("Hurt");
      }
    }
    else
    {
      Die();
    }
  }

  // Increases the player's health by a certain amount
  public void Heal(int healAmount)
  {
    if (healAmount < 0)
    {
      Debug.Log("Heal amount can't be negative.");
      return;
    }

    _currentHealth += healAmount;

    if (_currentHealth > _maxHealth)
    {
      _currentHealth = _maxHealth;
    }
  }

  // Called when the player's health reaches 0
  private void Die()
  {
    if (_animator != null)
    {
      _animator.SetTrigger("Death");
    }
    // Disable player control, collider etc. 
    // Depends on your implementation

    Destroy(gameObject);
  }
}
