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
        SetupGun(gun);
    }
    private void SetupGun(GunSO gunSet)
    {
        activeGun = gunSet.Clone() as GunSO;
        activeGun.Spawn(_gunParent, this);
    }

    public void DespawnActiveGun()
    {
        if (activeGun != null)
        {
            activeGun.Despawn();
        }

        DestroyImmediate(activeGun);
    }

    public void PickUpGun(GunSO gun)
    {
        DespawnActiveGun();
        _gun = gun.type;
        SetupGun(gun);
    }
}
