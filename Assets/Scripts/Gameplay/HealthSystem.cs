using System;
using UnityEngine;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;

    public int health;
    private int healthMax;

    public HealthSystem(int healthMax, int health)
    {
        this.healthMax = healthMax;
        this.health = health;
        health = healthMax;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health < 0) health = 0;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
        Debug.Log(health);
    }

    public void Heal(int heathAmount)
    {
        health += heathAmount;
        if (health > healthMax) health = healthMax;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }
}
