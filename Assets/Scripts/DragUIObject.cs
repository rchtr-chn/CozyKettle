using UnityEngine;
using UnityEngine.EventSystems;

public class DragUIObject : MonoBehaviour, IDragHandler
{
    public RectTransform rectTransform;
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
