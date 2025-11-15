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
            if (kettle != null)
            {
                //brewingStationManager.InvertInteractability(brewingStationManager.herbChoices);
            }
        }
    }
}
