using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.LowLevel;
using UnityEngine.SceneManagement;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI seeds;
    private int _seedCount;

    //================PAUSE================
    [SerializeField] GameObject pauseMenu;
    bool isPauseMenuOn;

    //================TIME================
    public Slider slider;
    [SerializeField] float duration = 120.0f; //  minutes in seconds
    private float targetValue;
    private float startValue;
    private float elapsedTime = 0.0f;
    private float timeRemaining;

    //Win lose panels

    [SerializeField] private GameObject onEndGamePanel;
    [SerializeField] private TextMeshProUGUI textEndGame;

    [SerializeField] private GameObject summaryPanel;
    [SerializeField] private TextMeshProUGUI summaryTimeLeft;
    [SerializeField] private TextMeshProUGUI summarySeed;
    [SerializeField] private List<GameObject> starsObj = new List<GameObject>();

    // guns

    [SerializeField] private List<GameObject> gunsUI = new List<GameObject>();
    [SerializeField] PlayerGunSelector playerGunSelector;
    private GunType type;

    private void OnEnable()
    {
        Seed.onPickedUpSeed += AddSeeds;
        PlayerHP.onPlayerDead += PlayerDeath;
        PlayerController.onPlayerDead += PlayerDeath;
        SetStartProperties.onPlayerWin += PlayerWin;
    }

    private void Start()
    {
        slider.value = 100f;
        targetValue = 0f;
        startValue = slider.value;

        type = playerGunSelector.activeGun.type;

        if (!isPauseMenuOn)
            StartCoroutine(DecreaseSliderValue());
    }

    private void Update()
    {
        if(slider.value <= 0f)
        {
            PlayerDeath("Time is over!");
        }

        type = playerGunSelector.activeGun.type;
        DisplayGuns();
    }

    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPauseMenuOn = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPauseMenuOn = false;
    }

    public void LoadLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Levels");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void LoadAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Demo");
    }

    private System.Collections.IEnumerator DecreaseSliderValue()
    {
        while (elapsedTime < duration)
        {
            float newValue = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            slider.value = newValue;
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        slider.value = targetValue;
    }

    private void PlayerDeath(string text)
    {
        onEndGamePanel.SetActive(true);
        Time.timeScale = 0;
        textEndGame.SetText(text);
    }

    private void PlayerWin()
    {
        timeRemaining = duration - elapsedTime;
        summaryPanel.SetActive(true);
        Time.timeScale = 0;
        summarySeed.SetText(_seedCount.ToString("0"));
        summaryTimeLeft.SetText((duration - elapsedTime).ToString("0") + "s");

        int stars = CalculateStars();

        switch (stars)
        {
            case 0:
                starsObj[0].SetActive(true);
                PlayerPrefs.SetInt("starRate", stars);
                break;
            case 1:
                starsObj[1].SetActive(true);
                PlayerPrefs.SetInt("starRate", stars);
                break;
            case 2:
                starsObj[2].SetActive(true);
                PlayerPrefs.SetInt("starRate", stars);
                break;
            case 3:
                starsObj[3].SetActive(true);
                PlayerPrefs.SetInt("starRate", stars);
                break;
        }

        PlayerPrefs.SetString("seeds", _seedCount.ToString("0"));
        PlayerPrefs.SetString("time", (duration - elapsedTime).ToString("0") + "s");
    }

    private void AddSeeds()
    {
        _seedCount += 5;
        seeds.SetText($"{_seedCount}");

        switch (_seedCount)
        {
            case int count when _seedCount < 9:
                seeds.SetText($"000{_seedCount}");
                break;
            case int count when _seedCount > 9 && _seedCount < 99:
                seeds.SetText($"00{_seedCount}");
                break;
            case int count when _seedCount > 99 && _seedCount < 999:
                seeds.SetText($"0{_seedCount}");
                break;
            case int count when _seedCount > 999:
                seeds.SetText($"{_seedCount}");
                break;
        }
    }

    private void DisplayGuns()
    {
        switch (type)
        {
            case GunType.Glock:
                gunsUI[0].SetActive(true);
                gunsUI[1].SetActive(false);
                gunsUI[2].SetActive(false);
                break;
            case GunType.MachineGun:
                gunsUI[0].SetActive(false);
                gunsUI[1].SetActive(true);
                gunsUI[2].SetActive(false);
                break;
            case GunType.Shotgun:
                gunsUI[0].SetActive(false);
                gunsUI[1].SetActive(false);
                gunsUI[2].SetActive(true);
                break;
        }
    }

    public int CalculateStars()
    {
        int timeStars = 0;
        if (timeRemaining > 90)
            timeStars = 3;
        else if (timeRemaining >= 60 && timeRemaining <= 90)
            timeStars = 2;
        else if (timeRemaining >= 30 && timeRemaining < 60)
            timeStars = 1;
        else if (timeRemaining < 30)
            timeStars = 0;


        return timeStars;
    }



    private void OnDisable()
    {
        Seed.onPickedUpSeed -= AddSeeds;
        PlayerHP.onPlayerDead -= PlayerDeath;
        PlayerController.onPlayerDead -= PlayerDeath;
        SetStartProperties.onPlayerWin -= PlayerWin;
    }
}
