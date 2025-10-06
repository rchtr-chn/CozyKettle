using UnityEngine;

[CreateAssetMenu(fileName = "New Herb", menuName = "Items/Herbs")]
public class Herb : ScriptableObject
{
    public string HerbName;
    public string HerbDescription;
    public Sprite HerbSprite;
    public TasteProfile HerbTasteProfile;
}
