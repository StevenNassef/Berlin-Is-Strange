using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class MainMenuController : GameComponent
{
    [SerializeField] private PlayableAsset charSelctionMenuIn;
    [SerializeField] private PlayableAsset charSelctionMenuOut;
    [SerializeField] private PlayableAsset charSelectedIn;
    [SerializeField] private PlayableAsset charSelectedOut;

    [Space(5)]
    private Camera mainCamera;
    [SerializeField] private GameObject mainCameraObject;

    [Space(10)]
    [Header("UI")]
    [SerializeField] private GameObject startPanel;
    [Space(10)]
    [Header("Virtual Cameras")]
    [SerializeField] private PlayableDirector director;
    [SerializeField] private Transform charSelectCameraTransform;
    [SerializeField] private Transform mainMenuMainCamTransform;
    [Space(10)]
    [Header("Lights and VFX")]
    [SerializeField] private List<Light> mainMenuLights;
    [Space(5)]
    [Header("Camera Positions")]
    [SerializeField] private Transform startPos;
    [SerializeField] private List<Transform> cameraPositions;

    private int charSelected = -1;
    private bool selected;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        CheckForSelection();

        if (!selected && Input.GetMouseButtonDown(0))
        {
            SelectChar();
        }
    }

    protected override void OnComponentEnabled()
    {
        mainCamera = mainCameraObject.GetComponent<Camera>();
        mainCameraObject.SetActive(true);
        charSelectCameraTransform.position = startPos.position;
        startPanel.SetActive(true);
    }
    protected override void GameInitialized()
    {
        gameObject.SetActive(false);
    }

    public void SelectChar()
    {
        if (charSelected > -1)
        {

            charSelectCameraTransform.position = cameraPositions[charSelected].position;
            director.playableAsset = charSelectedIn;
            selected = true;
            PlayerController.instance.SelectCharacter(charSelected);
            mainMenuLights[charSelected].color = Color.green;
            director.Play();
        }
    }

    private void CheckForSelection()
    {
        if (!selected)
        {
            RaycastHit hit;
            // Debug.DrawRay(mainCamera.ScreenPointToRay(Input.mousePosition), Color.red, 0.01f);
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 30, 9))
            {
                if (hit.collider.CompareTag("CharSelectionTrigger"))
                {
                    GameObject selectedChar = hit.collider.gameObject;

                    if (selectedChar.name.Contains("1"))
                        charSelected = 0;
                    else if (selectedChar.name.Contains("2"))
                        charSelected = 1;
                    else if (selectedChar.name.Contains("3"))
                        charSelected = 2;
                    else if (selectedChar.name.Contains("4"))
                        charSelected = 3;

                    mainMenuLights[charSelected].color = Color.blue;
                }
                else
                {
                    Debug.Log("fail");
                    for (int i = 0; i < 4; i++)
                    {
                        mainMenuLights[i].color = Color.white;
                    }
                    charSelected = -1;
                }
            }
        }
    }

    public void PlayTimeLineOnLoop(PlayableAsset timeLine)
    {
        director.extrapolationMode = DirectorWrapMode.Loop;
        director.playableAsset = timeLine;
        director.Play();
    }

    public void PlayTimeLineOnHold(PlayableAsset timeLine)
    {
        director.extrapolationMode = DirectorWrapMode.Hold;
        director.playableAsset = timeLine;
        director.Play();
    }

    public void BackToMainMenu()
    {
        startPanel.SetActive(true);
        charSelectCameraTransform.position = startPos.position;
        director.playableAsset = charSelctionMenuOut;
        director.Play();

    }
    public void CancelSelection()
    {
        Debug.Log("Cancel");
        selected = false;
        charSelected = -1;
        director.playableAsset = charSelectedOut;
        director.Play();
        for (int i = 0; i < 4; i++)
        {
            mainMenuLights[i].color = Color.white;
        }
    }
    public void StartGame()
    {
        if (charSelected != -1)
        {
            PlayerController.instance.SelectCharacter(charSelected);
            mainCameraObject.SetActive(false);
            GameManager.instance.StartLevel();
        }
    }
}
