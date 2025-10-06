using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stove : MonoBehaviour, IDropHandler
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private BrewingStationManager _brewingStationManager;
    Vector2 offsetPos = new Vector2(0f, 150f);

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Kettle kettle = eventData.pointerDrag.GetComponent<Kettle>();
            if (kettle != null)
            {
                kettle.transform.SetParent(this.transform);

                _rectTransform.SetAsLastSibling();

                RectTransform kettleTransform = kettle.GetComponent<RectTransform>();
                kettleTransform.anchoredPosition = offsetPos;

                kettle.LatestLegalPosition = kettleTransform.anchoredPosition;
            }
        }
    }
}
