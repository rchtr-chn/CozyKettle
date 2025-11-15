using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDisplay : MonoBehaviour
{
    public Image itemIcon;
    public Text itemNameText;
    public Text itemQtyText;
    public BrewingStaticData.Items itemData;

    public void SetIcon(Sprite img)
    {
        AdjustImageComponentSize(img);
        itemIcon.sprite = img;
    }

    private void AdjustImageComponentSize(Sprite img)
    {
        float maxDimension = 75f; // Maximum width or height
        float width = img.rect.width;
        float height = img.rect.height;
        float scale = Mathf.Min(maxDimension / width, maxDimension / height, 1f);
        itemIcon.rectTransform.sizeDelta = new Vector2(width * scale, height * scale);
    }
}
