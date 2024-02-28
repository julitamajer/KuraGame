using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private AudioSource click;

    public void LoadLevel() {
        click.Play();
        SceneManager.LoadScene("Levels");
    }

    public void OpenTuto()
    {
        click.Play();
        _tutorialPanel.SetActive(true);
    }

    public void CloseTuto()
    {
        click.Play();
        _tutorialPanel.SetActive(false);
    }
}
