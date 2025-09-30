using UnityEngine;
using UnityEngine.UI;

public class BrewingStationManager : MonoBehaviour
{
    public Button[] herbChoices;

    [SerializeField] private Beverage[] _beverages;
    public Beverage SelectedBeverage;

    [SerializeField] private GameObject _boilingWaterMinigameGO;
    [SerializeField] private GameObject _frenchPressMinigameGO;

    [SerializeField] private GameObject _beveragePrefab;
    [SerializeField] private Transform _beverageParent;

    private void Awake()
    {
        if(_beverages != null)
        {
            _beverages = Resources.LoadAll<Beverage>("OBJs/Beverages");
        }
    }

    private void Start()
    {
        InvertInteractability(herbChoices);
        //boilingWaterMinigameGO.SetActive(false);
    }

    public void SelectBlack()
    {
        SelectedBeverage = _beverages[0];
    }
    public void SelectGreen()
    {
        SelectedBeverage = _beverages[1];
    }
    public void SelectMatcha()
    {
        SelectedBeverage = _beverages[2];
    }
    public void SelectOolong()
    {
        SelectedBeverage = _beverages[3];
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
        InvertInteractability(herbChoices);
    }
    public void CreateBeverage()
    {
        GameObject beverage = Instantiate(_beveragePrefab, _beverageParent);
        BeverageDisplay beverageDisplay = beverage.GetComponent<BeverageDisplay>();
        if (beverageDisplay != null)
        {
            beverageDisplay.BeverageSO = SelectedBeverage;
            beverageDisplay.UpdateBeverageInfo();
        }
    }
}
