using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour
{
    Transform parentTransform;
    GameObject parentObject;
    UIHandler uiHandler;
    int childPosition;

    //==================LEVEL INFO====================

    [SerializeField] int number;
    [SerializeField] float time;
    [SerializeField] int stars;
    [SerializeField] int seeds;
    [SerializeField] bool passed;
    [SerializeField] bool clicked;

    void Awake() {
        parentTransform = transform.parent;
        parentObject = parentTransform.gameObject;
        uiHandler = parentObject.GetComponent<UIHandler>();
    }

    void Start() {
        CheckWhichChildObjectIs();
        CreateLevel();
    }

    void CheckWhichChildObjectIs() {
        
        for (int i = 0; i < uiHandler.levels.Count; i++)
        {
            if (gameObject == uiHandler.levels[i].gameObject)
            {
                childPosition = i;
                break;
            }
        }
    }

    public void ClickedButton() {
        clicked = true;
        uiHandler.createdLevels[childPosition].clicked = clicked;
    }
    
    void CreateLevel() {
        uiHandler.createdLevels[childPosition].number = $"Level {(childPosition+1).ToString()}";
        uiHandler.createdLevels[childPosition].time = time;
        uiHandler.createdLevels[childPosition].seeds = seeds;
        uiHandler.createdLevels[childPosition].stars = stars;
        uiHandler.createdLevels[childPosition].passed = passed;
    }
}
