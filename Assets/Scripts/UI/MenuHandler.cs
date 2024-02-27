using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialPanel;

    public void LoadLevel() {
        SceneManager.LoadScene("Levels");
    }

    public void OpenTuto()
    {
        _tutorialPanel.SetActive(true);
    }

    public void CloseTuto()
    {
        _tutorialPanel.SetActive(false);

    }
}
