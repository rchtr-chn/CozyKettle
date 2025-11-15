using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "Items/Seed")]
public class Seed : ItemSO
{
    public int growthTime;

    public Sprite GrowingPhaseOne;
    public Sprite GrowingPhaseTwo;
    public Sprite GrowingPhaseThree;
}
