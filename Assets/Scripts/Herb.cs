using UnityEngine;

[CreateAssetMenu(fileName = "New Herb", menuName = "Item/Herb")]
public class Herb : ItemSO
{
    public bool IsInstant;
    public TasteProfile herbTasteProfile;
}