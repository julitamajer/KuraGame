using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Chicks")]
public class ChicksPowerUp : ScriptableObject
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public float durationTime;
    [SerializeField] public float rotationSpeed;
}
