using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class PlantStaticData
{
    public static List<PlantBaseState> PlantStates;
    public static List<Seed> SeedTypes;
    public static bool[] PreviouslyWatered;
    public static int[] GrowthCountdowns;
}
