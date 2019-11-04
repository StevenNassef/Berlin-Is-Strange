using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCanvas : GameCanvas
{
    // Update is called once per frame
    public static LevelCanvas instance;
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

    protected override void InitializePanel()
    {

    }
    protected override void GameInitialized()
    {
        OpenPanel(panels[(int)GamePanels.LEVEL_UI]);
    }
    protected override void GamePaused()
    {
        OpenPanel(panels[(int)GamePanels.PAUSE_PANEL]);
    }

    protected override void GameResumed()
    {
        OpenPanel(panels[(int)GamePanels.LEVEL_UI]);
    }

    protected override void GameEnded(GameState state)
    {
        OpenPanel(panels[(int)GamePanels.GAMEOVER_PANEL]);
    }

    protected override void GameLoaded()
    {
        OpenPanel(panels[(int)GamePanels.START_MENU]);
    }

    public void OpenPanelWithButtonAlone(int panel)
    {
        OpenPanel(panels[panel]);
    }

}

public enum GamePanels
{
    START_MENU, PACK_SELECTION, LEVEL_SELECTION, LEVEL_UI, PAUSE_PANEL, GAMEOVER_PANEL
}