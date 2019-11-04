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
    public static event GameControlsWithState OnLevelUp;
    public static event GameControls OnGameLoaded;
    public static event GameControls OnGameInitialized;

    [SerializeField] private bool _gameEnded;
    public bool isGameEnded { get { return _gameEnded; } }
    [SerializeField] private bool _gamePaused;
    public bool isGamePaused { get { return _gamePaused; } }
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
    }

    public void StartLevel()
    {
        IntializeGame();
        StartGame();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            PauseGame();
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            ResumeGame();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            StartLevel();
        }
    }

    // Update is called once per frame

    public void loadGame()
    {
        _gamePaused = true;
        OnGameLoaded.Invoke();
    }
    public void IntializeGame()
    {
        if (OnGameInitialized != null)
            OnGameInitialized.Invoke();
    }
    public void StartGame()
    {
        _gameEnded = false;
        _gamePaused = false;
        if (OnGameStarted != null)
        {

            if (OnGameStarted != null)
                OnGameStarted.Invoke();
            // GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "game");
        }
    }
    public void PauseGame()
    {
        if (_gamePaused == false && OnGamePaused != null)
        {
            _gamePaused = true;
            OnGamePaused.Invoke();
        }
    }
    public void ResumeGame()
    {
        if (_gamePaused == true && OnGameResumed != null)
        {
            _gamePaused = false;
            OnGameResumed.Invoke();
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
            Debug.Log("GameOver");
            // GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "game", LevelManager.instance.getCurrentPlayerStats().kills);
        }
    }
    public void setVibrationMode(bool state)
    {
        vibration = !state;
    }
}
