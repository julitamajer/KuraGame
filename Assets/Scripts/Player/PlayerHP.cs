using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : HealhtBehaviour
{
    private void OnEnable()
    {
        Egg.onPickedEgg += AddHealth;
        GunSO.onMaxHealthDecrease += DecreaseHealth;
    }

    private void DecreaseHealth()
    {
        maxHealth -= 3;
    }

    private void AddHealth()
    {
        maxHealth += 3;
        health += 3; 
    }

    public override void Dead() 
    {
        Debug.Log("Dead " + name);
    }

    private void OnDisable()
    {
        Egg.onPickedEgg -= AddHealth;
        GunSO.onMaxHealthDecrease -= DecreaseHealth;
    }
}
