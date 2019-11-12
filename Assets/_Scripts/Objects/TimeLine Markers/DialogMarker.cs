using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
public class DialogMarker : Marker, INotification, INotificationOptionProvider
{
    [Space(10)]
    [Tooltip("The name of the Character that is currently speaking.")]
    [SerializeField] private string characterName;
    [Tooltip("The message to be printed.")]
    [SerializeField] private string dialogText;
    [Space(10)]
    [Tooltip("Speed at which characters will be typed on the screen, Character Per Second")]
    [SerializeField] private float speed = 0.05f;
    [Tooltip("Time to show the text before clearing it")]
    [SerializeField] private float timeToClear = 2;
    [Space(20)]
    [SerializeField] private bool retroaction = false;
    [SerializeField] private bool emitOnce = true;

    public string DialogText => dialogText;
    public string CharacterName => characterName;
    public float Speed => speed;
    public float TimeToClear => timeToClear;
    public PropertyName id => new PropertyName();
    public NotificationFlags flags =>
        (retroaction ? NotificationFlags.Retroactive : default) |
        (emitOnce ? NotificationFlags.TriggerOnce : default);
}
