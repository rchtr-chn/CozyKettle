using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Playables;
using UnityEngine.Events;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class BrewingCutsceneManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector; // Assign in inspector

    public TimelineAsset BrewingCutsceneTimeline; // Assign in inspector
    public TimelineAsset MatchaCutsceneTimeline; // Assign in inspector
    [SerializeField] private TimelineAsset _SummaryScreenTimeline; // Assign in inspector

    public UnityEvent OnCutsceneEnd;

    public void PlayCutscene()
    {
        if(_playableDirector.playableAsset == BrewingCutsceneTimeline || _playableDirector.playableAsset == MatchaCutsceneTimeline)
        {
            _playableDirector.stopped += OnVideoEnd;
        }

        _playableDirector.Play();
    }

    void OnVideoEnd(PlayableDirector dir)
    {
        if(_playableDirector.playableAsset == BrewingCutsceneTimeline || _playableDirector.playableAsset == MatchaCutsceneTimeline)
        {
            _playableDirector.stopped -= OnVideoEnd;
        }

        OnCutsceneEnd.Invoke();

    }

    public void SetCutscene(TimelineAsset timeline)
    {
        _playableDirector.playableAsset = timeline;
    }
}
