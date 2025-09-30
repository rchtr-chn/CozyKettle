using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Beverage", menuName = "Items/Beverages")]
public class Beverage : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
}
