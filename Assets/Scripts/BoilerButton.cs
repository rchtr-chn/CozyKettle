using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoilerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public BoilWaterMinigame MinigameScript;

    public Slider BoilingWaterSlider;
    private bool _isPressed = false;

    [SerializeField] private float _boilingPointGain = 0.4f;
    [SerializeField] private float _boilingPointLoss = 0.6f;
    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
    }

    private void Update()
    {
        if(!MinigameScript.TimerReached)
        {
            UpdateSliderValue();
        }
    }

    void UpdateSliderValue()
    {
        if (_isPressed)
        {
            BoilingWaterSlider.value += _boilingPointGain * Time.deltaTime;
        }
        else
        {
            BoilingWaterSlider.value -= _boilingPointLoss * Time.deltaTime;
        }

        BoilingWaterSlider.value = Mathf.Clamp01(BoilingWaterSlider.value);
    }
}
