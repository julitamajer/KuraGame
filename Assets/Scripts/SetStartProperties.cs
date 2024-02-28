using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartProperties : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _eggsParent;
    [SerializeField] private GameObject _doors;
    [SerializeField] private UIBehaviour _uIBehaviour;
    [SerializeField] private GameObject _bodyParts;

    private int _maxChicks;
    private int _collectedChicks;

    private bool _firstLeft;

    private Vector3 _doorsPosition;
    private bool _doorsOnTrigger;
    private bool _isParentEmpty;

    public delegate void OnPlayerWin(int collected);
    public static event OnPlayerWin onPlayerWin;

    public delegate void OnGameStart(int max);
    public static event OnGameStart onGameStart;

    void Awake()
    {
        _doorsPosition = _doors.transform.position;
        Time.timeScale = 1;
        _maxChicks = CountChildren(_eggsParent);
        onGameStart?.Invoke(_maxChicks);
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
        if (_firstLeft && _doorsOnTrigger) 
        {
            _collectedChicks = CountChildren(_bodyParts);
            onPlayerWin?.Invoke(_collectedChicks);
        }
    }

    void Check_eggsParent()
    {
        _isParentEmpty = (_eggsParent.transform.childCount == 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _firstLeft)
        {
            _doorsOnTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _firstLeft = true;
            _doorsOnTrigger = false;
        }
    }

    private int CountChildren(GameObject obj)
    {
        int count = 0;
        count = obj.transform.childCount;


        return count;
    }
}
