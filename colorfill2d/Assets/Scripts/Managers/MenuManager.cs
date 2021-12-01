using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject inGameDonePanel;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject levelDonePanel;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if (GameManager.Instance.isLevelDone)
        {
            levelDonePanel.SetActive(true);
            Time.timeScale = 0;
        }
        if (PlayerControl.Instance.isDead)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OnClick_PauseButton()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnClick_ContinueButton()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClick_RestartButton()
    {
        Time.timeScale = 1;
        inGameDonePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        levelDonePanel.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
