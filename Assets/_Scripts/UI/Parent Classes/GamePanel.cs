using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : GameComponent
{
    // Start is called before the first frame update
    private void Start()
    {
        InitializePanel();
    }
    protected virtual void InitializePanel() { }
    protected GameObject InitUIElement(GameObject prefab, Transform parent)
    {
        GameObject temp = Instantiate(prefab, parent);

        RectTransform tempRect = temp.GetComponent<RectTransform>();
        RectTransform prefabRect = prefab.GetComponent<RectTransform>();

        tempRect.localPosition = prefabRect.localPosition;
        tempRect.localRotation = prefabRect.localRotation;
        tempRect.localScale = prefabRect.localScale;

        return temp;
    }

    // protected GameObject[] InitializeItems(GameObject prefab, GameObject container, World currentWorld, int numItems, float itemWidth, float itemHeight, float padding, int cols)
    // {
    //     GameObject[] temp = new GameObject[numItems];

    //     int rows = numItems / cols;
    //     float leftinsit = padding;
    //     float topinsit;


    //     //Set up Container
    //     RectTransform contRect = container.GetComponent<RectTransform>();
    //     if (currentWorld == null)
    //     {
    //         contRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (itemHeight + padding) * rows);
    //         contRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (itemWidth + 2 * padding) * cols);
    //         topinsit = padding * 1.2f;
    //     }
    //     else
    //     {
    //         // Debug.Log(Screen.width);
    //         contRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, LevelCanvas.instance.RefrenceResolution.x * 0.8f);
    //         Debug.Log(contRect.sizeDelta.x);
    //         padding = contRect.sizeDelta.x / ((5 * cols) + 1);
    //         Debug.Log(padding);
    //         itemWidth = 4 * padding;
    //         itemHeight = 4 * padding;
    //         leftinsit = padding;
    //         topinsit = padding * 2;
    //         contRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ((itemHeight + padding) * (rows + 1)) + topinsit);
    //         // contRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (itemWidth + (padding + 1)) * cols);
    //     }
    //     //Attach Canvas

    //     //Set up items
    //     int colCount = 0;
    //     for (int i = 0; i < numItems; i++)
    //     {
    //         GameObject item = InitUIElement(prefab, container.transform);
    //         RectTransform rect = item.GetComponent<RectTransform>();


    //         rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, leftinsit, itemWidth);
    //         rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, topinsit, itemHeight);
    //         if (currentWorld == null) //Worlds
    //         {
    //             // topinsit = padding * 0.5f;
    //             leftinsit += (itemWidth + 2 * padding);
    //             item.GetComponent<WorldPanelScript>().PanelWorld = GameManager.instance.AllWorlds[i];
    //         }
    //         else //levels
    //         {
    //             item.GetComponent<LevelButtonScript>().SetLevel(currentWorld.Levels[i],i + 1);
    //             item.name = "Level " + (i + 1);
    //             leftinsit += (itemWidth + padding);
    //         }
    //         temp[i] = item;


    //         colCount++;

    //         if (colCount == cols)
    //         {
    //             topinsit += (padding + itemHeight);
    //             colCount = 0;
    //             leftinsit = padding;
    //         }
    //         item.SetActive(true);
    //     }
    //     return temp;
    // }
}
