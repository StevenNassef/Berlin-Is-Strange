using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
public class DialogReceiver : MonoBehaviour, INotificationReceiver
{
    [SerializeField] private DialogHandler handler;
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is DialogMarker dialogMarker && handler != null)
        {
            Debug.Log("Dialog Received");
            handler.ShowDialog(dialogMarker.Speed, dialogMarker.TimeToClear,
            dialogMarker.CharacterName, dialogMarker.DialogText);
        }
    }

}
