using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartProperties : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject doors;
    [SerializeField] UIBehaviour uIBehaviour;

    Vector3 doorsPosition;
    bool doorsOnTrigger;

    public delegate void OnPlayerWin();
    public static event OnPlayerWin onPlayerWin;

    void Awake()
    {
        doorsPosition = doors.transform.position;
        Time.timeScale = 1;
    }

    void Start() {
        SetPlayerPosition();
    }

    void Update()
    {
        EndGame();
    }

    void SetPlayerPosition() {
        doorsPosition.x = 1f;
        doorsPosition.y = 1f;
        player.transform.position = doorsPosition;
    }

    void EndGame(){
        if(uIBehaviour.slider.value <= 0.8f && doorsOnTrigger) {
            onPlayerWin?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            doorsOnTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            doorsOnTrigger = false;
        }
    }
}
