using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
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
        float XangleDelta = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * yAxisSens;
        float YangleDelta = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xAxisSens;

        xAngle -= XangleDelta;
        yAngle += YangleDelta;
        xAngle = Mathf.Clamp(xAngle, -60,60);
        
        // cameraTransform.Rotate(Vector3.up, YangleDelta);
        cameraTransform.LookAt(cameraTargetTransform);
        transform.rotation = Quaternion.Euler(xAngle, yAngle,0);   
        transform.RotateAround(playerTransform.position, Vector3.up, YangleDelta);

        //Updating Position
        transform.position = Vector3.Lerp(playerTransform.position + cameraControllerOffset, playerTransform.position, cameraSmoothingFactor * 0.00001f * Time.deltaTime);
    }
}
