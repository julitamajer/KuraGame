using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPowerUp : MonoBehaviour
{
    [SerializeField] private List<GunSO> gunsSO = new List<GunSO>();
    private GunSO selectedGun;

   // public delegate void OnPlayerPickUp();
   //public static event OnPlayerPickUp onPlayerPickUp;

    private void Start()
    {
        selectedGun = RandomizeGun();
        Debug.Log("Selected Gun: " + selectedGun.ToString());
    }

    private GunSO RandomizeGun()
    {
        GunSO[] gunsArray = gunsSO.ToArray();

        int randomIndex = Random.Range(0, gunsArray.Length);

        return gunsArray[randomIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerGunSelector gunSelector) && other.gameObject.CompareTag("Player"))
        {
            // onPlayerPickUp?.Invoke();

            gunSelector.PickUpGun(selectedGun, selectedGun.type);
            Destroy(gameObject);
        }
    }
}
