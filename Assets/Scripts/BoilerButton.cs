using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoilerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public BoilWaterMinigame minigameScript;

    public Slider boilingWaterSlider;
    bool isPressed = false;

    [SerializeField] float boilingPointGain = 0.4f;
    [SerializeField] float boilingPointLoss = 0.6f;
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    private void Update()
    {
        if(!minigameScript.timerReached)
        {
            UpdateSliderValue();
        }
    }

    void UpdateSliderValue()
    {
        if (isPressed)
        {
            boilingWaterSlider.value += boilingPointGain * Time.deltaTime;
        }
        else
        {
            boilingWaterSlider.value -= boilingPointLoss * Time.deltaTime;
        }

        boilingWaterSlider.value = Mathf.Clamp01(boilingWaterSlider.value);
    }
}
