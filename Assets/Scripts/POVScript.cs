using System.Collections;
using UnityEngine;

public class POVScript : MonoBehaviour
{
    [SerializeField] BrewingStationManager _brewingStationManager;
    private RectTransform _rectTransform;

    private Vector3 _defaultPos = new Vector3(0f, -420f, 0f);
    private Vector3 _brewingDeskPos = new Vector3(0f, 420f, 0f);
    public bool IsLookingDown = false;

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
        if(!_brewingStationManager.lockPOV)
        {
            CheckPlayerPOV();
        }
    }

    private void CheckPlayerPOV()
    {
        _mousePos = Input.mousePosition;

        if (_mousePos.y < 100f && !IsLookingDown && _lerpCoroutine == null)
        {
            Debug.Log("Looking down!");
            _lerpCoroutine = StartCoroutine(LerpPOV(_brewingDeskPos));
        }
        else if (_mousePos.y > 990f && IsLookingDown && _lerpCoroutine == null)
        {
            Debug.Log("Looking up!");
            _lerpCoroutine = StartCoroutine(LerpPOV(_defaultPos));
        }
    }

    IEnumerator LerpPOV(Vector3 target)
    {
        IsLookingDown = !IsLookingDown;

        float timer = 0f;
        float duration = 0.7f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            _rectTransform.anchoredPosition = Vector3.Lerp(_rectTransform.anchoredPosition, target, timer);
            yield return null;
        }
        _lerpCoroutine = null;
    }
}
