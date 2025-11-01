using UnityEngine;

[CreateAssetMenu(fileName = "New Herb", menuName = "Item/Herb")]
public class Herb : ItemSO
{
    public bool IsInstant;
    public TasteProfile HerbTasteProfile;
    public Vector2 IdealRange;
    public Color BrewColor;
}