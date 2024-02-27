using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealhtBehaviour : MonoBehaviour
{
    public int maxHealth = 3;
    public int health = 3;
    public HealthSystem healthSystem;
    public bool immunity = false;

    private void Start()
    {
        healthSystem = new HealthSystem(maxHealth, health);
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(object sender, EventArgs e)
    {
        CheckDead();
    }

    public void TakeDamage(int damageAmount)
    {
        if (!immunity)
        {
            healthSystem.Damage(damageAmount);
        }
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
