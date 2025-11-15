using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeedDatabase", menuName = "Databases/SeedDatabase")]

public class SeedDatabase : ScriptableObject
{
    public List<Seed> Seeds;
    private Dictionary<string, Seed> _seedDictionary;

    public void Initialize()
    {
        _seedDictionary = new Dictionary<string, Seed>();
        foreach (var seed in Seeds)
        {
            _seedDictionary[seed.itemID] = seed;
        }
    }

    public Seed GetSeed(string id)
    {
        if (_seedDictionary != null && _seedDictionary.TryGetValue(id, out var seed))
        {
            return seed;
        }
        Debug.LogWarning($"Seed with ID {id} not found.");
        return null;
    }
}
