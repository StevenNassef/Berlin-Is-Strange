using UnityEngine;
using UnityEngine.Events;
public class InteractableParentObjectController : GameComponent
{

    [SerializeField] private GameObject objectGFX;
    [SerializeField] private GameObject interactionUI;
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

    public UnityEvent OnInteraction;

    protected InteractableUIController UIController;

    protected bool selected;
    private float deltaDistance;
    public float DeltaDistance { get { return deltaDistance; } }
    public float MinimumViewDistance { get { return minimumViewDistance; } }
    public GameObject ObjectGFX { get { return objectGFX; } }

    public delegate void InteractableControls(bool enable);

    public event InteractableControls OnObjectSelected;



    private void Awake()
    {
        InitObject();
    }
    protected override void UpdateComponent()
    {
        UpdateObject();
    }
    protected void InitObject()
    {
        InitUI();
    }

    private void InitUI()
    {
        UIController = interactionUI.transform.GetComponent<InteractableUIController>();
        Debug.Log(UIController);
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

    protected override void OnComponentDisabled()
    {
        InteractionController.OnDisableInteractions -= OnInteractionDisabled;
        InteractionController.OnEnableInteractions -= OnInteractionEnabled;
    }
    protected override void OnComponentEnabled()
    {
        InteractionController.OnDisableInteractions += OnInteractionDisabled;
        InteractionController.OnEnableInteractions += OnInteractionEnabled;
    }
    private void OnInteractionDisabled()
    {
        SetInteractable(false);
    }

    private void OnInteractionEnabled()
    {
        SetInteractable(true);
    }
    public void SetInteractable(bool enable)
    {
        if (enable != interactable)
        {
            if (!enable)
            {
                SetObjectSelected(false);
            }
            interactionUI.SetActive(enabled);
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

    public bool Interact()
    {
        if (OnInteraction != null)
        {
            OnInteraction.Invoke();
            return true;
        }
        return false;

    }
}
