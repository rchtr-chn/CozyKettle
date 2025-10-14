using UnityEngine;
using UnityEngine.UI;

public class ShopProductContent : MonoBehaviour
{
    public ShopProduct shopProductSO;
    [SerializeField] private Image productImage; // assign in inspector
    [SerializeField] private Text productNameText; // assign in inspector
    [SerializeField] private Text productPriceText; // assign in inspector

    public void InitializeVisuals()
    {
        if(shopProductSO.ProductImage == null)
        {
            Debug.LogError("SO image not initialized.");
        }
        else
        {
            productImage.sprite = shopProductSO.ProductImage;
        }
        productNameText.text = shopProductSO.ProductName;
        productPriceText.text = "$" + shopProductSO.Price.ToString() + ".00";
    }
}
