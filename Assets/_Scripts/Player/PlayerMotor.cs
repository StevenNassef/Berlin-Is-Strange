using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    
    [Space(10)]
    [SerializeField] private float movementSpeed;
    [SerializeField] private Animator playerAnimator;
    void Start()
    {
        
    }

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 moveDirection = transform.forward * Input.GetAxis("Vertical");
        moveDirection += transform.right * Input.GetAxis("Horizontal");

        moveDirection.Normalize();
        characterController.Move(moveDirection * movementSpeed * Time.deltaTime);
        
        Vector3 localMoveDirection = Vector3.forward * Input.GetAxis("Vertical");
        localMoveDirection += Vector3.right  * Input.GetAxis("Horizontal");
        

        playerAnimator.SetFloat("xSpeed", localMoveDirection.x);
        playerAnimator.SetFloat("zSpeed", localMoveDirection.z);
    }

}   
