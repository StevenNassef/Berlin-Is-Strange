using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController characterController;
    private Transform cameraTarget;
    [Space(10)]
    [SerializeField] private float movementSpeed;
    [Space(10)]
    [SerializeField] private float rotationSpeed;

    [Tooltip("This is the angle at which the rotation factor is applied, to smooth the rotation")]
    [SerializeField] private float rotationAngleThreeshold;
    [SerializeField] private Animator playerAnimator;
    public Animator PlayerAnimator => playerAnimator;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTarget = PlayerCameraController.instance.CameraTargetTransform;
    }

    void Update()
    {
        MovePlayer();
    }
    public void RotateWithAnotherTransform(Transform referenceTransform)
    {
        var newRotation = new Vector3(transform.eulerAngles.x, referenceTransform.eulerAngles.y, transform.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(newRotation), rotationSpeed * Time.fixedDeltaTime);
        // targetRotation = transform.rotation;
    }

    public void SetAnimator(Animator animator)
    {
        this.playerAnimator = animator;
    }

    private void MovePlayer()
    {
        Vector3 targetDirection = cameraTarget.forward * Input.GetAxis(InputController.instance.VerticalAxis);
        
        targetDirection += cameraTarget.right * Input.GetAxis(InputController.instance.HorizontalAxis);
        targetDirection = Vector3.ProjectOnPlane(targetDirection, Vector3.up);
        targetDirection.Normalize();

        //to ignore any movement on the y Axis
        Vector3 moveDirection = targetDirection;
        if (targetDirection.sqrMagnitude > 0)
        {
            //Make the player's forward direction look as the camera
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            targetRotation.x = transform.eulerAngles.x;
            targetRotation.z = transform.eulerAngles.z;

            //To make the Camera rotate smoother in small angles
            // float rotationSpeedFactor = Mathf.Abs(targetRotation.eulerAngles.y - transform.eulerAngles.y) > rotationAngleThreeshold ? 1f : 0.01f;

            // moveDirection = Vector3.Lerp(transform.forward, targetDirection, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;

            moveDirection.y = 0;
            // transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            moveDirection.Normalize();  
        }

        // if(moveDirection.sqrMagnitude > 0)
        // {
        //     transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        // }

        characterController.Move(targetDirection * movementSpeed * Time.deltaTime);

        Vector3 localMoveDirection = Vector3.forward * Input.GetAxis(InputController.instance.VerticalAxis);
        localMoveDirection += Vector3.right * Input.GetAxis(InputController.instance.HorizontalAxis);

        localMoveDirection.Normalize();
        playerAnimator.SetFloat("Speed", localMoveDirection.magnitude);

    }

    // private float ScaleValue(float fromIn, float toIn, float fromOut, float toOut, float value)
    // {
    //     float output = 0;
    //     if(value >= toIn)
    //         output = toOut;
    //     else if(value <= fromIn)
    //         output = fromOut;
    //     else {
    //         output = () * ( (Math) / ())
            
    //     }
    //     return output;
    // }

}
