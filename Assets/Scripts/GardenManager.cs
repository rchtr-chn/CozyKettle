using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
    [SerializeField] private SeedDatabase _seedDatabase; // assign in Inspector
    [SerializeField] private List<PlantStateManager> _pots;

    private void Start()
    {
        _seedDatabase.Initialize(); // build dictionary

        if (PlantStaticData.Pots == null || PlantStaticData.Pots.Count == 0)
        {
            // First-time init
            foreach (var pot in _pots)
            {
                pot.CurrentState = new PlantInitialState();
                pot.CurrentState.EnterState(pot);
            }
        }
        else
        {
            // Restore from static data
            for (int i = 0; i < _pots.Count; i++)
            {
                var data = PlantStaticData.Pots[i];

                // Restore state
                _pots[i].CurrentState = PlantStateDictionary.GetStateByID(data.StateID);

                // Restore seed
                _pots[i].SeedData = string.IsNullOrEmpty(data.SeedID) ? null : _seedDatabase.GetSeed(data.SeedID);

                _pots[i].IsWatered = data.IsWatered;
                _pots[i].GrowthCountdown = data.GrowthCountdown;

                _pots[i].CurrentState.EnterState(_pots[i]);
            }
        }
    }

    public void SavePotData()
    {
        PlantStaticData.Pots = new List<PotData>();

        foreach (var pot in _pots)
        {
            PlantStaticData.Pots.Add(new PotData
            {
                StateID = pot.CurrentState.ID,
                SeedID = pot.SeedData != null ? pot.SeedData.itemID : null,
                IsWatered = pot.IsWatered,
                GrowthCountdown = pot.GrowthCountdown
            });
        }
    }
}
