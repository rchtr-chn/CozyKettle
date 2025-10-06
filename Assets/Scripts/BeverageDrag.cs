using UnityEngine;
using UnityEngine.EventSystems;

public class BeverageDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform _beverageTransform;
    [SerializeField] private RectTransform _beverageSpawnPos;
    [SerializeField] private RectTransform _beverageParent;
    [SerializeField] private Canvas _canvas;
    private Vector2 _lastLegalPosition = Vector2.zero;

    private void Start()
    {
        if (_canvas == null)
            _canvas = GetComponentInParent<Canvas>();
        if (_beverageTransform == null)
            _beverageTransform = GetComponent<RectTransform>();
        if (_beverageSpawnPos == null)
            _beverageSpawnPos = GameObject.Find("BeverageSpawnPos").GetComponent<RectTransform>();
        if (_beverageParent == null)
            _beverageParent = GameObject.Find("BeverageParent").GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _beverageTransform.SetParent(_beverageParent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _beverageTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(eventData.pointerEnter == null || eventData.pointerEnter.tag != "CustomerArea")
        {
            _beverageTransform.SetParent(_beverageSpawnPos);
            _beverageTransform.anchoredPosition = _lastLegalPosition;
        }
        else
        {
            // Successfully delivered beverage to customer
        }
    }
}
