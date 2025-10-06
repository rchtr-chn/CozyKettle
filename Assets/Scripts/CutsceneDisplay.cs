using UnityEngine;
using UnityEngine.Video;

public class CutsceneDisplay : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    private void OnDisable()
    {
        if(_videoPlayer != null )
        {
            _videoPlayer.Stop();
        }
    }

    public void OnEnable()
    {
        if(_videoPlayer != null)
        {
            _videoPlayer.Play();
        }
    }
}
