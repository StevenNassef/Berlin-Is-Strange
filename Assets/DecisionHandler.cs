using UnityEngine;
using TMPro;
public class DecisionHandler : MonoBehaviour
{
    [SerializeField] private GameObject decisionPanel;
    [SerializeField] private TextMeshProUGUI firstDecisionText;
    [SerializeField] private TextMeshProUGUI secondDecisionText;

    private void Start()
    {
        decisionPanel.SetActive(false);
    }
    private GameDecision currentDecision;
    public void ShowDecision(GameDecision decision)
    {
        firstDecisionText.SetText(decision.FirstDecision);
        secondDecisionText.SetText(decision.SecondDecision);
        currentDecision = decision;
        decisionPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ActivateDecisionChoice(int i)
    {
        decisionPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        if (currentDecision != null)
        {
            if (i == 1)
            {
                DirectorController.instance.PlayTrack(currentDecision.FirstDecisionCutScene);
            }
            else if (i == 2)
            {
                DirectorController.instance.PlayTrack(currentDecision.SecondDecisionCutScene);
            }
        }
    }
}
