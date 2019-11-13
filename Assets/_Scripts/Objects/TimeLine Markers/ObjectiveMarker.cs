using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class ObjectiveMarker : Marker, INotification, INotificationOptionProvider
{   
    [Header("Objective Settings")]
    [Tooltip("Enable or Disable this objective.")]
    [SerializeField] private bool enableObjective;
    [Tooltip("The index of the selected objective in the Objectives List on Objective Canvas")]
    [SerializeField] private int objectiveIndex;

    
    [Header("Interactable Settings")]
    [SerializeField] private InteractableParentObjectController objectiveInteractable;
    [Tooltip("enabling or disabling the selected InteractableObject.")]
    [SerializeField] private bool interactable;
    [Space(20)]
    [SerializeField] private bool retroaction = false;
    [SerializeField] private bool emitOnce = true;


    public PropertyName id => new PropertyName();

    public bool EnableObjective => enableObjective;
    public int ObjectiveIndex => objectiveIndex;
    public InteractableParentObjectController Interactable => objectiveInteractable;
    public bool isInteractable => interactable;

    public NotificationFlags flags =>
        (retroaction ? NotificationFlags.Retroactive : default) |
        (emitOnce ? NotificationFlags.TriggerOnce : default);
}
