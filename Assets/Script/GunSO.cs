using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Gun", menuName = "Guns/Gun",order = 0)]
public class GunSO : ScriptableObject
{
    public GunType type;
    public string gunName;
    public GameObject modelPrefab;
    public Vector3 spawnPoint;
    public Vector3 spawnRotation;

    public ShootConfigurationSO shootConfiguration;
    public TrailConfigSO trailConfig;

    private MonoBehaviour _monoBehaviour;
    private GameObject _model;
    private float _lastShootTime = 0f;
    private ParticleSystem _shootSystem;
    private ObjectPool<TrailRenderer> _trailPool;

    public void Spawn(Transform parent, MonoBehaviour monoBehaviour)
    {
        _monoBehaviour = monoBehaviour;
        _lastShootTime = 0;
        _trailPool = new ObjectPool<TrailRenderer>(CreateTrail);

        _model = Instantiate(modelPrefab);
        _model.transform.SetParent(parent, false);
        _model.transform.localPosition = spawnPoint;
        _model.transform.localRotation = Quaternion.Euler(spawnRotation);

        _shootSystem = _model.GetComponentInChildren<ParticleSystem>();
    }

    public void Shoot()
    {
        if (Time.time > shootConfiguration.fireRate + _lastShootTime)
        {
            _lastShootTime = Time.time; // Update the time of the last shot

            _shootSystem.Play();
            Vector3 shootDirection = _shootSystem.transform.forward +
                new Vector3(UnityEngine.Random.Range(-shootConfiguration.spread.x, shootConfiguration.spread.x),
                            UnityEngine.Random.Range(-shootConfiguration.spread.y, shootConfiguration.spread.y),
                            UnityEngine.Random.Range(-shootConfiguration.spread.z, shootConfiguration.spread.z));

            shootDirection.Normalize();

            if (Physics.Raycast(
                _shootSystem.transform.position,
                shootDirection,
                out RaycastHit hit,
                float.MaxValue,
                shootConfiguration.hitMask))
            {
                _monoBehaviour.StartCoroutine(
                    PlayTrail(_shootSystem.transform.position, hit.point, hit));
            }
            else
            {
                _monoBehaviour.StartCoroutine(
                    PlayTrail(_shootSystem.transform.position, _shootSystem.transform.position + (shootDirection * trailConfig.missDistance),
                    new RaycastHit()));
            }
        }
    }

    private IEnumerator PlayTrail(Vector3 startPoint, Vector3 endPoint, RaycastHit hit)
    {
        TrailRenderer instance = _trailPool.Get();
        instance.gameObject.SetActive(true);
        instance.transform.position = startPoint;   
        yield return null;

        instance.emitting = true;

        float distance = Vector3.Distance(startPoint, endPoint);
        float remainingDistance = distance;

        while(remainingDistance > 0)
        {
            instance.transform.position = Vector3.Lerp(startPoint, endPoint, Mathf.Clamp01(1 - remainingDistance/distance));

            remainingDistance -= trailConfig.simulationSpeed *Time.deltaTime;
            yield return null;
        }

        instance.transform.position = endPoint;

        if(hit.collider != null)
        {
            if(hit.collider.GetComponent<EnemyHP>()) 
            {
                hit.collider.GetComponent<EnemyHP>().TakeDamage(1);
                Debug.Log(hit.collider.name);

            }

            if (hit.collider.GetComponent<PlayerHP>())
            {
                hit.collider.GetComponent<PlayerHP>().TakeDamage(1);
                Debug.Log(hit.collider.name);

            }
        }

        yield return new WaitForSeconds(trailConfig.duration);
        yield return null;
        instance.emitting = false;
        instance.gameObject.SetActive(true);
        _trailPool.Release(instance);
    }

    private TrailRenderer CreateTrail()
    {
        GameObject instance = new GameObject("Bullet Trail");
        TrailRenderer trail = instance.AddComponent<TrailRenderer>();
        trail.colorGradient = trailConfig.color;
        trail.material = trailConfig.material;
        trail.widthCurve = trailConfig.witdhCurve;
        trail.time = trailConfig.duration;
        trail.minVertexDistance =  trailConfig.minVertexDistance;

        trail.emitting = false;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        return trail;
    }
}
