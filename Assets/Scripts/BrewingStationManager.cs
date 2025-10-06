using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrewingStationManager : MonoBehaviour
{
    [SerializeField] private List<Herb> _herbs = new List<Herb>();
    [SerializeField] private List<AddOn> _addOns = new List<AddOn>();
    [SerializeField] private List<Beverage> _beverages = new List<Beverage>();
    public Herb SelectedHerb;
    public AddOn SelectedAddOn;
    public Beverage SelectedBeverage;

    [SerializeField] private GameObject _boilingWaterMinigameGO;
    [SerializeField] private GameObject _frenchPressMinigameGO;

    [SerializeField] private GameObject _beveragePrefab;
    [SerializeField] private RectTransform _beverageSpawnPos;
    [SerializeField] private Transform _beverageParent;

    [SerializeField] private GameObject _cutsceneManager;
    [SerializeField] private GameObject _brewingCutscene;

    public bool lockPOV = false;

    private void Awake()
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
        if (_addOns != null)
        {
            AddOn[] load = Resources.LoadAll<AddOn>("OBJs/AddOns");
            _addOns = new List<AddOn>(load);
        }
    }

    public void InvertInteractability(Button[] buttons)
    {
        foreach (var i in buttons)
        {
            if (i != null)
            {
                i.interactable = !i.interactable;
            }
        }
    }

    public void StartBoilingMinigame()
    {
        _boilingWaterMinigameGO.SetActive(true);
    }

    public void StartFrenchPressMinigame()
    {
        _frenchPressMinigameGO.SetActive(true);
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

            SelectedBeverage = GetBeverageResult(SelectedHerb.HerbTasteProfile, SelectedAddOn.AddOnTasteProfile);
            beverageDisplay.BeverageSO = SelectedBeverage;
            beverageDisplay.UpdateBeverageInfo();

            ResetBrewingSequence();
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
        SelectedHerb = null;
        SelectedAddOn = null;
        lockPOV = false;
    }

    public void StartBrewingSequence()
    {
        _cutsceneManager.SetActive(true);
        _brewingCutscene.SetActive(true);
    }


    //-- Beverage Selection Methods --//

    public void SelectBlack()
    {
        foreach (var herb in _herbs)
        {
            if (herb.HerbTasteProfile == TasteProfile.Bold)
            {
                SelectedHerb = herb;
                break;
            }
        }
    }
    public void SelectGreen()
    {
        foreach (var herb in _herbs)
        {
            if (herb.HerbTasteProfile == TasteProfile.Refreshing)
            {
                SelectedHerb = herb;
                break;
            }
        }
    }
    public void SelectMatcha()
    {
        foreach (var herb in _herbs)
        {
            if (herb.HerbTasteProfile == TasteProfile.Grassy)
            {
                SelectedHerb = herb;
                break;
            }
        }
    }
    public void SelectOolong()
    {
        foreach (var herb in _herbs)
        {
            if (herb.HerbTasteProfile == TasteProfile.Floral)
            {
                SelectedHerb = herb;
                break;
            }
        }
    }

    public void SelectLemon()
    {
        if(SelectedAddOn == null || SelectedAddOn.AddOnTasteProfile != TasteProfile.Citrusy)
        {
            foreach (var addOn in _addOns)
            {
                if (addOn.AddOnTasteProfile == TasteProfile.Citrusy)
                {
                    SelectedAddOn = addOn;
                    break;
                }
            }
        }
        else if(SelectedAddOn.AddOnTasteProfile == TasteProfile.Citrusy)
        {
            SelectedAddOn = null;
        }
    }

    public void SelectHoney()
    {
        if (SelectedAddOn == null || SelectedAddOn.AddOnTasteProfile != TasteProfile.Sweet)
        {
            foreach (var addOn in _addOns)
            {
                if (addOn.AddOnTasteProfile == TasteProfile.Sweet)
                {
                    SelectedAddOn = addOn;
                    break;
                }
            }
        }
        else if (SelectedAddOn.AddOnTasteProfile == TasteProfile.Sweet)
        {
            SelectedAddOn = null;
        }
    }

    public void SelectMilk()
    {
        if (SelectedAddOn == null || SelectedAddOn.AddOnTasteProfile != TasteProfile.Creamy)
        {
            foreach (var addOn in _addOns)
            {
                if (addOn.AddOnTasteProfile == TasteProfile.Creamy)
                {
                    SelectedAddOn = addOn;
                    break;
                }
            }
        }
        else if (SelectedAddOn.AddOnTasteProfile == TasteProfile.Creamy)
        {
            SelectedAddOn = null;
        }
    }
}
