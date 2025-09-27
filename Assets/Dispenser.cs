using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dispenser : MonoBehaviour, IDropHandler
{
    [SerializeField] Vector2 offsetPos = new Vector2(0f, -110f);
    [SerializeField] RectTransform dispenserRectTransform;
    [SerializeField] float fillDuration = 2f;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Kettle kettle = eventData.pointerDrag.GetComponent<Kettle>();
            RectTransform kettleTransform = kettle.GetComponent<RectTransform>();
            if (kettle != null && !kettle.isFilled)
            {
                kettle.transform.SetParent(this.transform.parent);

                dispenserRectTransform.SetAsLastSibling();

                kettleTransform.anchoredPosition = offsetPos;

                kettle.latestLegalPosition = kettleTransform.anchoredPosition;
            }
            else
            {
                kettleTransform.anchoredPosition = kettle.latestLegalPosition;
            }
        }
    }

    public void FillKettle(Kettle kettle)
    {
        if (!kettle.isFilled && !kettle.dispenserLocked && kettle.transform.parent.name == this.transform.parent.name)
        {
            StartCoroutine(FillKettleCoroutine(kettle, fillDuration));
        }
    }

    IEnumerator FillKettleCoroutine(Kettle kettle, float fillDuration)
    {
        kettle.dispenserLocked = true;
        yield return new WaitForSeconds(fillDuration);
        kettle.isFilled = true;
        kettle.dispenserLocked = false;
    }
}
