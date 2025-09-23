using UnityEngine;
using UnityEngine.UI;

public class BrewingStationManager : MonoBehaviour
{
    public Button[] herbChoices;

    public GameObject boilingWaterMinigameGO;

    void InvertInteractability(Button[] buttons)
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
        //sets herb jars disabled
        InvertInteractability(herbChoices);

        boilingWaterMinigameGO.SetActive(true);
    }
}
