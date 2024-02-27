using System;
using UnityEngine;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;

    public int health;
    private int _healthMax;

    public HealthSystem(int _healthMax, int health)
    {
        this._healthMax = _healthMax;
        this.health = health;
        health = _healthMax;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;

        if (health < 0) health = 0;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public void Heal(int heathAmount)
    {
        health += heathAmount;

        if (health > _healthMax) health = _healthMax;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }
}
