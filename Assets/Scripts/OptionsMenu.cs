using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider; // Assign in inspector
    [SerializeField] private Slider _sfxSlider; // Assign in inspector

    private void Start()
    {
        _musicSlider.onValueChanged.AddListener(SoundManager.Instance.SetMusicVolume);
        _sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetSFXVolume);
    }
}
