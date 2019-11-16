using UnityEngine;
using Unity.Collections;
using System.Collections.Generic;

public class PlayerController : GameComponent
{
    [SerializeField] private List<GameObject> characterPrefabs;
    [SerializeField] private PlayerMotor motor;
    public PlayerMotor Motor { get { return motor; } }
    private GameObject GFX;
    public GameObject GFXModel => GFX;
    private GameObject cutScenePlayerGFX;
    public GameObject CutScenePlayerGFX => cutScenePlayerGFX;
    [Space(20)]

    [SerializeField] private GameCharacter characterSelected;
    public GameCharacter CharacterSelected => characterSelected;
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

    protected override void GameInitialized()
    {
        GameObject gfx = Instantiate(characterPrefabs[(int)characterSelected], Vector3.one , Quaternion.identity);
        cutScenePlayerGFX = Instantiate(characterPrefabs[(int)characterSelected], Vector3.one , Quaternion.identity);
        cutScenePlayerGFX.SetActive(false);
        gfx.transform.SetParent(transform);
        gfx.transform.localPosition = Vector3.zero;
        gfx.transform.localRotation = Quaternion.identity;
        motor.SetAnimator(gfx.GetComponent<Animator>());
        GFX = gfx;
    }

    protected override void CutSceneEnded()
    {
        GFX.SetActive(true);
        motor.enabled = true;
    }

    protected override void CutSceneStarted()
    {
        motor.enabled = false;
        if(GFX != null)
            GFX.SetActive(false);
    }

    void Update()
    {

    }

    public void SelectCharacter(int i)
    {
        characterSelected = (GameCharacter)i;
    }

}

public enum GameCharacter 
{
    Ibram = 1, Bas = 0, Michael = 2, Steven = 3
}
