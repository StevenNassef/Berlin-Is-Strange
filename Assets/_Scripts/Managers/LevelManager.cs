using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private Level _currentLevel;
    public Level CurrentLevel
    {
        get { return _currentLevel; }
    }

    private int currentLevelNumber;
    private GameObject currentLevelPrefab;

    private int currentNumberOfFlicks;
    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }
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
        GameManager.OnGameLoaded += DestoryLevel;
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
        if (currentLevelPrefab != null)
            Destroy(currentLevelPrefab);
        currentLevelPrefab = Instantiate(CurrentLevel.LevelPrefab);

        _currentLevel.CurrentScore = 0;
        currentNumberOfFlicks = 0;
    }
    public void LoadNextLevel()
    {


    }

    public void LoadLevel(Level value, int LevelNumber)
    {
        _currentLevel = value;
        currentLevelNumber = LevelNumber;
        GameManager.instance.StartLevel();

    }

    private void DestoryLevel()
    {
        Destroy(currentLevelPrefab);
    }

    public void updateFlicks()
    {
        currentNumberOfFlicks++;
        _currentLevel.CurrentScore = currentNumberOfFlicks;
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
public enum LevelScore
{
    ONE_STAR, TWO_STARS, THREE_STARS
}
