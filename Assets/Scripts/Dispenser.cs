using UnityEngine;
using UnityEngine.EventSystems;

public class Dispenser : MonoBehaviour, IDropHandler
{
    [SerializeField] private BrewingStationManager _brewingStationManager;
    [SerializeField] private Vector2 _dispenserSlotPos = new Vector2(0f, -110f);
    [SerializeField] private RectTransform _dispenserRectTransform;
    private Transform _kettleOriginalParent;

    private void Awake()
    {
        if (_brewingStationManager == null)
        {
            _brewingStationManager = FindAnyObjectByType<BrewingStationManager>();
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Kettle kettle = eventData.pointerDrag.GetComponent<Kettle>();
            RectTransform kettleTransform = kettle.GetComponent<RectTransform>();
            if (kettle != null)
            {
                _kettleOriginalParent = kettleTransform.parent;

                _dispenserRectTransform.SetAsLastSibling();
                kettle.transform.SetParent(this.transform.parent);
                kettleTransform.anchoredPosition = _dispenserSlotPos;
            }
            else
            {
                kettleTransform.anchoredPosition = kettle.LatestLegalPosition;
            }
        }
    }

    public void ConfirmSelection(Kettle kettle)
    {
        if (_brewingStationManager.SelectedHerb == null)
        {
            return;
        }
        _brewingStationManager.lockPOV = true;
        if (kettle.transform.parent.name == this.transform.parent.name)
        {
            _brewingStationManager.StartBrewingSequence();
        }

        RectTransform kettleTransform = kettle.GetComponent<RectTransform>();
        kettle.transform.SetParent(_kettleOriginalParent);
        kettleTransform.anchoredPosition = kettle.LatestLegalPosition;
    }
}
