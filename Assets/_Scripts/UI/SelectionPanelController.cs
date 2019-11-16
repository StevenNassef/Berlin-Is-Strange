using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectionPanelController : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject lockedButton;
    private void OnEnable()
    {
        if (DirectorController.instance.StartScenesList[(int)PlayerController.instance.CharacterSelected] != null)
        {
            lockedButton.SetActive(false);
            startButton.SetActive(true);
        }
        else
        {
            lockedButton.SetActive(true);
            startButton.SetActive(false);
        }
    }
}
