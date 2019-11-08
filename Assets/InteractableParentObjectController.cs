using UnityEngine;
using UnityEngine.UI;
public class InteractableParentObjectController : MonoBehaviour
{

    [SerializeField] private GameObject objectGFX;
    [Space(10)]
    [Tooltip("Minimum Distance at which the Object's UI can be seen.")]
    [SerializeField] private float minimumViewDistance;

    protected InteractableUIController UIController;

    protected bool interactable;
    private float deltaDistance;
    public float DeltaDistance { get { return deltaDistance; } }
    public float MinimumViewDistance { get { return minimumViewDistance; } }
    public GameObject ObjectGFX { get { return objectGFX; } }

    public delegate void InteractableControls(bool enable);

    public event InteractableControls OnInteractableActivated;

    void Start()
    {
        InitObject();
    }

    void Update()
    {
        UpdateObject();
    }

    protected void InitObject()
    {
        InitUI();
    }

    private void InitUI()
    {
        UIController = GetComponentInChildren<InteractableUIController>();
    }

    private void UpdateObject()
    {
        deltaDistance = (PlayerCameraController.instance.CameraTransform.position - transform.position).magnitude;

        if (deltaDistance > minimumViewDistance)
        {
            UIController.enabled = false;
        }
        else
        {
            UIController.enabled = true;
        }
    }

    public void SetInteractable(bool enable)
    {
        if(interactable != enable)
        {
            Debug.Log("INTERACTBLE");
            interactable = enable;
            this.OnInteractableActivated.Invoke(enable);
        }
    }

    public bool isInteractable()
    {
        return interactable;
    }

    public virtual void Interact()
    {

    }
}
