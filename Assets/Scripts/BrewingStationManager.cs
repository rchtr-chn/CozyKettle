using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.UI;

public class BrewingStationManager : MonoBehaviour
{
    [Header("Beverage Selection Data")]
    [SerializeField] private List<Herb> _herbs = new List<Herb>();
    [SerializeField] private List<Herb> _instantHerbs = new List<Herb>();
    [SerializeField] private List<AddOn> _addOns = new List<AddOn>();
    [SerializeField] private List<Beverage> _beverages = new List<Beverage>();
    public Herb SelectedHerb;
    public AddOn SelectedAddOn;
    public Beverage SelectedBeverage;
    public Vector2 SelectedIdealIntensity { get; private set; }
    public float SelectedBrewingIntensity { get; private set; }

    [Header("Minigame References")] // Assign these in inspector
    [SerializeField] private GameObject _DispenserMinigameGO;
    [SerializeField] private GameObject _boilingWaterMinigameGO;
    [SerializeField] private GameObject _frenchPressMinigameGO;
    [SerializeField] private GameObject _matchaWhiskingMinigameGO;
    [SerializeField] private GameObject _pourToCupMinigameGO;

    [Header("Beverage Creation References")] // Assign these in inspector
    [SerializeField] private GameObject _beveragePrefab;
    public RectTransform _beverageSpawnPos;
    [SerializeField] private Transform _beverageParent;

    [Header("Script References")] // Assign these in inspector
    [SerializeField] BrewingInventoryManager _brewingInventoryManager;
    [SerializeField] BrewingPOVScript _brewingPOVScript;

    [Header("ItemChoices")]
    [SerializeField] private Button[] itemChoices = new Button[7]; // assign in inspector
    private Button LastPickedAddon;

    public static Dictionary<ItemSO, BrewingStaticData.Items> SODictionary;

    private void Awake()
    {
        InitSO();
    }

    private void Start()
    {
        SODictionary = new Dictionary<ItemSO, BrewingStaticData.Items>()
        {
            { _herbs[0], BrewingStaticData.Items.BlackHerb },
            { _herbs[1], BrewingStaticData.Items.GreenHerb },
            { _herbs[2], BrewingStaticData.Items.MatchaHerb },
            { _herbs[3], BrewingStaticData.Items.OolongHerb },
            { _addOns[1], BrewingStaticData.Items.Lemon },
            { _addOns[0], BrewingStaticData.Items.Honey },
            { _addOns[2], BrewingStaticData.Items.Milk },
            { _instantHerbs[0], BrewingStaticData.Items.InstantBlack },
            { _instantHerbs[1], BrewingStaticData.Items.InstantGreen },
            { _instantHerbs[2], BrewingStaticData.Items.InstantMatcha },
            { _instantHerbs[3], BrewingStaticData.Items.InstantOolong }
        };

        CheckMaterialAvailability();
    }

    void InitSO()
    {
        if (_beverages != null)
        {
            Beverage[] load = Resources.LoadAll<Beverage>("OBJs/Beverages");
            _beverages = new List<Beverage>(load);
        }
        if (_herbs != null)
        {
            Herb[] load = Resources.LoadAll<Herb>("OBJs/Herbs");
            _herbs = new List<Herb>(load);
        }
        if (_instantHerbs != null)
        {
            Herb[] load = Resources.LoadAll<Herb>("OBJs/InstantHerbs");
            _instantHerbs = new List<Herb>(load);
        }
        if (_addOns != null)
        {
            AddOn[] load = Resources.LoadAll<AddOn>("OBJs/AddOns");
            _addOns = new List<AddOn>(load);
        }
    }

