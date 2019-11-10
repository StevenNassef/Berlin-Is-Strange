using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameComponent : MonoBehaviour
{

    // Use this for initialization

    // protected bool isPaused = true;

    // Update is called once per frame

    // void Awake()
    // {
    //     OnEnable();
    // }

    private void Update()
    {
        if (!GameManager.instance.isGamePaused)
        {
            UpdateComponent();
        }
    }
    protected virtual void UpdateComponent() { }
    protected virtual void OnComponentDestroyed() { }
    protected virtual void OnComponentDisabled() { }
    protected virtual void OnComponentEnabled() { }
    void OnEnable()
    {
        GameManager.OnGameStarted += startGame;
        GameManager.OnGamePaused += pauseGame;
        GameManager.OnGameResumed += resumeGame;
        GameManager.OnGameInitialized += initializeGame;
        GameManager.OnGameLoaded += loadGame;
        GameManager.OnGameOver += gameOver;
        GameManager.OnCutSceneEnded += endCutScene;
        GameManager.OnCutSceneStarted += startCutScene;
        OnComponentEnabled();
    }
    void OnDisable()
    {
        GameManager.OnGameStarted -= startGame;
        GameManager.OnGamePaused -= pauseGame;
        GameManager.OnGameResumed -= resumeGame;
        GameManager.OnGameInitialized -= initializeGame;
        GameManager.OnGameLoaded -= loadGame;
        GameManager.OnGameOver -= gameOver;
        GameManager.OnCutSceneEnded -= endCutScene;
        GameManager.OnCutSceneStarted -= startCutScene;
        OnComponentDisabled();
    }

    private void OnDestroy()
    {
        GameManager.OnGameStarted -= startGame;
        GameManager.OnGamePaused -= pauseGame;
        GameManager.OnGameResumed -= resumeGame;
        GameManager.OnGameInitialized -= initializeGame;
        GameManager.OnGameLoaded -= loadGame;
        GameManager.OnGameOver -= gameOver;
        GameManager.OnCutSceneEnded -= endCutScene;
        GameManager.OnCutSceneStarted -= startCutScene;
        OnComponentDestroyed();
    }

    private void loadGame()
    {
        GameLoaded();
    }
    private void initializeGame()
    {
        GameInitialized();
    }
    private void startGame()
    {
        GameStarted();
    }
    private void pauseGame()
    {
        GamePaused();
    }
    private void resumeGame()
    {
        GameResumed();
    }
    private void startCutScene()
    {
        CutSceneStarted();
    }
    private void endCutScene()
    {
        CutSceneEnded();
    }
    private void gameOver(GameState state)
    {
        GameEnded(state);
    }
    protected virtual void GameLoaded() { }
    protected virtual void GameInitialized() { }
    protected virtual void GameStarted() { }
    protected virtual void GamePaused() { }
    protected virtual void GameResumed() { }
    protected virtual void CutSceneEnded() { }
    protected virtual void CutSceneStarted() { }
    protected virtual void GameEnded(GameState state) { }

}
