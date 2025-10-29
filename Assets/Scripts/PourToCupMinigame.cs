using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PourToCupMinigame : MonoBehaviour, IPointerDownHandler, IPointerUpHandler  
{
    [SerializeField] private Sprite[] _pourerSprite; // Assign in inspector
    /*
     * 0 = Black
     * 1 = Green
     * 2 = Matcha
     * 3 = Oolong
     */

    private float _maxWidth = 480f;
    private float _maxHeight = 480f;
    private bool _isFull = false;
    [SerializeField] private RectTransform _fillingRT; // Assign in inspector
    [SerializeField] private GameObject _fPressPouring; // Assign in inspector
    [SerializeField] private BrewingStationManager _brewingStationManager; // Assign in inspector
    private Coroutine _pourCoroutine;
    private Coroutine _endMinigameCoroutine;

    private static Dictionary<TasteProfile, int> _beverageTPToSpriteIndex = new Dictionary<TasteProfile, int>()
    {
        { TasteProfile.Bold, 0 },
        { TasteProfile.Refreshing, 1 },
        { TasteProfile.Grassy, 2 },
        { TasteProfile.Floral, 3 }
    };

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_pourCoroutine == null && !_isFull)
        {
            SoundManager.Instance.StartChargingSFX(SoundManager.Instance.PouringTeaSFX);
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
        // Set correct pourer sprite based on selected herb
        _beverageTPToSpriteIndex.TryGetValue(_brewingStationManager.SelectedHerb.herbTasteProfile, out int spriteIndex);
        Image fPressImg = _fPressPouring.GetComponent<Image>();
        fPressImg.sprite = _pourerSprite[spriteIndex];

        _fillingRT.sizeDelta = new Vector2(0f, 0f);
        _isFull = false;
        _pourCoroutine = null;
        _endMinigameCoroutine = null;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(_pourCoroutine != null)
        {
            SoundManager.Instance.StopChargingSFX();
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
        SoundManager.Instance.StopChargingSFX();

        SoundManager.Instance.PlaySFX(SoundManager.Instance.LockSFX);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.MinigameSuccessSFX);

        yield return new WaitForSeconds(2f);
        _pourCoroutine = null;
        this.gameObject.SetActive(false);

        _brewingStationManager.CreateBeverage();
    }
}
