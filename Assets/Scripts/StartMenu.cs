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
    [SerializeField] private CanvasGroup _fadeEffect; // Assign in inspector
    [SerializeField] private Button _startButton; // Assign in inspector
    [SerializeField] private Button _optionButton; // Assign in inspector

    private void Awake()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _startButton.onClick.AddListener(() => SoundManager.Instance.PlaySFX(SoundManager.Instance.UIClickSFX));
        _optionButton.onClick.AddListener(() => SoundManager.Instance.PlaySFX(SoundManager.Instance.UIClickSFX));
    }

    public void OnStartButtonClicked()
    {
        _startButton.gameObject.SetActive(false);
        _optionButton.gameObject.SetActive(false);
        StartCoroutine(ZoomInAndStartGame());
    }

    IEnumerator ZoomInAndStartGame()
    {
        float elapsed = 0f;

        float startScale = _startMenuBG.localScale.x;
        float startAlpha = _fadeEffect.alpha;
        Vector2 startPosition = _startMenuBG.anchoredPosition;

        while (elapsed < _zoomDuration)
        {
            float newScale = Mathf.Lerp(startScale, _targetScale, elapsed / _zoomDuration);
            _startMenuBG.localScale = new Vector3(newScale, newScale, 1f);

            Vector2 newPosition = Vector2.Lerp(startPosition, _targetPosition, elapsed / _zoomDuration);
            _startMenuBG.anchoredPosition = newPosition;

            float newAlpha = Mathf.Lerp(startAlpha, _targetAlpha, elapsed / _zoomDuration);
            _fadeEffect.alpha = newAlpha;

            elapsed += Time.deltaTime;
            yield return null;
        }

        _startMenuBG.localScale = new Vector3(_targetScale, _targetScale, 1f);
        _fadeEffect.alpha = _targetAlpha;
        _startMenuBG.anchoredPosition = _targetPosition;


        SceneController.Instance.LoadTeaShopScene();
    }
}
