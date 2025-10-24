using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Dispenser : MonoBehaviour, IDropHandler
{
    [SerializeField] private BrewingStationManager _brewingStationManager; // Assign in inspector if Awake() fails
    [SerializeField] private Vector2 _dispenserSlotPos = new Vector2(27.5f, -60f);
    [SerializeField] private RectTransform _dispenserRectTransform; // Assign in inspector
    private Transform _kettleOriginalParent;

    public UnityEvent OnConfirmSelection;

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
                kettle.transform.SetParent(this.transform);
                kettleTransform.anchoredPosition = _dispenserSlotPos;
                kettle.LatestLegalPosition = _dispenserSlotPos;
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
        if (kettle.transform.parent.name == this.transform.name)
        {
            // Start brewing cutscene sequence and lock POV
            OnConfirmSelection.Invoke();

            _kettleOriginalParent.SetAsLastSibling();

            RectTransform kettleTransform = kettle.GetComponent<RectTransform>();
            kettle.transform.SetParent(_kettleOriginalParent);
            kettleTransform.anchoredPosition = kettle.LatestLegalPosition = kettle.DefaultPosition;
        }
    }
}
