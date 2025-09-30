using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class BrewingStationManager : MonoBehaviour
{
    public Button[] herbChoices;
    public BeverageChoices selectedBeverage;

    [SerializeField] GameObject boilingWaterMinigameGO;
    [SerializeField] GameObject frenchPressMinigameGO;

    private void Start()
    {
        InvertInteractability(herbChoices);
        //boilingWaterMinigameGO.SetActive(false);
    }

    public void SelectBlack()
    {
        selectedBeverage = BeverageChoices.BlackTea;
    }
    public void SelectGreen()
    {
        selectedBeverage = BeverageChoices.GreenTea;
    }
    public void SelectOolong()
    {
        selectedBeverage = BeverageChoices.OolongTea;
    }
    public void SelectMatcha()
    {
        selectedBeverage = BeverageChoices.Matcha;
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
        Debug.Log("Starting Boiling Minigame");
        boilingWaterMinigameGO.SetActive(true);
    }

    public void StartFrenchPressMinigame()
    {
        frenchPressMinigameGO.SetActive(true);
        InvertInteractability(herbChoices);
    }

    public enum BeverageChoices
    {
        BlackTea,
        GreenTea,
        OolongTea,
        Matcha
    }
}
