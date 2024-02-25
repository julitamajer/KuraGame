using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;

    public void LoadLevel() {
        SceneManager.LoadScene("Levels");
    }

    public void OpenTuto()
    {
        tutorialPanel.SetActive(true);
    }

    public void CloseTuto()
    {
        tutorialPanel.SetActive(false);

    }
}
