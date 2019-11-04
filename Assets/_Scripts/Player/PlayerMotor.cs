using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    
    [Space(10)]
    [SerializeField] private float movementSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 moveDirection = transform.forward * Input.GetAxisRaw("Vertical");
        moveDirection += transform.right * Input.GetAxisRaw("Horizontal");
        moveDirection.Normalize();
        characterController.Move(moveDirection * movementSpeed * Time.deltaTime);
     
    }

}   
