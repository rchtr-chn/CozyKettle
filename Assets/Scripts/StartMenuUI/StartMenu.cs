using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    float _targetScale = 2f;
    float _zoomDuration = 2f;

    float _targetAlpha = 1f;
    Vector2 _targetPosition = new Vector2(0, 420f);


    [Header("UI References")]
    [SerializeField] private RectTransform _startMenuBG; // Assign in inspector
    [SerializeField] private CanvasGroup _fadeOutEffect; // Assign in inspector
    [SerializeField] private CanvasGroup _fadeInEffect; // Assign in inspector
    [SerializeField] private Button _startButton; // Assign in inspector
    [SerializeField] private Button _optionButton; // Assign in inspector

    private void Awake()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _startButton.onClick.AddListener(() => SoundManager.Instance.PlaySFX(SoundManager.Instance.UIClickSFX));
        _optionButton.onClick.AddListener(() => SoundManager.Instance.PlaySFX(SoundManager.Instance.UIClickSFX));
    }

    private void Start()
    {
        StartCoroutine(InitialFadeIn());
    }

    public void OnStartButtonClicked()
    {
        _startButton.gameObject.SetActive(false);
        _optionButton.gameObject.SetActive(false);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.FootstepSFX);
        StartCoroutine(ZoomInAndStartGame());
    }

    IEnumerator ZoomInAndStartGame()
    {
        float elapsed = 0f;

        float startScale = _startMenuBG.localScale.x;
        float startAlpha = _fadeOutEffect.alpha;
        Vector2 startPosition = _startMenuBG.anchoredPosition;

        while (elapsed < _zoomDuration)
        {
            float newScale = Mathf.Lerp(startScale, _targetScale, elapsed / _zoomDuration);
            _startMenuBG.localScale = new Vector3(newScale, newScale, 1f);

            Vector2 newPosition = Vector2.Lerp(startPosition, _targetPosition, elapsed / _zoomDuration);
            _startMenuBG.anchoredPosition = newPosition;

            float newAlpha = Mathf.Lerp(startAlpha, _targetAlpha, elapsed / _zoomDuration);
            _fadeOutEffect.alpha = newAlpha;

            elapsed += Time.deltaTime;
            yield return null;
        }

        _startMenuBG.localScale = new Vector3(_targetScale, _targetScale, 1f);
        _fadeOutEffect.alpha = _targetAlpha;
        _startMenuBG.anchoredPosition = _targetPosition;


        SceneController.Instance.LoadTeaShopScene();
    }

    IEnumerator InitialFadeIn()
    {
        float fadeDuration = 0.75f;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            _fadeInEffect.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _fadeInEffect.alpha = 0f;
    }
}
