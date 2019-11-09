using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    public Transform CameraTransform { get { return cameraTransform; } }

    [SerializeField] private Camera playerCamera;
    public Camera PlayerCamera { get { return playerCamera; } }
    [SerializeField] private Transform cameraTargetTransform;

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
        Cursor.lockState = CursorLockMode.Locked;
        InitializeCamera();
    }

    void LateUpdate()
    {
        CameraMovement();
    }

    private void InitializeCamera()
    {
        playerTransform = PlayerController.instance.transform;
        cameraControllerOffset = transform.position - playerTransform.position;
        targetOffset = transform.position - cameraTargetTransform.position;
        cameraTransform.position = cameraTargetTransform.position + cameraOffset;
    }
    
    private void CameraMovement()
    {   
        float XangleDelta = Input.GetAxisRaw(InputController.instance.Yaxis) * Time.deltaTime * yAxisSens;
        float YangleDelta = Input.GetAxisRaw(InputController.instance.Xaxis) * Time.deltaTime * xAxisSens;

        xAngle -= XangleDelta;
        yAngle += YangleDelta;
        xAngle = Mathf.Clamp(xAngle, -60,60);
        
        // cameraTransform.Rotate(Vector3.up, YangleDelta);
        cameraTransform.LookAt(cameraTargetTransform);
        // cameraTransform.rotation = Quaternion.LookRotation(Vector3.Lerp((cameraTargetTransform.position - cameraTransform.position).normalized, cameraTransform.forward, Time.deltaTime));
        transform.rotation = Quaternion.Euler(xAngle, yAngle,0);   
        

        //Updating Position
        // transform.position = Vector3.Lerp(playerTransform.position + cameraControllerOffset, playerTransform.position, Time.deltaTime);
        transform.position = playerTransform.position + cameraControllerOffset;
    }
}
