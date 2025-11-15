using UnityEngine;
using UnityEngine.EventSystems;

public class SliderSoundHandler : MonoBehaviour, IPointerUpHandler
{
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Slider handle released");
        SoundManager.Instance.PlaySFX(SoundManager.Instance.UIClickSFX);
    }
}
