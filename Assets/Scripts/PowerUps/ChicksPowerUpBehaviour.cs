using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChicksPowerUpBehaviour : MonoBehaviour
{
    [SerializeField] private ChicksPowerUp _chicksPowerUp;

    protected float durationTime;
    private float _rotationSpeed;

    private GameObject rotation;

    private void Awake()
    {
        rotation = GameObject.Find("Rotation");
        transform.SetParent(rotation.transform);

        durationTime = _chicksPowerUp.durationTime;
        _rotationSpeed = _chicksPowerUp.rotationSpeed;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, _rotationSpeed, 0) * Time.deltaTime);
    }
}
