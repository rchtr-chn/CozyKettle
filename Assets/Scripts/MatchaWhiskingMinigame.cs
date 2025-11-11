using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchaWhiskingMinigame : MonoBehaviour
{
    [Header("Whisking Parameters")]
    [SerializeField] private RectTransform _whiskRT; // Assign in inspector
    [SerializeField] private MatchaWhisk _matchaWhisk; // Assign in inspector
    [SerializeField] private float _maxProgress = 100f;
    [SerializeField] private float _progressDecayRate = 10f; // Progress decay per second
    [SerializeField] private float _progressIncreaseRate = 30f; // Progress increase per second when whisking correctly
    [SerializeField] private float _whiskingThreshold = 50f; // Minimum speed to consider as effective whisking
    private Vector2 _lastWhiskPosition;
    private float _currentProgress = 0f;
    private bool _doneWhisking = false;
    private Coroutine _whiskCompleteCoroutine;
    private bool _whiskSFXPlaying = false;

    [Header("UI Elements")]
    [SerializeField] private Image _matchaFoam1; // Assign in inspector
    [SerializeField] private Image _matchaFoam2; // Assign in inspector

    [Header("References")]
    [SerializeField] private BrewingStationManager _brewingStationManager; // Assign in inspector
    [SerializeField] private GameObject _minigameParent;

    private void OnEnable()
    {
        _matchaWhisk.enabled = true;
        // reset variables
        _currentProgress = 0f;
        _doneWhisking = false;
        _lastWhiskPosition = _whiskRT.anchoredPosition;
        // reset foam opacity
        Color color1 = _matchaFoam1.color;
        color1.a = 0f;
        _matchaFoam1.color = color1;
        Color color2 = _matchaFoam2.color;
        color2.a = 0f;
        _matchaFoam2.color = color2;
    }

    private void Update()
    {
        // updates progress based on whisking speed
        if (!_doneWhisking)
        {
            // tracks whisk motion/magnitude
            Vector2 delta = (Vector2)_whiskRT.anchoredPosition - _lastWhiskPosition;
            float whiskingSpeed = delta.magnitude / Time.deltaTime;
            _lastWhiskPosition = _whiskRT.anchoredPosition;

            if (whiskingSpeed >= _whiskingThreshold)
            {
                if(!_whiskSFXPlaying)
                {
                    SoundManager.Instance.StartChargingSFX(SoundManager.Instance.MatchaWhiskSFX);
                    _whiskSFXPlaying = true;
                }
                _currentProgress += _progressIncreaseRate * Time.deltaTime;
            }
            else
            {
                if (_whiskSFXPlaying)
                {
                    SoundManager.Instance.StopChargingSFX();
                    _whiskSFXPlaying = false;
                }
                _currentProgress -= _progressDecayRate * Time.deltaTime;
            }

            // clamps progress and updates froth opacity
            _currentProgress = Mathf.Clamp(_currentProgress, 0f, _maxProgress);
            AdjustMatchaFoamOpacity();
        }
        // checks for completion
        if (_currentProgress >= _maxProgress && _whiskCompleteCoroutine == null)
        {
            OnWhiskingComplete();
        }
    }

    private void AdjustMatchaFoamOpacity()
    {
        float halfProgress = _maxProgress / 2;
        if(_currentProgress <= halfProgress)
        {
            float alpha = Mathf.Clamp01(_currentProgress / halfProgress);
            Color color = _matchaFoam1.color;
            color.a = alpha;
            _matchaFoam1.color = color;
        }
        else
        {
            float alpha = Mathf.Clamp01((_currentProgress - halfProgress) / halfProgress);
            Color color = _matchaFoam2.color;
            color.a = alpha;
            _matchaFoam2.color = color;
        }
    }

    private void OnWhiskingComplete()
    {
        SoundManager.Instance.StopChargingSFX();
        _matchaWhisk.enabled = false;
        _doneWhisking = true;
        _whiskCompleteCoroutine = StartCoroutine(WaitAndEndMinigame());
    }
    IEnumerator WaitAndEndMinigame()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.LockSFX);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.MinigameSuccessSFX);

        yield return new WaitForSeconds(2f);
        _whiskCompleteCoroutine = null;
        _minigameParent.SetActive(false);

        _brewingStationManager.StartPourToCupMinigame();
    }

}
