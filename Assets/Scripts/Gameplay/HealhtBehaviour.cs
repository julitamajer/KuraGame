using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealhtBehaviour : MonoBehaviour
{
    public int maxHealth = 3;
    public HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = new HealthSystem(maxHealth);
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(object sender, EventArgs e)
    {
        // You can update UI elements or perform other actions based on the health change here
        CheckDead();
    }

    public void TakeDamage(int damageAmount)
    {
        healthSystem.Damage(damageAmount);
    }

    public void Heal(int healAmount)
    {
        healthSystem.Heal(healAmount);
    }

    private void CheckDead()
    {
        if (healthSystem.health <= 0)
        {
            Dead();
        }
    }

    public virtual void Dead()
    {
        Debug.Log("Dead " + name);
    }
}
