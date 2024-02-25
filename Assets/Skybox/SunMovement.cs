using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    public float dayDurationInSeconds = 120.0f;

    private Light sunLight;
    private float rotationDegreePerSecond;

    void Start()
    {
        sunLight = GetComponent<Light>();
        rotationDegreePerSecond = 360.0f / dayDurationInSeconds;
    }

    void Update()
    {
        transform.Rotate(Vector3.right, rotationSpeed * rotationDegreePerSecond * Time.deltaTime);
    }
}
