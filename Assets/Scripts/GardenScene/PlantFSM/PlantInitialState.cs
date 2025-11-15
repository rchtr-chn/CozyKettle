using UnityEngine;

public class PlantInitialState : PlantBaseState
{
    public PlantInitialState()
    {
        ID = "Initial";
    }
    public override void EnterState(PlantStateManager plant)
    {
        if(plant.SeedData != null && plant.IsWatered)
        {
            plant.SwitchState(plant.GrowingState);
        }
    }
    public override void UpdateState(PlantStateManager plant)
    {
        if(plant.SeedData != null)
        {
            if(plant.IsWatered)
            {
                plant.CanBeWatered = false;
                plant.GrowthCountdown = plant.SeedData.growthTime;
            }
        }
    }
}
