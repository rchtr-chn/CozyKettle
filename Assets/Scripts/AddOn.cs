using UnityEngine;

[CreateAssetMenu(fileName = "New AddOn", menuName = "Items/AddOn")]
public class AddOn : ScriptableObject
{
    public string AddOnName;
    public string AddOnDescription;
    public Sprite AddOnSprite;
    public TasteProfile AddOnTasteProfile;
}
