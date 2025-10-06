using UnityEngine;
using UnityEngine.EventSystems;

public class FrenchPressHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private FrenchPressMinigame _minigameHandler;
    [SerializeField] private RectTransform _presserRTransform;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!_minigameHandler.FullyPressed)
        {
            _presserRTransform.anchoredPosition -= new Vector2(0, 20f);
        }
    }
}
