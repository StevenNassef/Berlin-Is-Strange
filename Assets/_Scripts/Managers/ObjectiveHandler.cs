using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ObjectiveHandler : GameComponent
{
    [SerializeField] private GameObject objectivePanel;
    [SerializeField] private TextMeshProUGUI objectiveTextUI;
    [SerializeField] private GameObject objectiveArrow;
    [Space(20)]

    [SerializeField] private List<GameObjective> objectivesList;
    private Transform currentObjective;
    private InteractableParentObjectController currentObjectiveInteractable;

    private bool tracking;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (tracking && currentObjective != null)
        {
            objectiveArrow.SetActive(true);
            objectiveArrow.transform.LookAt(currentObjective.transform.position);
        }
        else
        {
            objectiveArrow.SetActive(false);
        }

    }

    public void UpdateObjective(int objectiveIndex, bool enableObjective)
    {
        if (objectiveIndex < objectivesList.Count)
        {
            if (enableObjective)
            {
                objectiveTextUI.SetText(objectivesList[objectiveIndex].ObjectiveText);
                currentObjective = objectivesList[objectiveIndex].ObjectiveObject;
            }
            else
            {
                objectivePanel.SetActive(false);
                tracking = false;
                currentObjective = null;
            }
        }
    }

    public void UpdateInteractable(InteractableParentObjectController interactable, bool isInteractable)
    {
        this.currentObjectiveInteractable = interactable;
        interactable.SetInteractable(isInteractable);
    }
    protected override void CutSceneStarted()
    {
        objectivePanel.SetActive(false);
        tracking = false;
    }

    protected override void CutSceneEnded()
    {
        if (currentObjective != null)
        {
            objectivePanel.SetActive(true);
            tracking = true;
        }
    }
}
