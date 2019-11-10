using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
[CreateAssetMenu(menuName = "GameDecision")]
public class GameDecision : ScriptableObject
{
    [SerializeField] private string decisionQuestion;
    public string DecisionQuestion { get { return decisionQuestion; } }
    [SerializeField] private string firstDecision;
    public string FirstDecision { get { return firstDecision; } }
    [SerializeField] private string secondDecision;
    public string SecondDecision { get { return secondDecision; } }

    [SerializeField] private PlayableAsset firstDecisionCutScene;
    public PlayableAsset FirstDecisionCutScene { get { return firstDecisionCutScene; } }
    [SerializeField] private PlayableAsset secondDecisionCutScene;
    public PlayableAsset SecondDecisionCutScene { get { return secondDecisionCutScene; } }
}
