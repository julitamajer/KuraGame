using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : HealhtBehaviour
{
    public delegate void OnPlayerDead(string text);
    public static event OnPlayerDead onPlayerDead;

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
        onPlayerDead?.Invoke("You are dead!");
    }

    private void OnDisable()
    {
        Egg.onPickedEgg -= AddHealth;
        GunSO.onMaxHealthDecrease -= DecreaseHealth;
    }
}
