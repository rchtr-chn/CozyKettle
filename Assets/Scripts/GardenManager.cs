using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
    [SerializeField] private List<PlantStateManager> _pots;

    private void Start()
    {
        if(PlantStaticData.PlantStates == null)
        {
            foreach (var pot in _pots)
            {
                pot.CurrentState = pot.InitialState;
                pot.CurrentState.EnterState(pot);
            }
        }
        else
        {
            //initialize pots with existing static data
            for (int i = 0; i < _pots.Count; i++)
            {
                _pots[i].CurrentState = PlantStaticData.PlantStates[i];
                _pots[i].SeedData = PlantStaticData.SeedTypes[i];
                _pots[i].IsWatered = PlantStaticData.PreviouslyWatered[i];
                _pots[i].GrowthCountdown = PlantStaticData.GrowthCountdowns[i];

                _pots[i].CurrentState.EnterState(_pots[i]);
            }
        }
    }

    public void SaveGardenData()
    {
        PlantStaticData.PlantStates = new List<PlantBaseState>();
        PlantStaticData.SeedTypes = new List<Seed>();
        PlantStaticData.PreviouslyWatered = new bool[_pots.Count];
        PlantStaticData.GrowthCountdowns = new int[_pots.Count];

        for (int i = 0; i < _pots.Count; i++)
        {
            PlantStaticData.PlantStates.Add(_pots[i].CurrentState);
            PlantStaticData.SeedTypes.Add(_pots[i].SeedData);
            PlantStaticData.PreviouslyWatered[i] = _pots[i].IsWatered;
            PlantStaticData.GrowthCountdowns[i] = _pots[i].GrowthCountdown;
        }
    }
}
