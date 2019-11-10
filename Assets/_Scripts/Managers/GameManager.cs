using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using GameAnalyticsSDK;
public class GameManager : MonoBehaviour
{

    /*  Android Version ____
        Bundle Version Code 1
        iOS Version 0.
        Build 1
    */
    public static GameManager instance;
    public delegate void GameControls();
    public delegate void GameControlsWithState(GameState state);
    public static event GameControls OnGameStarted;
    public static event GameControls OnGamePaused;
    public static event GameControls OnGameResumed;
    public static event GameControlsWithState OnGameOver;
    public static event GameControls OnGameLoaded;
    public static event GameControls OnCutSceneStarted;
    public static event GameControls OnCutSceneEnded;
    public static event GameControls OnGameInitialized;

    [SerializeField] private bool _gameEnded;
    public bool isGameEnded { get { return _gameEnded; } }
    [SerializeField] private bool _gamePaused;
    public bool isGamePaused { get { return _gamePaused; } }
    [SerializeField] private bool _cutSceneRolling;
    public bool isCutSceneRolling { get { return _cutSceneRolling; } }
    [SerializeField] private GameState _gameState;
    public GameState gameState { get { return _gameState; } }

    public bool vibration;
    public Camera MainCamera;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        // GameAnalytics.Initialize(); 
    }

    void Start()
    {
        loadGame();
        StartLevel();
    }

    public void StartLevel()
    {
        IntializeGame();
        StartGame();
    }

    private void Update()
    {
    }

    // Update is called once per frame

    public void loadGame()
    {
        _gamePaused = true;
        Time.timeScale = 0;
        if (OnGameLoaded != null)
        {
            OnGameLoaded.Invoke();
            Debug.Log("Game Loaded");
        }
    }
    public void IntializeGame()
    {
        if (OnGameInitialized != null)
        {
            OnGameInitialized.Invoke();
            Debug.Log("Game Initalized");
        }
    }
    public void StartGame()
    {
        _gameEnded = false;
        _gamePaused = false;
        if (OnGameStarted != null)
        {
            OnGameStarted.Invoke();
            Time.timeScale = 1;
            Debug.Log("Game Started");
            // GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "game");
        }
    }

    public void StartScene()
    {
        Debug.Log("StartScene");
        PlayerCameraController.instance.gameObject.SetActive(true);
        PlayerController.instance.gameObject.SetActive(true);
    }
    public void PauseGame()
    {
        if (_gamePaused == false && OnGamePaused != null)
        {
            _gamePaused = true;
            Time.timeScale = 0;
            OnGamePaused.Invoke();
            Debug.Log("Game Paused");
        }
    }

    public void StartCutScene()
    {
        if (_cutSceneRolling == false && OnCutSceneStarted != null)
        {
            _cutSceneRolling = true;
            OnCutSceneStarted.Invoke();
            Debug.Log("Cut Scene Started");
        }
    }

    public void EndCutScene()
    {
        if (_cutSceneRolling == true && OnCutSceneEnded != null)
        {
            _cutSceneRolling = false;
            OnCutSceneEnded.Invoke();
            Debug.Log("Cut Scene Ended");
        }
    }

    public void ResumeGame()
    {
        if (_gamePaused == true && OnGameResumed != null)
        {
            _gamePaused = false;
            Time.timeScale = 1;
            OnGameResumed.Invoke();
            Debug.Log("Game Resumed");
        }

    }

    public void RestartGame()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        IntializeGame();
        StartGame();
    }

    public void GameOver(GameState state)
    {
        // currentPlayerStats = Player.GetComponent<ScoreManager>().playerStats;
        _gameState = state;
        if (!_gameEnded)
        {
            _gameEnded = true;
            OnGameOver.Invoke(state);
            Time.timeScale = 0;
            Debug.Log("GameOver");
            // GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "game", LevelManager.instance.getCurrentPlayerStats().kills);
        }
    }
    public void setVibrationMode(bool state)
    {
        vibration = !state;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
