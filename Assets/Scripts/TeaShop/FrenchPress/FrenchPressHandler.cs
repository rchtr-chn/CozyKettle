using UnityEngine;
using UnityEngine.EventSystems;

public class FrenchPressHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private FrenchPressMinigame _minigameHandler; // assign in inspector

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!_minigameHandler.FullyPressed)
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.UIClickSFX);

            _minigameHandler.PresserRT.anchoredPosition -= new Vector2(0, 20f);

            if (_minigameHandler.PresserRT.anchoredPosition.y < _minigameHandler._overlapPos.y)
            {
                _minigameHandler.FillingRT.anchoredPosition += new Vector2(0, 20f);
            }
        }
    }
}
