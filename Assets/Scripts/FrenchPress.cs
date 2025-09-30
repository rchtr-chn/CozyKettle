using UnityEngine;
using UnityEngine.EventSystems;

public class FrenchPress : MonoBehaviour, IDropHandler
{
    [SerializeField] BrewingStationManager brewingStationManager;
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            Kettle kettle = eventData.pointerDrag.GetComponent<Kettle>();
            if (kettle != null && kettle.isFilled)
            {
                kettle.isFilled = false;
                brewingStationManager.InvertInteractability(brewingStationManager.herbChoices);
            }
        }
    }
}
