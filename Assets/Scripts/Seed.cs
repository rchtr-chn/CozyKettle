using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "Items/Seed")]
public class Seed : ScriptableObject
{
    public string seedName;
    public Sprite seedIcon;
    public int growthTime;

    public Sprite GrowingPhaseOne;
    public Sprite GrowingPhaseTwo;
    public Sprite GrowingPhaseThree;
}
