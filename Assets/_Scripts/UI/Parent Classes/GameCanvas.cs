using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameCanvas : GameComponent
{
    // DON'T USE the Start() method in any child of GameCanvas, Use InitializePanel() instead.
    [SerializeField] protected List<GameObject> panels;
    [SerializeField] private int mainPanelIndex = -1;
    // Start is called before the first frame update
    private Vector2 refrenceResolution;
    public Vector2 RefrenceResolution { get { return refrenceResolution; } }
    void Start()
    {
        refrenceResolution = gameObject.GetComponent<RectTransform>().sizeDelta;
        foreach (Component child in gameObject.GetComponentsInChildren(typeof(RectTransform),true))
        {
            if (child.gameObject.CompareTag("UI_Panel"))
            {
                panels.Add(child.gameObject);
            }
        }
        if (mainPanelIndex > -1 && panels.Count > mainPanelIndex)
        {
            OpenPanel(panels[mainPanelIndex]);
        }
        Debug.Log("hi");
        InitializePanel();
    }
    protected virtual void InitializePanel() { }
    protected void OpenPanel(GameObject targetPanel)
    {
        OpenPanel(targetPanel, false);
    }

    protected void OpenPanel(GameObject targetPanel, bool OpenOverlay) //OpenOverlay is whether you want the target panel to open alone or not
    {
        if (OpenOverlay == false)
        {
            foreach (GameObject panel in panels)
            {
                if (panel != targetPanel)
                {
                    panel.SetActive(false);
                }
            }
        }
        targetPanel.SetActive(true);
    }

    protected void ClosePanel(GameObject targetPanel)
    {
        targetPanel.SetActive(false);
    }
}
