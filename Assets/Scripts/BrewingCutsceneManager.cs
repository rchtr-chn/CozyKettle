using UnityEngine;
using UnityEngine.Video;

public class BrewingCutsceneManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private GameObject _cutsceneDisplay;
    [SerializeField] private BrewingStationManager _brewingStationManager;

    private void Start()
    {
        _videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        _brewingStationManager.StartFrenchPressMinigame();
        vp.gameObject.SetActive(false);
        _cutsceneDisplay.SetActive(false);
    }
}
