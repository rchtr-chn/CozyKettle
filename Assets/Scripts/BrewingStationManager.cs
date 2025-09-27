using UnityEngine;
using UnityEngine.UI;

public class BrewingStationManager : MonoBehaviour
{
    public Button[] herbChoices;

    [SerializeField] GameObject boilingWaterMinigameGO;
    [SerializeField] GameObject frenchPressMinigameGO;

    private void Start()
    {
        InvertInteractability(herbChoices);
        //boilingWaterMinigameGO.SetActive(false);
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
}
