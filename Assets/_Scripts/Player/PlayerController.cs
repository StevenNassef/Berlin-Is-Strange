using UnityEngine;

public class PlayerController : GameComponent
{
    private PlayerMotor motor;
    private PlayerCameraController cameraController;
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        cameraController = GetComponent<PlayerCameraController>();
    }

    void Update()
    {

    }

}
