using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{

    [SerializeField] private float minimumInteractionDistance;
    [SerializeField] private LayerMask interactableLayerNumber;

    private InteractableParentObjectController currentInteractableObject;
    private static InteractionController _instance;

    public static InteractionController instance { get { return _instance; } }
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

    }

    void Update()
    {
        CheckObject();
    }

    private void CheckObject()
    {

        RaycastHit hit;
        Debug.DrawRay(PlayerCameraController.instance.CameraTransform.position,
        PlayerCameraController.instance.CameraTransform.forward * minimumInteractionDistance, Color.red, 0.1f, true);
        if (Physics.Raycast(PlayerCameraController.instance.CameraTransform.position,
        PlayerCameraController.instance.CameraTransform.forward,
        out hit,
        minimumInteractionDistance,
         interactableLayerNumber))
        {

            if (currentInteractableObject)
            {
                if (currentInteractableObject != hit.collider.gameObject.GetComponentInParent<InteractableParentObjectController>())
                {
                    currentInteractableObject.SetObjectSelected(false);
                    currentInteractableObject = hit.collider.gameObject.GetComponentInParent<InteractableParentObjectController>();
                    currentInteractableObject = currentInteractableObject.SetObjectSelected(true) ? currentInteractableObject : null;

                }
            }
            else
            {
                currentInteractableObject = hit.collider.gameObject.GetComponentInParent<InteractableParentObjectController>();
                currentInteractableObject = currentInteractableObject.SetObjectSelected(true) ? currentInteractableObject : null;
            }

        }
        else
        {
            if (currentInteractableObject)
            {
                currentInteractableObject.SetObjectSelected(false);
                currentInteractableObject = null;
            }
        }

    }
}
