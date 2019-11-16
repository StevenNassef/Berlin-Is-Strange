using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCanvas : GameComponent
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject levelPanel;


    protected override void GameResumed()
    {
        pausePanel.SetActive(false);
        levelPanel.SetActive(true);
    }
    protected override void GameStarted()
    {
        pausePanel.SetActive(false);
        levelPanel.SetActive(true);
    }
    protected override void GamePaused()
    {
        pausePanel.SetActive(true);
        levelPanel.SetActive(false);
    }

    protected override void GameLoaded()
    {
        pausePanel.SetActive(false);
        levelPanel.SetActive(false);
    }


}

