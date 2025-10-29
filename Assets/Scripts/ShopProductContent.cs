using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopProductContent : MonoBehaviour, IPointerClickHandler
{
    public SummaryManager SummaryManager;
    public GameObject MoneyText;
    public Transform MoneyGroup;
    public ItemSO shopProductSO;
    [SerializeField] private Image productImage; // assign in inspector
    [SerializeField] private Text productNameText; // assign in inspector
    [SerializeField] private Text productPriceText; // assign in inspector

    public void InitializeVisuals()
    {
        if (shopProductSO.itemSprite == null)
        {
            Debug.LogError("SO image not initialized.");
        }
        else
        {
            Sprite sprite = shopProductSO.itemSprite;
            AdjustImageComponentSize(sprite);
            productImage.sprite = sprite;
        }
        productNameText.text = shopProductSO.itemName;
        if(shopProductSO.itemType == ItemType.InstantHerbs)
        {
            productNameText.text += " (10 pcs)";
        }
        else if (shopProductSO.itemType == ItemType.Seeds)
        {
            productNameText.text += " (8 pcs)";
        }
        productPriceText.text = "$" + shopProductSO.itemPrice.ToString("F2");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            float playerMoney = PlayerStaticData.GetMoney();
            if (playerMoney >= shopProductSO.itemPrice)
            {
                CreateMoneyText(shopProductSO.itemPrice);

                SoundManager.Instance.PlaySFX(SoundManager.Instance.BuyItemSFX);

                PlayerStaticData.SubtractMoney(shopProductSO.itemPrice);
                if(shopProductSO.itemType == ItemType.InstantHerbs)
                {
                    BrewingStaticData.AddItemQuantity(shopProductSO.item, 10);
                }
                else if (shopProductSO.itemType == ItemType.Seeds)
                {
                    BrewingStaticData.AddItemQuantity(shopProductSO.item, 8);
                }
                else
                {
                    BrewingStaticData.AddItemQuantity(shopProductSO.item, 1);
                }

                SummaryManager.AddExpense(shopProductSO.itemPrice);
            }
            else
            {
                // Not enough money feedback can be implemented here
            }
        }
    }

    private void AdjustImageComponentSize(Sprite img)
    {
        float maxDimension = 75f; // Maximum width or height
        float width = img.rect.width;
        float height = img.rect.height;
        float scale = Mathf.Min(maxDimension / width, maxDimension / height, 1f);
        productImage.rectTransform.sizeDelta = new Vector2(width * scale, height * scale);
    }

    private void CreateMoneyText(float price)
    {
        GameObject obj = Instantiate(MoneyText, transform.position, Quaternion.identity, MoneyGroup);
        Text txt = obj.GetComponent<Text>();
        txt.text = "-$" + price.ToString("F2");
        txt.color = Color.red;
    }
}
