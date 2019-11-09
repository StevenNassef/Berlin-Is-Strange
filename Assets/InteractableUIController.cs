using UnityEngine;
using UnityEngine.UI;
public class InteractableUIController : MonoBehaviour
{

    [SerializeField] private GameObject namePanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private Shader outlineShader;
    [SerializeField] private Shader defaultShader;
    [Space(10)]

    [Tooltip("Scaling factor with respect to the distance from the camera.")]
    [SerializeField] private float scaleFactor; //Scaling factor with respect to the distance of the camera
    private InteractableParentObjectController objectController;
    private InteractableObjectUI objectUI;
    private Text objectNameText;
    private Canvas objectUICanvas;
    private CanvasGroup objectUICanvasGroup;
    private bool glow; //show the state of the glow

    private void Awake()
    {
        InitUI();
    }
    private void OnEnable()
    {
        if(objectController != null)
            objectController.OnObjectSelected += OnObjectSelected;
    }
    protected void InitUI()
    {
        objectUICanvas = GetComponentInChildren<Canvas>();
        objectUICanvasGroup = GetComponentInChildren<CanvasGroup>();
        objectController = transform.GetComponentInParent<InteractableParentObjectController>();
        objectController.OnObjectSelected += OnObjectSelected;
    }
    void LateUpdate()
    {
        UpdateUI();
    }

    protected void UpdateUI()
    {
        // Rotate the UI to make it always facing the Camera
        transform.rotation = PlayerCameraController.instance.CameraTransform.rotation;

        //Scale the UI relative to the distance from the camera to always look the same;
        objectUICanvas.transform.localScale = Vector3.one * objectController.DeltaDistance * scaleFactor;

        //Changing the alpha according to the distance
        objectUICanvasGroup.alpha = (objectController.MinimumViewDistance - objectController.DeltaDistance) * 0.1f;

        //check if the object is currently interactble

    }

    private void OnObjectSelected(bool enable)
    {

        //Show the options panel
        optionsPanel.SetActive(enable);
        namePanel.SetActive(!enable);
        MakeObjectGlow(enable);

    }
    private void MakeObjectGlow(bool enable)
    {
        if (glow != enable)
        {
            Material[] mats = objectController.ObjectGFX.GetComponent<MeshRenderer>().materials;
            foreach (Material mat in mats)
            {
                if (enable)
                    mat.shader = outlineShader;
                else
                    mat.shader = defaultShader;
            }
            glow = enable;
        }
    }

    private void OnDisable()
    {
        objectController.OnObjectSelected -= OnObjectSelected;
    }
}