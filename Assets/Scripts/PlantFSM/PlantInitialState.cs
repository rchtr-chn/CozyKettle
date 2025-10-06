using UnityEngine;

public class PlantInitialState : PlantBaseState
{
    public override void EnterState(PlantStateManager plant)
    {
        throw new System.NotImplementedException();
    }
    public override void UpdateState(PlantStateManager plant)
    {
        if(plant.SeedData != null)
        {
            plant.GrowthCountdown = plant.SeedData.growthTime;
            if(plant.IsWatered)
            {
                plant.IsWatered = true;
                plant.CanBeWatered = false;
                plant.SwitchState(plant.GrowingState);
            }
        }
    }
}
