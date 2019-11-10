using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
public class DirectorController : MonoBehaviour
{
    [SerializeField] List<TimelineAsset> timeLines;

    private PlayableDirector director;
    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    void Update()
    {
        
    }  

    public void PlayTrack(int i)
    {
        if(i < timeLines.Count)
        {
            director.playableAsset = timeLines[i];
            director.Play();
        }
    }
}
