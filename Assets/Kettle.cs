using UnityEngine;
using UnityEngine.EventSystems;

public class Kettle : MonoBehaviour,IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool dispenserLocked = false;
    public bool isFilled = false;

    public Vector3 latestLegalPosition;

    [SerializeField] GameObject dispenserSlot;
    [SerializeField] GameObject KettleStove;

    Canvas canvas;
    RectTransform rectTransform;
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        latestLegalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!dispenserLocked)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        Dispenser dispenser = eventData.pointerEnter?.GetComponent<Dispenser>();
        Stove stove = eventData.pointerEnter?.GetComponent<Stove>();
        if (dispenser == null && transform.parent.name != dispenserSlot.name)
        {
            rectTransform.anchoredPosition = latestLegalPosition;
        }
        else if(stove == null && transform.parent.name != KettleStove.name)
        {
            rectTransform.anchoredPosition = latestLegalPosition;
        }
    }
}
