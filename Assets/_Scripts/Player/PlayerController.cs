using UnityEngine;

public class PlayerController : GameComponent
{
    [SerializeField] private PlayerMotor motor;
    public PlayerMotor Motor { get { return motor; } }
    [SerializeField] private GameObject GFX;
    private PlayerCameraController cameraController;

    private static PlayerController _instance;
    public static PlayerController instance { get { return _instance; } }
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
    void Start()
    {
        cameraController = PlayerCameraController.instance;
    }

    protected override void CutSceneEnded()
    {
        // transform.rotation = Quaternion.identity;
        // GFX.localRotation = Quaternion.identity;
        // transform.position = GFX.position;
        GFX.SetActive(true);
        motor.enabled = true;
    }

    protected override void CutSceneStarted()
    {
        motor.enabled = false;
        GFX.SetActive(false);
    }

    void Update()
    {

    }

}
