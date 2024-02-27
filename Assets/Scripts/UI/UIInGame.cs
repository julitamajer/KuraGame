using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private GameObject _pauseMenu;
    bool isPauseMenuOn;

    [Header("Time")]
    public Slider slider;
    [SerializeField] private float _duration = 120.0f; 
    private float _targetValue;
    private float _startValue;
    private float _elapsedTime = 0.0f;

    private void Start()
    {
        slider.value = 100f;
        _targetValue = 0f;
        _startValue = slider.value;

        if (!isPauseMenuOn)
            StartCoroutine(DecreaseSliderValue());
    }

    public void PauseMenu()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPauseMenuOn = true;
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPauseMenuOn = false;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Levels");
    }

    private IEnumerator DecreaseSliderValue()
    {
        while (_elapsedTime < _duration)
        {
            float newValue = Mathf.Lerp(_startValue, _targetValue, _elapsedTime / _duration);
            slider.value = newValue;
            _elapsedTime += Time.deltaTime;

            yield return null;
        }
        slider.value = _targetValue;
    }
}

