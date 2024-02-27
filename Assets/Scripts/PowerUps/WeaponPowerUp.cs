using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPowerUp : PickableItems
{
    [SerializeField] private List<GunSO> _gunsSO = new List<GunSO>();
    private GunSO selectedGun;

    private void Start()
    {
        selectedGun = RandomizeGun();
    }

    private GunSO RandomizeGun()
    {
        GunSO[] gunsArray = _gunsSO.ToArray();
        int randomIndex = Random.Range(0, gunsArray.Length);

        return gunsArray[randomIndex];
    }

    public override void OnPickedUp(Collider other)
    {
        PlayerGunSelector gunSelector = other.GetComponent<PlayerGunSelector>();
        gunSelector.PickUpGun(selectedGun);
        Destroy(gameObject);
    }
}
