using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform cameraTarget;
    [Space(10)]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Animator playerAnimator;
    void Start()
    {
        
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
    private void MovePlayer()
    {
        
        Vector3 moveDirection = cameraTarget.forward * Input.GetAxis("Vertical");
        moveDirection += cameraTarget.right * Input.GetAxis("Horizontal");

        moveDirection.y = 0;
        if(moveDirection.sqrMagnitude > 0)
        {
            RotateWithAnotherTransform(cameraTarget);
        }
        moveDirection.Normalize();
        characterController.Move(moveDirection * movementSpeed * Time.deltaTime);
        
        Vector3 localMoveDirection = Vector3.forward * Input.GetAxis("Vertical");
        localMoveDirection += Vector3.right  * Input.GetAxis("Horizontal");
        
        localMoveDirection.Normalize();
        playerAnimator.SetFloat("Speed", localMoveDirection.magnitude);

    }

}   
