using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChicksPowerUpBehaviour : MonoBehaviour
{
    [SerializeField] private ChicksPowerUp chicksPowerUp;

    protected float durationTime;
    private float rotationSpeed;

    private GameObject rotation;

    private void Awake()
    {
        rotation = GameObject.Find("Rotation");
        transform.SetParent(rotation.transform);

        durationTime = chicksPowerUp.durationTime;
        rotationSpeed = chicksPowerUp.rotationSpeed;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
    }
}
