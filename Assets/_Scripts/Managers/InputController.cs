using UnityEngine;

public class InputController : GameComponent
{

    [SerializeField] private string xAxis;
    public string Xaxis { get { return xAxis; } }
    [SerializeField] private string yAxis;
    public string Yaxis { get { return yAxis; } }
    [SerializeField] private string verticalAxis;
    public string VerticalAxis { get { return verticalAxis; } }
    [SerializeField] private string horizontalAxis;
    public string HorizontalAxis { get { return horizontalAxis; } }
    [SerializeField] private string interactionButton;
    public string InteractionButton { get { return interactionButton; } }

    private static InputController _instance;

    public static InputController instance { get { return _instance; } }
    void Awake()
    {
        //Check if instance already exists
        if (_instance == null)

            //if not, set instance to this
            _instance = this;

        //If instance already exists and it's not this:
        else if (_instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        // GameAnalytics.Initialize(); 
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            GameManager.instance.StartLevel();
            DirectorController.instance.OverrideScene();
        }
        else
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.PauseGame();
            Cursor.lockState = CursorLockMode.None;
        }
    }

}