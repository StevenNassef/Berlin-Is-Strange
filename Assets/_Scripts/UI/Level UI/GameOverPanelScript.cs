using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanelScript : GamePanel
{
    // Start is called before the first frame update
    [SerializeField] private GameObject starsPanel;
    [SerializeField] private GameObject loseButtonPanel;
    [SerializeField] private GameObject winButtonPanel;
    protected override void InitializePanel()
    {
        if (GameManager.instance.isGameEnded)
        {
            if (GameManager.instance.gameState == GameState.GAMEOVER_WIN)
            {
                starsPanel.SetActive(true);
                loseButtonPanel.SetActive(false);
                winButtonPanel.SetActive(true);
            }
            else
            {
                starsPanel.SetActive(false);
                loseButtonPanel.SetActive(true);
                winButtonPanel.SetActive(false);
            }
        }
    }

    protected override void OnComponentEnabled()
    {
        InitializePanel();
    }
}
