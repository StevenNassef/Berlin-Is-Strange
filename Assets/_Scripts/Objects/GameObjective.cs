using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameObjective
{
    [Header("Objective Info")]

    [Tooltip("The objective text to infrom the player exactly what they need to do.")]
    [SerializeField] private string objectiveText;
    [Tooltip("The gameobject that is required to complete this objective.")]
    [SerializeField] private Transform objectiveObject;

    [Header("Interactable Settings")]
    [SerializeField] private InteractableParentObjectController objectiveInteractable;
    [Tooltip("enable this if you want to change the settings of the interactable object.")]
    [SerializeField] private bool overrideInteractableSettings;
    [Tooltip("Only availiable if OverrideInteractableSettings is set to True.")]
    [SerializeField] private string newInteractableName;
    [Tooltip("Only availiable if OverrideInteractableSettings is set to True.")]
    [SerializeField] private string newInteractionText;
    public string ObjectiveText => objectiveText;
    public Transform ObjectiveObject => objectiveObject;
    public InteractableParentObjectController Interactable => objectiveInteractable;
    public bool OverrideInteractableSettings => overrideInteractableSettings;
    public string NewInteractableName => newInteractableName;
    public string NewInteractionText => newInteractionText;
}
