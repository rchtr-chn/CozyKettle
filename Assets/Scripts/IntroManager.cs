using UnityEngine;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    private void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer source)
    {
        SceneController.Instance.LoadStartMenuScene();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneController.Instance.LoadStartMenuScene();
        }
    }
}
