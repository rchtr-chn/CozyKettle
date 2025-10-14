using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Product", menuName = "Shop Product")]
public class ShopProduct : ScriptableObject
{
    public string ProductName;
    public Sprite ProductImage;
    public int Price;
}
