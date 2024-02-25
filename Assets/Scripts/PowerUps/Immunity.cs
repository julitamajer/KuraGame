using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immunity : ChicksPowerUpBehaviour
{
    private GameObject _player;
    private PlayerHP _playerHP;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _playerHP = _player.GetComponent<PlayerHP>();

        StartCoroutine(ImmunityOn());
    }

    private IEnumerator ImmunityOn()
    {
        _playerHP.immunity = true;

        yield return new WaitForSeconds(durationTime);

        _playerHP.immunity = false;

        Destroy(gameObject);

        StopCoroutine(ImmunityOn());
    }
}
