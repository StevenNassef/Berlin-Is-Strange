using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }
    public delegate void GameControlsWithBoolean(bool isEnabled);
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            _instance = this;
        }
        // DontDestroyOnLoad(gameObject);
        GameManager.OnGameInitialized += LevelInitialization;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LevelInitialization()
    {

    }

    public void EndGame(GameState state)
    {
        switch (state)
        {
            case GameState.GAMEOVER_LOSE:
                GameManager.instance.GameOver(state);
                break;
            case GameState.GAMEOVER_WIN:
                GameManager.instance.GameOver(state);
                break;
            default:
                break;
        }
    }

    public void RestartGame()
    {
        GameManager.instance.RestartGame();
    }
    public void PauseGame()
    {
        GameManager.instance.PauseGame();
    }

    public void ResumeGame()
    {
        GameManager.instance.ResumeGame();
    }
}

public enum GameState
{
    GAMEOVER_LOSE, GAMEOVER_WIN
}
