using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MatchaWhisk : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _matchaBaseRT; // Assign in inspector

    private void Start()
    {
        if (_rectTransform == null)
        {
            _rectTransform = GetComponent<RectTransform>();
        }
        if (_canvas == null)
        {
            _canvas = GetComponentInParent<Canvas>();
        }
        if (_matchaBaseRT == null)
        {
            Debug.LogError("Matcha Base RectTransform is not assigned.");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        ClampToMatchaBase();
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    private void ClampToMatchaBase()
    {
        float radius = _matchaBaseRT.rect.width * 0.35f; // Assuming the base is a circle
        Vector2 center = _matchaBaseRT.anchoredPosition;
        Vector2 offset = _rectTransform.anchoredPosition - center;
        if (offset.magnitude > radius)
        {
            offset = offset.normalized * radius;
            _rectTransform.anchoredPosition = center + offset;
        }
    }
}
