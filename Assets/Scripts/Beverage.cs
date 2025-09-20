using UnityEngine;

[CreateAssetMenu(fileName = "New Beverage", menuName = "Items/Beverages")]
public class Beverage : ScriptableObject
{
    public string itemName;
    public string itemDescription;
}
