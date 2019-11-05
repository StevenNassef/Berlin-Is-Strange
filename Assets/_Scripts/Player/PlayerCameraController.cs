using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraHolder;
    [Space(10)]
    [SerializeField] private float xAxisSens;
    [SerializeField] private float yAxisSens;
    private float xAngle;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CameraMovement();
    }
    
    private void CameraMovement()
    {   
        float XangleDelta = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * yAxisSens;
        float YangleDelta = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xAxisSens;

        xAngle -= XangleDelta;

        xAngle = Mathf.Clamp(xAngle, -90,90);

        transform.Rotate(Vector3.up, YangleDelta);

        cameraHolder.localRotation = Quaternion.Euler(xAngle,0,0);   
                
    }
}
