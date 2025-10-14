using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Phone : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] private BrewingPOVScript _brewingPOVScript;
    [SerializeField] private float _lerpDuration = 0.7f;
    [SerializeField] private Vector2 _defaultPos = new Vector3(720f, -640f);
    [SerializeField] private Vector2 _hoveredPos = new Vector3(720f, -560f);
    [SerializeField] private Vector2 _activatedPos = new Vector3(720f, -240f);

    private RectTransform _rectTransform;
    private Coroutine _lerpCoroutine;

    private void Awake()
    {
        if(_rectTransform == null)
        {
            _rectTransform = GetComponent<RectTransform>();
        }
        if (_brewingPOVScript == null)
        {
            _brewingPOVScript = FindAnyObjectByType<BrewingPOVScript>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _brewingPOVScript.IsOnPhone = true;
        if(_lerpCoroutine != null)
        {
            StopCoroutine(_lerpCoroutine);
        }
        _lerpCoroutine = StartCoroutine(LerpToPos(_hoveredPos));
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_lerpCoroutine != null)
        {
            StopCoroutine(_lerpCoroutine);
        }
        _lerpCoroutine = StartCoroutine(LerpToPos(_activatedPos));
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_lerpCoroutine != null)
        {
            StopCoroutine(_lerpCoroutine);
        }
        _lerpCoroutine = StartCoroutine(LerpToPos(_defaultPos));
        _brewingPOVScript.IsOnPhone = false;
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
