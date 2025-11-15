using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySeedIcon : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private GardenInventoryManager _inventoryManager; // Assign in inspector
    [SerializeField] private Seed _seedType; // Assign in inspector
    static Dictionary<string, int> seedIndex = new Dictionary<string, int>(); // Seed ID to index mapping
    private Image _slotImage;

    private Canvas _parentCanvas;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private Vector3 _originalPosition;

    public bool OutOfStock = false;

    private void Start()
    {
        seedIndex = new()
        {
            {"Black", 0},
            {"Green", 1},
            {"Matcha", 2},
            {"Oolong", 3}
        };

        _slotImage = GetComponent<Image>();

        if (_seedType.itemSprite != null && _seedType != null)
        {
            _slotImage.sprite = _seedType.itemSprite;
        }
        else
        {
            Debug.LogWarning("Slot Image or Seed Type is not assigned in the inspector." + _seedType.itemID);
        }

        _parentCanvas = GetComponentInParent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        _originalPosition = _rectTransform.anchoredPosition;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        _inventoryManager.DisplayFeatured(_seedType.itemSprite, _seedType.itemName, _seedType.itemDescription);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!OutOfStock)
            _rectTransform.anchoredPosition += eventData.delta / _parentCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        _rectTransform.anchoredPosition = _originalPosition;
    }

    public Seed GetSeedType()
    {
        int index = seedIndex.ContainsKey(_seedType.itemID) ? seedIndex[_seedType.itemID] : -1;
        _inventoryManager.SeedCounts[index]--;
        _inventoryManager.UpdateTextQuanitity();
        _inventoryManager.CheckQuantityForDim();
        return _seedType;
    }
}
