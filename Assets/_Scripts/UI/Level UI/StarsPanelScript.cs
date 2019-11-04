using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsPanelScript : GamePanel
{
    // Start is called before the first frame update

    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;
    void Start()
    {
        
    }
    protected override void OnComponentEnabled()
    {
        UpdateStars();
    }
    private void UpdateStars()
    {
        Debug.Log(LevelManager.Instance.CurrentLevel.getCurrentStars());
        switch (LevelManager.Instance.CurrentLevel.getCurrentStars())
        {
            case 0:
                star1.SetActive(false);
                star2.SetActive(false);
                star3.SetActive(false);
                break;
            case 1:
                star1.SetActive(true);
                star2.SetActive(false);
                star3.SetActive(false);
                break;

            case 2:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(false);
                break;

            case 3:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                break;

            default:
                break;
        }
    }
}
