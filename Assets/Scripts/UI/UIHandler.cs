using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [Header("Levels")]
    public List<Transform> levels = new List<Transform>();
    [SerializeField] private GameObject _levelParent;
    public List<Level> createdLevels = new List<Level>();

    [Header("Panel")]
    
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _time;
    [SerializeField] private TextMeshProUGUI _seeds;

    public GameObject infoPanel;
    private int clickedLevel;
    public Button panelButton;
    public GameObject[] stars = new GameObject[4];

    [SerializeField] private AudioSource _click;

    void Awake() 
    {
        CopyListOfLevel();
        CreatedLevels();
    }

    void Update() 
    {
        CheckForClickedButton();
    }

    void CreatedLevels() 
    {
        for(int i = 0; i < levels.Count; i++)
        {
            Level levelToCreate = new Level();

            createdLevels.Add(levelToCreate);
            Debug.Log(createdLevels[i]);
        }
    }

    void CopyListOfLevel() 
    {
        Transform transformLevels = _levelParent.transform;

        foreach(Transform child in transformLevels){
            levels.Add(child);
            Debug.Log(child.name);
        }
    }

    void CheckForClickedButton() 
    {
        for(int i = 0; i < levels.Count; i++)
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
                i = 0;
            }
        }
    }

    void DispleyLevelInfo(int number) 
    {
        _title.SetText(createdLevels[number].number);
        _time.SetText(PlayerPrefs.GetString("time"));
        _seeds.SetText(PlayerPrefs.GetString("seeds"));

        switch(PlayerPrefs.GetInt("starRate"))
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

    public void ExitPanel() 
    {
        
        createdLevels[clickedLevel].clicked = false;
        infoPanel.SetActive(false);
        _click.Play();
    }

    public void LoadLevel() 
    {
        SceneManager.LoadScene("Demo");
        _click.Play();
    }
}

public class Level {
    public string number;
    public float _time;
    public int _seeds;
    public int stars;
    public bool passed;
    public bool clicked;
}
