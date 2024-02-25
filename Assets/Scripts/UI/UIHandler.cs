using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    // ===============LEVELS==================
    public List<Transform> levels = new List<Transform>();
    [SerializeField] GameObject levelParent;
    public List<Level> createdLevels = new List<Level>();

    // ===============PANEL==================
    public GameObject infoPanel;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI seeds;
    int clickedLevel;
    public Button panelButton;
    public GameObject[] stars = new GameObject[4];
    

    void Awake() {
        CopyListOfLevel();
        CreatedLevels();
    }


    void Update() {
        CheckForClickedButton();
    }


    void CreatedLevels() {
        for(int i = 0; i<levels.Count; i++)
        {
            Level levelToCreate = new Level();
            createdLevels.Add(levelToCreate);
            Debug.Log(createdLevels[i]);
        }
    }

    void CopyListOfLevel() {
        Transform transformLevels = levelParent.transform;

        foreach(Transform child in transformLevels){
            levels.Add(child);
            Debug.Log(child.name);
        }
    }

    void CheckForClickedButton() {
        for(int i = 0; i<levels.Count; i++)
        {
            if(createdLevels[i].clicked)
            {
                infoPanel.SetActive(true);
                clickedLevel = i;
                DispleyLevelInfo(clickedLevel);
                createdLevels[clickedLevel].clicked = false;
            }
            
            if(i == levels.Count)
            {
                i=0;
            }
        }
    }

    void DispleyLevelInfo(int number) {
        title.SetText(createdLevels[number].number);
        time.SetText(createdLevels[number].time.ToString());
        seeds.SetText(createdLevels[number].seeds.ToString());

         switch(createdLevels[number].stars)
        {
            case 1:
                stars[0].SetActive(false);
                stars[1].SetActive(true);
                stars[2].SetActive(false);
                stars[3].SetActive(false);
                break;
            case 2:
                stars[0].SetActive(false);
                stars[1].SetActive(false);
                stars[2].SetActive(true);
                stars[3].SetActive(false);
                break;
            case 3:
                stars[0].SetActive(false);
                stars[1].SetActive(false);
                stars[2].SetActive(false);
                stars[3].SetActive(true);
                break;
            default:
                stars[0].SetActive(true);
                stars[1].SetActive(false);
                stars[2].SetActive(false);
                stars[3].SetActive(false);
                break;
        }
    }

    public void ExitPanel() {
        createdLevels[clickedLevel].clicked = false;
        infoPanel.SetActive(false);
    }

    public void LoadLevel() {
        SceneManager.LoadScene("Demo");
    }
}

public class Level {
    public string number;
    public float time;
    public int seeds;
    public int stars;
    public bool passed;
    public bool clicked;
}