    void CheckMaterialAvailability()
    {
        if(BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.BlackHerb) <= 0 && BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.InstantBlack) <= 0)
        {
            itemChoices[0].interactable = false;
        }
        if (BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.GreenHerb) <= 0 && BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.InstantGreen) <= 0)
        {
            itemChoices[1].interactable = false;
        }
        if (BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.MatchaHerb) <= 0 && BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.InstantMatcha) <= 0)
        {
            itemChoices[2].interactable = false;
        }
        if (BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.OolongHerb) <= 0 && BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.InstantOolong) <= 0)
        {
            itemChoices[3].interactable = false;
        }
        if (BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.Lemon) <= 0)
        {
            itemChoices[4].interactable = false;
        }
        if (BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.Honey) <= 0)
        {
            itemChoices[5].interactable = false;
        }
        if (BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.Milk) <= 0)
        {
            itemChoices[6].interactable = false;
        }
    }

    public void StartBoilingMinigame()
    {
        _boilingWaterMinigameGO.SetActive(true);
    }

    void StartFrenchPressMinigame()
    {
        _frenchPressMinigameGO.SetActive(true);
    }

    void StartMatchaWhiskingMinigame()
    {
        _matchaWhiskingMinigameGO.SetActive(true);
    }

    public void StartPourToCupMinigame()
    {
        _pourToCupMinigameGO.SetActive(true);
    }

    public void StartDispenserMinigame()
    {
        if (SelectedHerb == null)
        {
            return;
        }
        _DispenserMinigameGO.SetActive(true);
    }

    public void CreateBeverage()
    {
        GameObject beverage = Instantiate(_beveragePrefab, _beverageSpawnPos);
        BeverageDisplay beverageDisplay = beverage.GetComponent<BeverageDisplay>();
        if (beverageDisplay != null)
        {
            if(SelectedAddOn == null)
            {
                SelectedAddOn = _addOns[_addOns.Count - 1]; //default to last add-on (none) if no add-on selected
            }

            //check if the selected brewing intensity is ideal or not
            if(SelectedBrewingIntensity < SelectedIdealIntensity.x || SelectedBrewingIntensity > SelectedIdealIntensity.y)
            {
                beverageDisplay.SetIntensity(false);
            }
            else
            {
                beverageDisplay.SetIntensity(true);
            }

            SelectedBeverage = GetBeverageResult(SelectedHerb.HerbTasteProfile, SelectedAddOn.AddOnTasteProfile);
            if(SelectedHerb.IsInstant)
            {
                SelectedBeverage.BeverageCost *= 0.8f;
            }
            beverageDisplay.BeverageSO = SelectedBeverage;
            beverageDisplay.UpdateBeverageInfo();

            DecrementItems(SelectedHerb, SelectedAddOn);
            BrewingStaticData.DisplayAllItemQuantities();
            _brewingInventoryManager.UpdateQtyVisuals();

            ResetBrewingSequence();
        }
    }
    private void DecrementItems(Herb selectedHerb, AddOn selectedAddOn)
    {
        BrewingStaticData.Items usedHerb = SODictionary[SelectedHerb];
        BrewingStaticData.SubtractItemQuantity(usedHerb, 1);

        if (SelectedAddOn.AddOnTasteProfile != TasteProfile.None)
        {
            BrewingStaticData.Items usedAddOn = SODictionary[SelectedAddOn];
            BrewingStaticData.SubtractItemQuantity(usedAddOn, 1);
        }
    }

    Beverage GetBeverageResult(TasteProfile herbTP, TasteProfile addOnTP)
    {
        foreach (var bev in _beverages)
        {
            if (bev.BeverageTasteProfile.Length == 2 && ((bev.BeverageTasteProfile[0] == herbTP && bev.BeverageTasteProfile[1] == addOnTP) || (bev.BeverageTasteProfile[0] == addOnTP && bev.BeverageTasteProfile[1] == herbTP)))
            {
                return bev;
            }
        }
        return _beverages[0]; //default to first beverage if no match found
    }

    void ResetBrewingSequence()
    {
        ResetItemSelections();
        _brewingPOVScript.SetLockPOV(false); // Unlock POV
    }

    void ResetItemSelections()
    {
        SelectedHerb = null;
        SelectedAddOn = null;
        foreach (var btn in itemChoices)
        {
            btn.interactable = true;
            Image img = btn.GetComponent<Image>();
            DimSelectedAddOn(img);
        }
    }

    //-- Beverage Selection Methods --//
    public void SelectBlack()
    {
        if(BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.InstantBlack) > 0)
        {
            foreach (var herb in _instantHerbs)
            {
                if (herb.HerbTasteProfile == TasteProfile.Bold)
                {
                    SoundManager.Instance.PlaySFX(SoundManager.Instance.ClickOnJarSFX);
                    SelectedHerb = herb;
                    return;
                }
            }
        }
        else if(BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.BlackHerb) > 0)
        {
            foreach (var herb in _herbs)
            {
                if (herb.HerbTasteProfile == TasteProfile.Bold)
                {
                    SoundManager.Instance.PlaySFX(SoundManager.Instance.ClickOnJarSFX);
                    SelectedHerb = herb;
                    break;
                }
            }
        }
    }
    public void SelectGreen()
    {
        if (BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.InstantGreen) > 0)
        {
            foreach (var herb in _instantHerbs)
            {
                if (herb.HerbTasteProfile == TasteProfile.Refreshing)
                {
                    SoundManager.Instance.PlaySFX(SoundManager.Instance.ClickOnJarSFX);
                    SelectedHerb = herb;
                    return;
                }
            }
        }
        else if (BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.GreenHerb) > 0)
        {
            foreach (var herb in _herbs)
            {
                if (herb.HerbTasteProfile == TasteProfile.Refreshing)
                {
                    SoundManager.Instance.PlaySFX(SoundManager.Instance.ClickOnJarSFX);
                    SelectedHerb = herb;
                    break;
                }
            }
        }
    }
    public void SelectMatcha()
    {
        if (BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.InstantMatcha) > 0)
        {
            foreach (var herb in _instantHerbs)
            {
                if (herb.HerbTasteProfile == TasteProfile.Grassy)
                {
                    SoundManager.Instance.PlaySFX(SoundManager.Instance.ClickOnJarSFX);
                    SelectedHerb = herb;
                    return;
                }
            }
        }
        else if (BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.MatchaHerb) > 0)
        {
            foreach (var herb in _herbs)
            {
                if (herb.HerbTasteProfile == TasteProfile.Grassy)
                {
                    SoundManager.Instance.PlaySFX(SoundManager.Instance.ClickOnJarSFX);
                    SelectedHerb = herb;
                    break;
                }
            }
        }
    }
    public void SelectOolong()
    {
        if (BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.InstantOolong) > 0)
        {
            foreach (var herb in _instantHerbs)
            {
                if (herb.HerbTasteProfile == TasteProfile.Floral)
                {
                    SoundManager.Instance.PlaySFX(SoundManager.Instance.ClickOnJarSFX);
                    SelectedHerb = herb;
                    return;
                }
            }
        }
        else if (BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.OolongHerb) > 0)
        {
            foreach (var herb in _herbs)
            {
                if (herb.HerbTasteProfile == TasteProfile.Floral)
                {
                    SoundManager.Instance.PlaySFX(SoundManager.Instance.ClickOnJarSFX);
                    SelectedHerb = herb;
                    break;
                }
            }
        }
    }

    public void SelectLemon()
    {
        if(BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.Lemon) > 0)
        {
            if (SelectedAddOn == null || SelectedAddOn.AddOnTasteProfile != TasteProfile.Citrusy)
            {
                foreach (var addOn in _addOns)
                {
                    if (addOn.AddOnTasteProfile == TasteProfile.Citrusy)
                    {
                        SoundManager.Instance.PlaySFX(SoundManager.Instance.UIClickSFX);
                        SelectedAddOn = addOn;
                        break;
                    }
                }
            }
            else if (SelectedAddOn.AddOnTasteProfile == TasteProfile.Citrusy)
            {
                SoundManager.Instance.PlaySFX(SoundManager.Instance.UIClickSFX);
                SelectedAddOn = null;
            }
        }
    }

    public void SelectHoney()
    {
        if (BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.Honey) > 0)
        {
            if (SelectedAddOn == null || SelectedAddOn.AddOnTasteProfile != TasteProfile.Sweet)
            {
                foreach (var addOn in _addOns)
                {
                    if (addOn.AddOnTasteProfile == TasteProfile.Sweet)
                    {
                        SoundManager.Instance.PlaySFX(SoundManager.Instance.ClickOnJarSFX);
                        SelectedAddOn = addOn;
                        break;
                    }
                }
            }
            else if (SelectedAddOn.AddOnTasteProfile == TasteProfile.Sweet)
            {
                SoundManager.Instance.PlaySFX(SoundManager.Instance.ClickOnJarSFX);
                SelectedAddOn = null;
            }
        }
    }

    public void SelectMilk()
    {
        if(BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.Milk) > 0)
        {
            if (SelectedAddOn == null || SelectedAddOn.AddOnTasteProfile != TasteProfile.Creamy)
            {
                foreach (var addOn in _addOns)
                {
                    if (addOn.AddOnTasteProfile == TasteProfile.Creamy)
                    {
                        SoundManager.Instance.PlaySFX(SoundManager.Instance.UIClickSFX);
                        SelectedAddOn = addOn;
                        break;
                    }
                }
            }
            else if (SelectedAddOn.AddOnTasteProfile == TasteProfile.Creamy)
            {
                SoundManager.Instance.PlaySFX(SoundManager.Instance.UIClickSFX);
                SelectedAddOn = null;
            }
        }
    }

    public void SetBrewingIntensity(float intensity)
    {
        SelectedBrewingIntensity = intensity;
        Debug.Log("Brewing Intensity Set To: " + SelectedBrewingIntensity);
    }

    public float GetBrewingIntensity()
    {
        return SelectedBrewingIntensity;
    }

    public void SetIdealIntensity(Vector2 idealIntensity)
    {
        SelectedIdealIntensity = idealIntensity;
        Debug.Log("Ideal Intensity Set To: " + SelectedIdealIntensity);
    }

    public Vector2 GetIdealIntensity()
    {
        return SelectedIdealIntensity;
    }

    public void DimSelectedAddOn(Image img)
    {
        if (SelectedAddOn != null)
        {
            Color cb = img.color;
            cb = new Color(0.5f, 0.5f, 0.5f);
            img.color = cb;
        }
        else
        {
            Color cb = img.color;
            cb = new Color(1f, 1f, 1f);
            img.color = cb;
        }

        if (LastPickedAddon == null)
        {
            LastPickedAddon = img.GetComponent<Button>();
        }
        else if (LastPickedAddon != img.GetComponent<Button>())
        {
            Image lastImg = LastPickedAddon.GetComponent<Image>();
            Color cl = lastImg.color;
            cl = new Color(1f, 1f, 1f);
            lastImg.color = cl;
            LastPickedAddon = img.GetComponent<Button>();
        }
    }

    public void StartMinigameSequences()
    {
        if (SelectedHerb.HerbTasteProfile == TasteProfile.Grassy)
        {
            StartMatchaWhiskingMinigame();
        }
        else
        {
            StartFrenchPressMinigame();
        }
    }
}
