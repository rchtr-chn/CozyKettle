using UnityEngine;
using UnityEngine.EventSystems;

public class Kettle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Vector3 DefaultPosition;
    public Vector3 LatestLegalPosition;

    [SerializeField] private Dispenser _dispenser;
    private Canvas _canvas;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvas = GetComponentInParent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        DefaultPosition = LatestLegalPosition = _rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (transform.parent.name != _dispenser.transform.parent.name)
        {
            _canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(transform.parent.name != _dispenser.transform.parent.name)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent.name != _dispenser.transform.parent.name)
        {
            _canvasGroup.blocksRaycasts = true;
            Dispenser dispenser = eventData.pointerEnter?.GetComponent<Dispenser>();
            if (dispenser == null)
            {
                _rectTransform.anchoredPosition = LatestLegalPosition;
            }
        }
    }
}
