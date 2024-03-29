﻿using UnityEngine;
using Cinemachine;
public class PlayerCameraController : GameComponent
{
    [SerializeField] private Transform cameraTransform;
    public Transform CameraTransform { get { return cameraTransform; } }

    [SerializeField] private Camera playerCamera;
    public Camera PlayerCamera { get { return playerCamera; } }
    [SerializeField] private Transform cameraTargetTransform;
    public Transform CameraTargetTransform { get { return cameraTargetTransform; } }

    [Space(10)]
    [SerializeField] private float xAxisSens;
    [SerializeField] private float yAxisSens;

    [Space(10)]
    [SerializeField] private float cameraSmoothingFactor;
    [SerializeField] private Vector3 targetOffset;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Vector3 cameraControllerOffset;
    private float xAngle;
    private float yAngle;
    private Transform playerTransform;

    private static PlayerCameraController _instance;

    public static PlayerCameraController instance { get { return _instance; } }
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
        playerTransform = PlayerController.instance.transform;
        InitializeCamera();
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isCutSceneRolling)
            CameraMovement();
    }

    protected override void GameLoaded()
    {
        cameraTransform.gameObject.SetActive(false);
    }

    protected  override void GameInitialized()
    {
        cameraTransform.gameObject.SetActive(true);
        InitializeCamera();
    }

    private void InitializeCamera()
    {

        // cameraControllerOffset = transform.position - playerTransform.position;
        // targetOffset = transform.position - cameraTargetTransform.position;
        // Cursor.lockState = CursorLockMode.Locked;
        cameraTargetTransform.localPosition = targetOffset;
        cameraTransform.localPosition = cameraOffset;
        transform.position = playerTransform.position + cameraControllerOffset;
    }

    private void CameraMovement()
    {
        float XangleDelta = Input.GetAxisRaw(InputController.instance.Yaxis) * Time.deltaTime * yAxisSens;
        float YangleDelta = Input.GetAxisRaw(InputController.instance.Xaxis) * Time.deltaTime * xAxisSens;

        xAngle -= XangleDelta;
        yAngle += YangleDelta;
        xAngle = Mathf.Clamp(xAngle, -60, 60);

        // cameraTransform.Rotate(Vector3.up, YangleDelta);
        CheckCameraClipping();
        cameraTransform.LookAt(cameraTargetTransform);
        // cameraTransform.rotation = Quaternion.LookRotation(Vector3.Lerp((cameraTargetTransform.position - cameraTransform.position).normalized, cameraTransform.forward, Time.deltaTime));
        transform.rotation = Quaternion.Euler(xAngle, yAngle, 0);


        //Updating Position
        // transform.position = Vector3.Lerp(playerTransform.position + cameraControllerOffset, playerTransform.position, Time.deltaTime);
        transform.position = playerTransform.position + cameraControllerOffset;
    }

    private void CheckCameraClipping()
    {
        int counter = 300;
        cameraTransform.localPosition = cameraOffset;
        RaycastHit hit;
        Vector3 targetDirection = cameraTargetTransform.position - cameraTransform.position;

        do
        {
            Debug.DrawRay(cameraTransform.position, targetDirection, Color.green, 0.1f);
            if (Physics.Raycast(cameraTransform.position, targetDirection, out hit, 10f))
            {
                if (hit.collider.gameObject.CompareTag("CameraTargetTrigger"))
                {
                    break;
                // Debug.Log("hit trigger");
                }
                else
                {
                    // Debug.Log("FindingTarget");
                    Vector3 newPos = cameraTransform.localPosition;
                    newPos.z += 0.05f;
                    cameraTransform.localPosition = newPos;
                }

                
            }

            // Debug.Log(hit.collider.gameObject.tag);
            counter--;
        } while (counter > 0);

    }
}
