using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class Dispenser : MonoBehaviour, IDropHandler
{
    [SerializeField] private BrewingStationManager _brewingStationManager; // Assign in inspector if Awake() fails
    [SerializeField] private BrewingCutsceneManager _brewingCutsceneManager; // Assign in inspector
    [SerializeField] private Vector2 _dispenserSlotPos = new Vector2(27.5f, -60f);
    [SerializeField] private RectTransform _dispenserRectTransform; // Assign in inspector
    private Transform _kettleOriginalParent;

    [SerializeField] private Slider _dispenserIntensitySlider; // Assign in inspector

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

                SoundManager.Instance.PlaySFX(SoundManager.Instance.UIClickSFX);

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
        if (kettle.transform.parent.name == this.transform.name)
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.DispenserBeepSFX);

            _brewingStationManager.SetBrewingIntensity(_dispenserIntensitySlider.value);
            _brewingStationManager.SetIdealIntensity(_brewingStationManager.SelectedHerb.IdealRange);
            _dispenserIntensitySlider.value = 0.01f;

            // Start brewing cutscene sequence and lock POV
            TimelineAsset selectedTimeline = GetSelectedTimeline(_brewingStationManager.SelectedHerb.HerbTasteProfile, _brewingStationManager.SelectedAddOn.AddOnTasteProfile);
            _brewingCutsceneManager.SetCutscene(selectedTimeline);
            OnConfirmSelection.Invoke();

            _kettleOriginalParent.SetAsLastSibling();

            RectTransform kettleTransform = kettle.GetComponent<RectTransform>();
            kettle.transform.SetParent(_kettleOriginalParent);
            kettleTransform.anchoredPosition = kettle.LatestLegalPosition = kettle.DefaultPosition;
        }
    }

    TimelineAsset GetSelectedTimeline(TasteProfile herb, TasteProfile addOn)
    {
        if (herb == TasteProfile.Grassy) // matcha section
        {
            if (_brewingStationManager.SelectedAddOn != null)
            {
                if (addOn == TasteProfile.Citrusy) // matcha + lemon
                {
                    return _brewingCutsceneManager.MatchaLemonCutsceneTimeline;
                }
                else if (addOn == TasteProfile.Sweet) // matcha + honey
                {
                    return _brewingCutsceneManager.MatchaHoneyCutsceneTimeline;
                }
                else // matcha + milk
                {
                    return _brewingCutsceneManager.MatchaMilkCutsceneTimeline;
                }
            }
            else // matcha
            {
                return _brewingCutsceneManager.MatchaCutsceneTimeline;
            }
        }
        else // non-matcha section
        {
            if (_brewingStationManager.SelectedAddOn != null)
            {
                if (addOn == TasteProfile.Citrusy) // tea + lemon
                {
                    return _brewingCutsceneManager.BrewingLemonCutsceneTimeline;
                }
                else if (addOn == TasteProfile.Sweet) // tea + honey
                {
                    return _brewingCutsceneManager.BrewingHoneyCutsceneTimeline;
                }
                else //  tea + milk
                {
                    return _brewingCutsceneManager.BrewingMilkCutsceneTimeline;
                }
            }
            else // tea
            {
                return _brewingCutsceneManager.BrewingCutsceneTimeline;
            }
        }
    }
}
