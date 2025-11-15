using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GardenInventoryManager _inventoryManager; //assign in inspector
    [SerializeField] private Seed _seedType; //assign in inspector

    public void OnPointerClick(PointerEventData eventData)
    {
        _inventoryManager.DisplayFeatured(_seedType.itemSprite, _seedType.itemName, _seedType.itemDescription);
    }
}
