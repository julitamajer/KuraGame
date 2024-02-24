using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="Trail Config", menuName = "Guns/Gun Trail Config", order = 4)]
public class TrailConfigSO : ScriptableObject
{
    public Material material;
    public AnimationCurve witdhCurve;
    public float duration = 0.5f;
    public float minVertexDistance = 0.1f;
    public Gradient color;

    public float missDistance = 100f;
    public float simulationSpeed = 25f;
}
