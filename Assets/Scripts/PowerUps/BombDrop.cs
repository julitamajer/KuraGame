using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BombDrop : ChicksPowerUpBehaviour
{
    [SerializeField] private GameObject _bomPrefab;
    [SerializeField] private GameObject _buttonObj;

    private Button _button;

    private void Start()
    {
        GameObject parent = GameObject.Find("Canvas");
        Transform childTransform = parent.transform.Find("BombButton");
        _buttonObj = childTransform.gameObject;
        Debug.Log(_buttonObj.name);

        _button = _buttonObj.GetComponent<Button>();
        _button.onClick.AddListener(DropTheBomb);


        _buttonObj.SetActive(true);
    }

    public void DropTheBomb()
    {
        GameObject newBomb = Instantiate(_bomPrefab, transform.position, Quaternion.identity);
        _buttonObj.SetActive(false);
        Destroy(gameObject);
    }
}
