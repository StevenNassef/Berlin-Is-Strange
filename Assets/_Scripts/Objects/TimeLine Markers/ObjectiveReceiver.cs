using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
public class ObjectiveReceiver : MonoBehaviour, INotificationReceiver
{
    [SerializeField] private ObjectiveHandler handler;
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is ObjectiveMarker objectiveMarker && handler != null)
        {
            Debug.Log("Objective Received");

            handler.UpdateObjective(objectiveMarker.ObjectiveIndex, objectiveMarker.EnableObjective);

            if (objectiveMarker.Interactable != null)
            {
                handler.UpdateInteractable(objectiveMarker.Interactable, objectiveMarker.isInteractable);
            }
        }
    }

}
