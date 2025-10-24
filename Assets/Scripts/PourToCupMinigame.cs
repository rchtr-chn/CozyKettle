using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PourToCupMinigame : MonoBehaviour, IPointerDownHandler, IPointerUpHandler  
{
    private float _maxWidth = 480f;
    private float _maxHeight = 480f;
    private bool _isFull = false;
    [SerializeField] private RectTransform _fillingRT; // Assign in inspector
    [SerializeField] private GameObject _fPressPouring; // Assign in inspector
    [SerializeField] private BrewingStationManager _brewingStationManager; // Assign in inspector
    private Coroutine _pourCoroutine;
    private Coroutine _endMinigameCoroutine;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(_pourCoroutine == null && !_isFull)
        {
            _fPressPouring.SetActive(true);
            _pourCoroutine = StartCoroutine(PourToCup());
        }
        else if (_isFull && _endMinigameCoroutine == null)
        {
            _fPressPouring.SetActive(false);
            _endMinigameCoroutine = StartCoroutine(WaitAndEndMinigame());
        }
    }

    private void OnEnable()
    {
        ResetOldProgress();
    }

    private void ResetOldProgress()
    {
        _fillingRT.sizeDelta = new Vector2(0f, 0f);
        _isFull = false;
        _pourCoroutine = null;
        _endMinigameCoroutine = null;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(_pourCoroutine != null)
        {
            _fPressPouring.SetActive(false);
            StopCoroutine(_pourCoroutine);
            _pourCoroutine = null;

            if(_isFull && _endMinigameCoroutine == null)
            {
                _endMinigameCoroutine = StartCoroutine(WaitAndEndMinigame());
            }
        }
    }

    IEnumerator PourToCup()
    {
        while(_fillingRT.sizeDelta.x < _maxWidth && _fillingRT.sizeDelta.y < _maxHeight)
        {
            _fillingRT.sizeDelta += new Vector2(100f, 100f) * Time.deltaTime;
            yield return null;
        }
        _isFull = true;

        _fPressPouring.SetActive(false);
        _pourCoroutine = null;
        if(_endMinigameCoroutine == null)
        {
            _endMinigameCoroutine = StartCoroutine(WaitAndEndMinigame());
        }
    }

    IEnumerator WaitAndEndMinigame()
    {
        yield return new WaitForSeconds(2f);
        _pourCoroutine = null;
        this.gameObject.SetActive(false);

        _brewingStationManager.CreateBeverage();
    }
}
