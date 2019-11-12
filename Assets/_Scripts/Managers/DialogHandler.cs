using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogHandler : GameComponent
{
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private GameObject dialogPanel;
    private string currentText; //The text the script reached till now.
    private string currentCharacterName;
    private string currentDialogMessage;
    private float currentSpeed;
    private float timeToClear;
    private bool printing;
    private bool finishedPrinting;

    protected override void GamePaused()
    {
        printing = false;
    }
    protected override void GameResumed()
    {
        printing = true;
        StartCoroutine(PrintCharacters());
    }
    protected override void GameEnded(GameState state)
    {
        printing = false;
        currentText = "";
        dialogText.SetText(currentText);
    }

    public void ShowDialog(float speed, float timeToClear, string characterName, string dialogMessage)
    {
        currentText = characterName + ": ";
        currentSpeed = speed;
        currentCharacterName = characterName;
        currentDialogMessage = dialogMessage;
        this.timeToClear = timeToClear;
        printing = true;
        finishedPrinting = false;
        StartCoroutine(PrintCharacters());
    }

    IEnumerator PrintCharacters()
    {
        int currentCharIndex = currentText.Length - (2 + currentCharacterName.Length);
        if (printing)
        {
            if (currentCharIndex < currentDialogMessage.Length)
            {
                currentText += currentDialogMessage[currentCharIndex];
                dialogText.SetText(currentText);
                yield return new WaitForSeconds(currentSpeed);
                StartCoroutine(PrintCharacters());
            }
            else
            {
                finishedPrinting = true;
                StartCoroutine(ClearTextTimer(timeToClear));
            }
        }
    }

    IEnumerator ClearTextTimer(float timerToClear)
    {
        yield return new WaitForSeconds(timeToClear);
        if(finishedPrinting)
        {
            Debug.Log(timeToClear);
            currentText = "";
            dialogText.SetText(currentText);
        }
    }
}
