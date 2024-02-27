using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartProperties : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _eggsParent;
    [SerializeField] private GameObject _doors;
    [SerializeField] private UIBehaviour _uIBehaviour;

    private Vector3 _doorsPosition;
    private bool _doorsOnTrigger;
    private bool _isParentEmpty;

    public delegate void OnPlayerWin();
    public static event OnPlayerWin onPlayerWin;

    void Awake()
    {
        _doorsPosition = _doors.transform.position;
        Time.timeScale = 1;
    }

    void Start() 
    {
        Set_playerPosition();
    }

    void Update()
    {
        Check_eggsParent();
        EndGame();
    }

    void Set_playerPosition() 
    {
        _doorsPosition.x = 1f;
        _doorsPosition.y = 1f;
        _player.transform.position = _doorsPosition;
    }

    void EndGame()
    {
        if (_isParentEmpty && _doorsOnTrigger) {
            onPlayerWin?.Invoke();
        }
    }

    void Check_eggsParent()
    {
        _isParentEmpty = (_eggsParent.transform.childCount == 0);
        Debug.Log("Is Eggs Parent Empty: " + _isParentEmpty);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _doorsOnTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _doorsOnTrigger = false;
        }
    }
}
