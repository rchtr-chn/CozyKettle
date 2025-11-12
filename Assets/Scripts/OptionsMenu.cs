using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider; // Assign in inspector
    [SerializeField] private Slider _sfxSlider; // Assign in inspector

    [SerializeField] private Button _deleteButton; // Assign in inspector
    [SerializeField] private Button _exitButton; // Assign in inspector

    private void Start()
    {
        _deleteButton.onClick.AddListener(() => SaveManager.Instance.DeleteSaveData());
        _exitButton.onClick.AddListener(() => SceneController.Instance.LoadStartMenuScene());

        _musicSlider.value = SoundManager.Instance.GetMusicVolume();
        _sfxSlider.value = SoundManager.Instance.GetSFXVolume();

        _musicSlider.onValueChanged.AddListener(SoundManager.Instance.SetMusicVolume);
        _sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetSFXVolume);
    }
}
