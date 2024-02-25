using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.LowLevel;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI seeds;
    private int _seedCount;

    private void OnEnable()
    {
        Seed.onPickedUpSeed += AddSeeds;
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
