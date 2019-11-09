using UnityEngine;
using UnityEngine.UI;
public class InteractableParentObjectController : MonoBehaviour
{

    [SerializeField] private GameObject objectGFX;
    [Space(10)]
    [Tooltip("The name of this object.")]
    [SerializeField] private string objectName = "Object";
    public string ObjectName { get { return objectName; } }
    [Tooltip("The name of the action of that can be applied on this object.")]
    [SerializeField] private string interactionName = "Interact";
    public string InteractionName { get { return interactionName; } }
    [Space(10)]
    [Tooltip("if this object is interactble by the player or not.")]
    [SerializeField] protected bool interactable;
    [Tooltip("Minimum Distance at which the Object's UI can be seen.")]
    [SerializeField] private float minimumViewDistance;

    protected InteractableUIController UIController;

    protected bool selected;
    private float deltaDistance;
    public float DeltaDistance { get { return deltaDistance; } }
    public float MinimumViewDistance { get { return minimumViewDistance; } }
    public GameObject ObjectGFX { get { return objectGFX; } }

    public delegate void InteractableControls(bool enable);

    public event InteractableControls OnObjectSelected;

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
        if (interactable)
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
    }

    public bool SetObjectSelected(bool enable)
    {
        if (interactable)
        {
            if (selected != enable && this.OnObjectSelected != null)
            {
                selected = enable;
                this.OnObjectSelected.Invoke(enable);
            }
            return true;
        }
        return false;
    }
    public void SetInteractable(bool enable)
    {
        if (enable != interactable)
        {
            if (!enable)
            {
                SetObjectSelected(false);
            }
        }
        interactable = enable;
    }

    public bool isInteractable()
    {
        return interactable;
    }
    public bool isSelected()
    {
        return interactable;
    }

    public virtual bool Interact()
    {
        if (selected)
        {
            Debug.Log("INTERACTION DONE!");
            return true;
        }
        return false;
    }
}
