using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Phone : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [Header("References")]
    [SerializeField] private BrewingPOVScript _brewingPOVScript; // assign in inspector
    [SerializeField] CanvasGroup _raycastBlocker; // assign in inspector

    [Header("Positions")]
    [SerializeField] private float _lerpDuration = 0.7f;
    [SerializeField] private Vector2 _defaultPos = new Vector3(720f, -640f);
    [SerializeField] private Vector2 _hoveredPos = new Vector3(720f, -560f);
    [SerializeField] private Vector2 _activatedPos = new Vector3(720f, -240f);

    private RectTransform _rectTransform;
    private Coroutine _lerpCoroutine;
    public bool phoneOpened = false;

    private void Awake()
    {
        if(_rectTransform == null)
        {
            _rectTransform = GetComponent<RectTransform>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _brewingPOVScript.SetIsHoveringUI(true);
        if (_lerpCoroutine != null)
        {
            StopCoroutine(_lerpCoroutine);
        }
        _lerpCoroutine = StartCoroutine(LerpToPos(_hoveredPos));
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        phoneOpened = true;
        if (_lerpCoroutine != null)
        {
            StopCoroutine(_lerpCoroutine);
        }
        _lerpCoroutine = StartCoroutine(LerpToPos(_activatedPos));
        _raycastBlocker.blocksRaycasts = false;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        phoneOpened = false;
        if (_lerpCoroutine != null)
        {
            StopCoroutine(_lerpCoroutine);
        }
        _lerpCoroutine = StartCoroutine(LerpToPos(_defaultPos));
        _brewingPOVScript.SetIsHoveringUI(false);
        _raycastBlocker.blocksRaycasts = true;
    }

    IEnumerator LerpToPos(Vector3 target)
    {
        float timer = 0f;
        while (timer < _lerpDuration)
        {
            timer += Time.deltaTime;
            _rectTransform.anchoredPosition = Vector3.Lerp(_rectTransform.anchoredPosition, target, timer / _lerpDuration);
            yield return null;
        }
    }
}
