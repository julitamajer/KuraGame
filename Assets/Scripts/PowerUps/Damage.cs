using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : ChicksPowerUpBehaviour
{
    private GameObject _player;
    private PlayerGunSelector _playerGunSelector;
    private int _oldDamage;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _playerGunSelector = _player.GetComponent<PlayerGunSelector>();
        _oldDamage = _playerGunSelector.activeGun.enemyDamage;

        StartCoroutine(MoreDamage());
    }

    private IEnumerator MoreDamage()
    {
        _playerGunSelector.activeGun.enemyDamage += 2;

        yield return new WaitForSeconds(durationTime);

        _playerGunSelector.activeGun.enemyDamage = _oldDamage;
        Destroy(gameObject);

        StopCoroutine(MoreDamage());
    }
}
