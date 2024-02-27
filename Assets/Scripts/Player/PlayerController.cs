using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Moving")]
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;

    [SerializeField] private float _smoothTime = 0.05f;
    private float _currentVelocity;

    public float speed;

    private bool _isWalking = false;

    [Header("Chicks")]
    [SerializeField] private GameObject _bodyPrefab;
    private List<GameObject> _bodyPartList = new List<GameObject>();
    private List<Vector3> _positionHistory = new List<Vector3>();

    [SerializeField] private int _gap;
    [SerializeField] private int _bodySpeed = 5;

    [SerializeField] private GameObject _bodyParent;

    public delegate void OnPlayerDead(string text);
    public static event OnPlayerDead onPlayerDead;

    [SerializeField] private PlayerGunSelector _gunSelector;

    private void OnEnable()
    {
        Egg.onPickedEgg += GrowTail;
        GunSO.onMaxHealthDecrease += DecreaseTail;
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_input.sqrMagnitude != 0)
        {
            var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, _smoothTime);

            transform.rotation = Quaternion.Euler(0, angle, 0);
            _characterController.Move(_direction * speed * Time.deltaTime);
        }

        Shoot();
        MoveChicks();
    }

    private void MoveChicks()
    {
        const int maxHistoryCount = 2000;

        if (_isWalking)
        {
            _positionHistory.Insert(0, transform.position);

            if (_positionHistory.Count > maxHistoryCount)
            {
                _positionHistory.RemoveRange(maxHistoryCount, _positionHistory.Count - maxHistoryCount);
            }
        }

        int index = 1;

        foreach (var body in _bodyPartList)
        {
            int pointIndex = Mathf.Min(index * _gap, _positionHistory.Count - 1);
            Vector3 currentPoint = _positionHistory[pointIndex];
            Vector3 nextPoint = _positionHistory[Mathf.Min((index + 1) * _gap, _positionHistory.Count - 1)];
            Vector3 interpolatedPosition = Vector3.Lerp(currentPoint, nextPoint, Time.smoothDeltaTime * _bodySpeed);

            body.transform.position = interpolatedPosition;
            index++;
        }
    }

    private void GrowTail()
    {
        GameObject body = Instantiate(_bodyPrefab);
        _bodyPartList.Add(body);
        body.transform.SetParent(_bodyParent.transform);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0, _input.y);
        _isWalking = _input.magnitude > 0;
    }

    public void Shoot()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Stationary) || (Mouse.current.leftButton.isPressed && _gunSelector.activeGun != null))
        {
            if (_gunSelector.activeGun != null)
            {
                _gunSelector.activeGun.Shoot();
            }
        }
    }

    public void DecreaseTail()
    {
        int numChildren = _bodyParent.transform.childCount;

        if (numChildren > 0)
        {
            _bodyPartList.RemoveAt(0);
            Destroy(_bodyParent.transform.GetChild(0).gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Body"))
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Over");
        onPlayerDead?.Invoke("You stepped your chicks!");
    }

    private void OnDisable()
    {
        Egg.onPickedEgg -= GrowTail;
        GunSO.onMaxHealthDecrease -= DecreaseTail;
    }
}
