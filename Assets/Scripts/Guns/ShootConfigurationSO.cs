using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Shoot Config", menuName = "Guns/Shoot Configuration", order = 2)]
public class ShootConfigurationSO : ScriptableObject
{
    public LayerMask hitMask;
    public int bulletPerShot = 1;
    public Vector3 spread = new Vector3 (0.1f, 0.1f, 0.1f);

    public float fireRate;
}
