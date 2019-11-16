using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class MainMenuController : MonoBehaviour
{
    [SerializeField] private PlayableAsset charSelctionMenuIn;
    [SerializeField] private PlayableAsset charSelctionMenuOut;
    [SerializeField] private PlayableAsset charSelectedIn;
    [SerializeField] private PlayableAsset charSelectedOut;

    [Space(5)]
    [SerializeField] private Camera mainCamera;
    [Space(5)]
    [SerializeField] private PlayableDirector director;
    [Space(5)]
    [Header("Virtual Cameras")]
    [SerializeField] private Transform startMenuCameraTransform;
    [SerializeField] private Transform charSelectCameraTransform;
    [SerializeField] private Transform mainMenuMainCamTransform;
    [Space(10)]
    [SerializeField] private List<Light> mainMenuLights;
    [SerializeField] private List<Transform> cameraPositions;

    private int charSelected = -1;
    private bool selected;
    private Vector3 initialSecondaryCameraPos;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        initialSecondaryCameraPos = charSelectCameraTransform.position;
    }

    void Update()
    {
        CheckForSelection();

        if (Input.GetMouseButtonDown(0))
        {
            SelectChar();
        }
    }

    private void OnEnable()
    {
        charSelectCameraTransform.position = initialSecondaryCameraPos;
        director.playableAsset = mainMenuIdle;
        director.Play();
    }

    public void SelectChar()
    {
        if (charSelected > -1)
        {
            charSelectCameraTransform.position = cameraPositions[charSelected].position;
            director.playableAsset = charSelection;
            selected = true;
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
                    if (charSelected != -1)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (i != charSelected)
                            {
                                mainMenuLights[i].color = Color.white;
                            }
                        }
                    }
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

    public void CancelSelection()
    {
        selected = false;
        charSelected = -1;
        charSelectCameraTransform.position = initialSecondaryCameraPos;
        director.playableAsset = mainMenuIdle;
        for (int i = 0; i < 4; i++)
        {
            mainMenuLights[i].color = Color.white;
        }
        director.Play();
    }

    public void StartGame()
    {
        if (charSelected != -1)
        {
            PlayerController.instance.SelectCharacter(charSelected);
        }
    }
}
