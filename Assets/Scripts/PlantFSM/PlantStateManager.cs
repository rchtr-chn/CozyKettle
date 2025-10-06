using UnityEngine;
using UnityEngine.EventSystems;

public class PlantStateManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    PlantBaseState _currentState;
    public PlantInitialState InitialState = new PlantInitialState();
    public PlantGrowingState GrowingState = new PlantGrowingState();
    public PlantReadyState ReadyState = new PlantReadyState();

    CanvasGroup _canvasGroup;
    Canvas _canvas;
    RectTransform _rectTransform;

    public Seed SeedData;
    public bool IsWatered = false;
    public bool CanBeWatered = true;
    public int GrowthCountdown;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = GetComponent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();

        _currentState = InitialState;
        _currentState.EnterState(this);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(PlantBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
    }
}
