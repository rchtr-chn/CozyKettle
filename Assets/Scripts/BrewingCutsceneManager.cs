using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Playables;
using UnityEngine.Events;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class BrewingCutsceneManager : MonoBehaviour
{
    [SerializeField] private BrewingStationManager _brewingStationManager; // Assign in inspector

    [SerializeField] private PlayableDirector _playableDirector; // Assign in inspector

    public TimelineAsset BrewingCutsceneTimeline; // Assign in inspector
    public TimelineAsset BrewingHoneyCutsceneTimeline; // Assign in inspector
    public TimelineAsset BrewingLemonCutsceneTimeline; // Assign in inspector
    public TimelineAsset BrewingMilkCutsceneTimeline; // Assign in inspector
    public TimelineAsset MatchaCutsceneTimeline; // Assign in inspector
    public TimelineAsset MatchaHoneyCutsceneTimeline; // Assign in inspector
    public TimelineAsset MatchaLemonCutsceneTimeline; // Assign in inspector
    public TimelineAsset MatchaMilkCutsceneTimeline; // Assign in inspector
    [SerializeField] private TimelineAsset _summaryScreenTimeline; // Assign in inspector

    public UnityEvent OnCutsceneEnd;

    [Header("UI Elements")]
    [SerializeField] private Image _fPressFillingTop; // Assign in inspector
    [SerializeField] private Image _fPressFillingBottom; // Assign in inspector

    public void PlayCutscene()
    {
        if(_playableDirector.playableAsset != _summaryScreenTimeline)
        {
            _fPressFillingBottom.color = _brewingStationManager.SelectedHerb.BrewColor;
            _fPressFillingTop.color = _brewingStationManager.SelectedHerb.BrewColor;

            _playableDirector.stopped += OnVideoEnd;
        }

        _playableDirector.Play();
    }

    void OnVideoEnd(PlayableDirector dir)
    {
        if(_playableDirector.playableAsset != _summaryScreenTimeline)
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
