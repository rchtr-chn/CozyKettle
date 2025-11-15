using System.Collections.Generic;
using UnityEngine;

public static class PlantStateDictionary
{
    public static Dictionary<string, PlantBaseState> StateToString = new()
    {
        {"Initial", new PlantInitialState()},
        {"Growing", new PlantGrowingState()},
        {"Ready", new PlantReadyState()}
    };

    public static PlantBaseState GetStateByID(string id)
    {
        if (StateToString.TryGetValue(id, out var state))
        {
            return state;
        }
        Debug.LogError($"State with ID {id} not found.");
        return null;
    }
}
