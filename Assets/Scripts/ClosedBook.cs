using UnityEngine;
using UnityEngine.EventSystems;

public class ClosedBook : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private BrewingPOVScript _brewingPOVScript; // assign in inspector

    public void OnPointerEnter(PointerEventData eventData)
    {
        _brewingPOVScript.SetIsHoveringUI(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _brewingPOVScript.SetIsHoveringUI(false);
    }
}
