using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunSelector : MonoBehaviour
{
    [SerializeField] private GunType _gun;
    [SerializeField] private Transform _gunParent;
    [SerializeField] private List<GunSO> _guns;

    [Space]
    [Header("Runtime Filled")]
    public GunSO activeGun;

    private void Start()
    {
        GunSO gun = _guns.Find(gun => gun.type == _gun);
        activeGun = gun;
        gun.Spawn(_gunParent, this);
    }
}
