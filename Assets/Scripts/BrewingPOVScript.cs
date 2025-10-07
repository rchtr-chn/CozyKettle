using System.Collections;
using UnityEngine;

public class BrewingPOVScript : MonoBehaviour
{
    [SerializeField] BrewingStationManager _brewingStationManager;
    private RectTransform _rectTransform;

    [SerializeField] private float _lerpDuration = 0.7f;
    private Vector3 _defaultPos = new Vector3(0f, -420f, 0f);
    private Vector3 _brewingDeskPos = new Vector3(0f, 420f, 0f);
    public bool IsLookingDown = false;
    public bool IsOnPhone = false;

    private Vector2 _mousePos;

    Coroutine _lerpCoroutine;

    private void Awake()
    {
        if (_brewingStationManager == null)
        {
            _brewingStationManager = FindAnyObjectByType<BrewingStationManager>();
        }
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(!_brewingStationManager.lockPOV && !IsOnPhone)
        {
            CheckPlayerPOV();
        }
    }

    private void CheckPlayerPOV()
    {
        _mousePos = Input.mousePosition;

        if (_mousePos.y < 100f && !IsLookingDown && _lerpCoroutine == null)
        {
            _lerpCoroutine = StartCoroutine(LerpPOV(_brewingDeskPos));
        }
        else if (_mousePos.y > 990f && IsLookingDown && _lerpCoroutine == null)
        {
            _lerpCoroutine = StartCoroutine(LerpPOV(_defaultPos));
        }
    }

    IEnumerator LerpPOV(Vector3 target)
    {
        IsLookingDown = !IsLookingDown;

        float timer = 0f;

        while (timer < _lerpDuration)
        {
            timer += Time.deltaTime;
            _rectTransform.anchoredPosition = Vector3.Lerp(_rectTransform.anchoredPosition, target, timer);
            yield return null;
        }
        _lerpCoroutine = null;
    }
}
