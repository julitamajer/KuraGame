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
    [SerializeField] float duration = 120.0f; //  minutes in seconds
    private float _targetValue;
    private float _startValue;
    private float _elapsedTime = 0.0f;
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

    private void PlayerDeath(string text)
    {
        _onEndGamePanel.SetActive(true);
        Time.timeScale = 0;
        _textEndGame.SetText(text);
    }

    private void PlayerWin()
    {
        _timeRemaining = duration - _elapsedTime;
        _summaryPanel.SetActive(true);
        Time.timeScale = 0;
        _summarySeed.SetText(_seedCount.ToString("0"));
        _summaryTimeLeft.SetText((duration - _elapsedTime).ToString("0") + "s");

        int stars = CalculateStars();

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

    public int CalculateStars()
    {
        int timeStars = 0;
        if (_timeRemaining > 90)
            timeStars = 3;
        else if (_timeRemaining >= 60 && _timeRemaining <= 90)
            timeStars = 2;
        else if (_timeRemaining >= 30 && _timeRemaining < 60)
            timeStars = 1;
        else if (_timeRemaining < 30)
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
