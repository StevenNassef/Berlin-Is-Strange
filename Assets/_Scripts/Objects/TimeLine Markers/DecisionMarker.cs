using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
public class DecisionMarker : Marker , INotification, INotificationOptionProvider
{
    [SerializeField] private GameDecision decision;
    [SerializeField] private float loopTime;
    [Space(20)]
    [SerializeField] private bool retroaction = false;
    [SerializeField] private bool emitOnce = false;
    
    public GameDecision Decision => decision;
    public float LoopTime => loopTime;
    public PropertyName id => new PropertyName();
    public NotificationFlags flags => 
        (retroaction ? NotificationFlags.Retroactive : default) |
        (emitOnce ? NotificationFlags.TriggerOnce : default);
}
