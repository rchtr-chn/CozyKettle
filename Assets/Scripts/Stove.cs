using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stove : MonoBehaviour, IDropHandler
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] BrewingStationManager brewingStationManager;
    Vector2 offsetPos = new Vector2(0f, 150f);

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        //brewingStationManager = GetComponentInParent<BrewingStationManager>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Kettle kettle = eventData.pointerDrag.GetComponent<Kettle>();
            if (kettle != null)
            {
                kettle.transform.SetParent(this.transform);

                rectTransform.SetAsLastSibling();

                RectTransform kettleTransform = kettle.GetComponent<RectTransform>();
                kettleTransform.anchoredPosition = offsetPos;

                kettle.latestLegalPosition = kettleTransform.anchoredPosition;

                if(kettle.isFilled)
                {
                    StartCoroutine(InitiateMinigame());
                }
            }
        }
    }

    IEnumerator InitiateMinigame()
    {
        yield return new WaitForSeconds(0.5f);
        brewingStationManager.StartBoilingMinigame();
    }
}
