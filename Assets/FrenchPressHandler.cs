using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FrenchPressHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] FrenchPressMinigame minigameHandler;
    [SerializeField] RectTransform presserRTransform;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!minigameHandler.fullyPressed)
        {
            presserRTransform.anchoredPosition -= new Vector2(0, 20f);
        }
    }
}
