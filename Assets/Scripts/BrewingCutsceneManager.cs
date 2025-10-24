using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Playables;
using UnityEngine.Events;
using UnityEngine.Timeline;

public class BrewingCutsceneManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;

    [SerializeField] private TimelineAsset _brewingCutsceneTimeline;
    [SerializeField] private TimelineAsset _SummaryScreenTimeline;

    public UnityEvent OnCutsceneEnd;

    private void Start()
    {
    }

    public void PlayCutscene()
    {
        if(_playableDirector.playableAsset == _brewingCutsceneTimeline)
        {
            _playableDirector.stopped += OnVideoEnd;
        }

        _playableDirector.Play();
    }

    void OnVideoEnd(PlayableDirector dir)
    {
        if(_playableDirector.playableAsset == _brewingCutsceneTimeline)
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
