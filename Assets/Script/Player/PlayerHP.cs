using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : HealhtBehaviour
{
    public override void Dead() 
    {
        Debug.Log("Dead " + name);
    }
}
