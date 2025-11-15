using UnityEngine;

[CreateAssetMenu(fileName = "New Beverage", menuName = "Items/Beverages")]
public class Beverage : ScriptableObject
{
    public string BeverageName;
    public string BeverageDescription;
    public Sprite BeverageSprite;
    public float BeverageCost;
    public TasteProfile[] BeverageTasteProfile;
}
