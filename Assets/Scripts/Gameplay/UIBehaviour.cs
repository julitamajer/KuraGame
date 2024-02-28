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

    [Header("Pause")]
    [SerializeField] GameObject pauseMenu;
    bool isPauseMenuOn;

    [Header("Time")]
    public Slider slider;
    private float _targetValue;
    private float _startValue;
    private float _timeRemaining;

    [Header("End Game Panel")]
    [SerializeField] private GameObject _onEndGamePanel;
    [SerializeField] private TextMeshProUGUI _textEndGame;

    [SerializeField] private GameObject _summaryPanel;
    [SerializeField] private TextMeshProUGUI _summaryTimeLeft;
    [SerializeField] private TextMeshProUGUI _summarySeed;
    [SerializeField] private List<GameObject> _starsObj = new List<GameObject>();

    [Header("Guns")]
    [SerializeField] private List<GameObject> _gunsUI = new List<GameObject>();
    [SerializeField] private PlayerGunSelector _playerGunSelector;
    private GunType type;

    [SerializeField] private int maxChickens;
    [SerializeField] float duration = 120.0f;

    [SerializeField] private int maxScore = 0; 
    [SerializeField] private int maxStars = 3;

    [SerializeField] private int collectedChickens = 0;
    private float _elapsedTime = 0.0f;
    [SerializeField] private int score = 0;

    public delegate void OnTimeChange(string state);
    public static event OnTimeChange onTimeChange;

    private bool startInvoked = false;
    private bool middleInvoked = false;
    private bool endInvoked = false;

    private void OnEnable()
    {
        Seed.onPickedUpSeed += AddSeeds;
        PlayerHP.onPlayerDead += PlayerDeath;
        PlayerController.onPlayerDead += PlayerDeath;
        SetStartProperties.onPlayerWin += PlayerWin;
        SetStartProperties.onGameStart += SetMaxChicks;
        Clock.onPickedUpClock += AddTime;
    }

    private void SetMaxChicks(int max)
    {
        maxChickens = max;
    }

    void CalculateMaxScore()
    {
        float chickenScore = maxChickens * 500f / maxChickens; // Maximum score for chickens is 500 per chicken
        float timeScore = duration / duration * 500f; // Maximum score for time is 500
        maxScore = Mathf.RoundToInt(chickenScore + timeScore);
    }

    private void Start()
    {
        CalculateMaxScore();

        slider.value = 100f;
        _targetValue = 0f;
        _startValue = slider.value;

        type = _playerGunSelector.activeGun.type;

        if (!isPauseMenuOn)
            StartCoroutine(DecreaseSliderValue());
    }

    private void Update()
    {
        if(slider.value <= 0f)
        {
            PlayerDeath("Time is over!");
        }

        type = _playerGunSelector.activeGun.type;
        DisplayGuns();
        OnChangeTime();
    }

    private void OnChangeTime()
    {
        if (slider.value >= 0.95f && !startInvoked)
        {
            onTimeChange?.Invoke("start");
            startInvoked = true;
        }
        else if (slider.value <= 0.60f && slider.value >= 0.50f && !middleInvoked)
        {
            onTimeChange?.Invoke("middle");
            middleInvoked = true;
        }
        else if (slider.value <= 0.30f && slider.value >= 0.20f && !endInvoked)
        {
            onTimeChange?.Invoke("end");
            endInvoked = true;
        }
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

    private IEnumerator DecreaseSliderValue()
    {
        while (_elapsedTime < duration)
        {
            float newValue = Mathf.Lerp(_startValue, _targetValue, _elapsedTime / duration);
            slider.value = newValue;
            _elapsedTime += Time.deltaTime;

            yield return null;
        }

        slider.value = _targetValue;
    }

    private void AddTime(float addedTime)
    {
        slider.value += addedTime;
        _elapsedTime -= addedTime; 
    }

    private void PlayerDeath(string text)
    {
        _onEndGamePanel.SetActive(true);
        Time.timeScale = 0;
        _textEndGame.SetText(text);
    }

    private void PlayerWin(int collected)
    {
        _timeRemaining = duration - _elapsedTime;
        _summaryPanel.SetActive(true);
        Time.timeScale = 0;
        _summarySeed.SetText(_seedCount.ToString("0"));
        _summaryTimeLeft.SetText((duration - _elapsedTime).ToString("0") + "s");

 
        collectedChickens = collected;

        score = CalculateScore();
        int stars = CalculateStars(score);

        Debug.Log("Stars: " + stars);

        switch (stars)
        {
            case 0:
                _starsObj[0].SetActive(true);
                PlayerPrefs.SetInt("starRate", stars);
                break;
            case 1:
                _starsObj[1].SetActive(true);
                PlayerPrefs.SetInt("starRate", stars);
                break;
            case 2:
                _starsObj[2].SetActive(true);
                PlayerPrefs.SetInt("starRate", stars);
                break;
            case 3:
                _starsObj[3].SetActive(true);
                PlayerPrefs.SetInt("starRate", stars);
                break;
        }

        PlayerPrefs.SetString("seeds", _seedCount.ToString("0"));
        PlayerPrefs.SetString("time", (duration - _elapsedTime).ToString("0") + "s");
    }

    int CalculateScore()
    {
        float chickenScore = (float)collectedChickens / maxChickens * 500f; // Maximum score for chickens is 500 per chicken
        float timeScore = (duration - _elapsedTime) / duration * 500f; // Maximum score for time is 500
        chickenScore = Mathf.Max(chickenScore, 0f);
        timeScore = Mathf.Max(timeScore, 0f);
        return Mathf.RoundToInt(chickenScore + timeScore);
    }

    int CalculateStars(int score)
    {
        float percent = (float)score / maxScore;
        int starsEarned = Mathf.RoundToInt(percent * maxStars);
        starsEarned = Mathf.Clamp(starsEarned, 0, maxStars);
        return starsEarned;
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
                _gunsUI[0].SetActive(true);
                _gunsUI[1].SetActive(false);
                _gunsUI[2].SetActive(false);
                break;
            case GunType.MachineGun:
                _gunsUI[0].SetActive(false);
                _gunsUI[1].SetActive(true);
                _gunsUI[2].SetActive(false);
                break;
            case GunType.Shotgun:
                _gunsUI[0].SetActive(false);
                _gunsUI[1].SetActive(false);
                _gunsUI[2].SetActive(true);
                break;
        }
    }

    private void OnDisable()
    {
        Seed.onPickedUpSeed -= AddSeeds;
        PlayerHP.onPlayerDead -= PlayerDeath;
        PlayerController.onPlayerDead -= PlayerDeath;
        SetStartProperties.onPlayerWin -= PlayerWin;
        Clock.onPickedUpClock -= AddTime;
    }
}
