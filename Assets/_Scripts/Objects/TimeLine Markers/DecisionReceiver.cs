using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
public class DecisionReceiver : MonoBehaviour, INotificationReceiver
{
    [SerializeField] private DecisionHandler handler;
    private float timer;
    private Playable lastOrigin;
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is DecisionMarker decisionMarker && handler != null)
        {
            Debug.Log("Decision Recieved");
            // origin.SetSpeed(0);
            var timelinePlayable = origin.GetGraph().GetRootPlayable(0);
            // timelinePlayable.Pause();
            lastOrigin = origin;
            if(decisionMarker.LoopTime > 0)
                StartCoroutine(LoopPart(decisionMarker.LoopTime, origin));
            handler.ShowDecision(decisionMarker.Decision);
        }
    }

    IEnumerator LoopPart(float timer, Playable currentOrigin)
    {
        float speedFactor = 0.5f;
        if (currentOrigin.IsValid())
        {
            var timelinePlayable = currentOrigin.GetGraph().GetRootPlayable(0);
            timelinePlayable.SetTime(timelinePlayable.GetTime() - timer * speedFactor);
            timelinePlayable.SetSpeed(speedFactor);
            Debug.Log("looptimer");
            yield return new WaitForSeconds(timer);
            StartCoroutine(LoopPart(timer, currentOrigin));
        }

    }
}
