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

    private void OnEnable()
    {
        Seed.onPickedUpSeed += AddSeeds;
    }

    private void Start()
    {
        slider.value = 100f;
        targetValue = 0f;
        startValue = slider.value;

        if (!isPauseMenuOn)
            StartCoroutine(DecreaseSliderValue());
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
        SceneManager.LoadScene("Levels");
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

    private void OnDisable()
    {
        Seed.onPickedUpSeed -= AddSeeds;
    }
}
