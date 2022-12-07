using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Cutscene : MonoBehaviour
{
    protected PlayableDirector playableDirector;

    [SerializeField] protected List<TimelineAsset> timelines;

    protected virtual void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    protected static void Bind(PlayableDirector director, string trackName, GameObject gameObject)
    {
        var timeline = director.playableAsset as TimelineAsset;
        foreach (var track in timeline.GetOutputTracks())
        {
            if (track.name == trackName)
            {
                director.SetGenericBinding(track, gameObject);
                break;
            }
        }
    }

    protected void DeactivatePlayer()
    {
        GameManager.instance.GetPlayer().SetIsAbleToMove(false);
    }

    protected void ActivatePlayer()
    {
        GameManager.instance.GetPlayer().SetIsAbleToMove(true);
    }
}
