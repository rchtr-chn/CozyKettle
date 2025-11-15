using UnityEngine;

public enum ItemType
{
    Herbs,
    Addons,
    Seeds,
    InstantHerbs,
}

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public string itemID;
    public ItemType itemType;
    public BrewingStaticData.Items item;
    public int itemPrice;
}
