using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
public class DirectorController : MonoBehaviour
{
    [SerializeField] List<TimelineAsset> timeLines;
    private PlayableDirector director;

    private static DirectorController _instance;

    public static DirectorController instance { get { return _instance; } }
    void Awake()
    {
        //Check if instance already exists
        if (_instance == null)

            //if not, set instance to this
            _instance = this;

        //If instance already exists and it's not this:
        else if (_instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        // GameAnalytics.Initialize(); 
    }
    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    void Update()
    {

    }

    public void PlayTrack(int i)
    {
        if (i < timeLines.Count)
        {
            director.playableAsset = timeLines[i];
            director.Play();
        }
    }

    public void PlayTrack(PlayableAsset timeline)
    {
        director.playableAsset = timeline;
        director.Stop();
        director.Play();
    }
}
