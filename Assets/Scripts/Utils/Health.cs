using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Member variables
    [SerializeField]
    private int maxHealth = 100; // Maximum amount of health the player can have
    [SerializeField]
    private int currentHealth; // Current amount of health the player has
    [SerializeField]
    private Animator animator; // Reference to the Animator component

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Set current health to maximum health when the game starts
        currentHealth = maxHealth;
        // Get reference to the Animator component
        animator = GetComponent<Animator>();
    }

    // The Hurt function decreases the player's health by a certain amount and checks if they should die
    public void Hurt(int damage)
    {
        // If the damage is negative, call the Heal function instead and return
        if (damage < 0)
        {
            Heal(-damage);
            return;
        }
        
        // Decrease current health by the amount of damage taken
        currentHealth -= damage;

        // If health is still greater than 0, play Hurt animation
        if (currentHealth > 0)
        {
            // If the Animator component is available, play the Hurt animation
            if(animator != null)
            {
                animator.SetTrigger("Hurt");
            }
        }
        // If health is 0 or below, call Die function
        else
        {
            Die();
        }
    }

    // The Heal function increases the player's health by a certain amount
    public void Heal(int healAmount)
    {
        // If the heal amount is negative, call the Hurt function instead and return
        if (healAmount < 0)
        {
            Hurt(-healAmount);
            return;
        }
        
        // Increase current health by the heal amount
        currentHealth += healAmount;

        // Make sure health doesn't exceed max health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    // The Die function is called when the player's health reaches 0
    private void Die()
    {
        // If the Animator component is available, play the Death animation
        if(animator != null)
        {
            animator.SetTrigger("Death");
        }
        // disable player control, collider etc. 
        // depends on your implementation
    }
}