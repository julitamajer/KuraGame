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
        _buttonObj = GameObject.Find("BombButton");

        _button = _buttonObj.GetComponent<Button>();
        _button.onClick.AddListener(DropTheBomb);

        _buttonObj.GetComponent<Image>().enabled = true;
    }

    public void DropTheBomb()
    {
        GameObject newBomb = Instantiate(_bomPrefab, transform.position, Quaternion.identity);

        _buttonObj.GetComponent<Image>().enabled = false;
        Destroy(gameObject);
    }
}
