using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Speed : ChicksPowerUpBehaviour
{
    private PlayerController _player;
    private float _speedOld;

    private void Start()
    {
        _player = FindAnyObjectByType<PlayerController>();
        _speedOld = _player.speed;

        StartCoroutine(SpeedOn());
    }

    private IEnumerator SpeedOn()
    {
        _player.speed *= 2;

        yield return new WaitForSeconds(durationTime);

        _player.speed = _speedOld;
        Destroy(gameObject);

        StopCoroutine(SpeedOn());
    }
}
