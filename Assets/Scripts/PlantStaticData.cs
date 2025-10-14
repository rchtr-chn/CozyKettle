using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class PlantStaticData
{
    public static List<PotData> Pots = new List<PotData>();
}

public class PotData
{
    public string StateID;
    public string SeedID;
    public bool IsWatered;
    public int GrowthCountdown;
}
