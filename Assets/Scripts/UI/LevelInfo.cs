using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour
{
    [Header("Level info")]
    [SerializeField] private int _number;
    [SerializeField] private float _time;
    [SerializeField] private int _stars;
    [SerializeField] private int _seeds;
    [SerializeField] private bool _passed;
    [SerializeField] private bool _clicked;

    private Transform _parentTransform;
    private GameObject _parentObject;
    private UIHandler _uiHandler;
    private int _childPosition;

    void Awake() 
    {
        _parentTransform = transform.parent;
        _parentObject = _parentTransform.gameObject;
        _uiHandler = _parentObject.GetComponent<UIHandler>();
    }

    void Start() 
    {
        CheckWhichChildObjectIs();
        CreateLevel();
    }

    void CheckWhichChildObjectIs() 
    { 
        for (int i = 0; i < _uiHandler.levels.Count; i++)
        {
            if (gameObject == _uiHandler.levels[i].gameObject)
            {
                _childPosition = i;
                break;
            }
        }
    }

    public void ClickedButton() 
    {
        _clicked = true;
        _uiHandler.createdLevels[_childPosition].clicked = _clicked;
    }
    
    void CreateLevel() 
    {
        _uiHandler.createdLevels[_childPosition].number = $"Level {(_childPosition+1).ToString()}";
        _uiHandler.createdLevels[_childPosition]._time = _time;
        _uiHandler.createdLevels[_childPosition]._seeds = _seeds;
        _uiHandler.createdLevels[_childPosition].stars = _stars;
        _uiHandler.createdLevels[_childPosition].passed = _passed;
    }
}
