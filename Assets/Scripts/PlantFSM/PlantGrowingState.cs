using UnityEngine;

public class PlantGrowingState : PlantBaseState
{
    public override void EnterState(PlantStateManager plant)
    {
        //checks if plant is watered yesterday
        if(plant.IsWatered)
        {
            plant.GrowthCountdown -= 1;
            plant.IsWatered = false;
        }

        if (plant.GrowthCountdown <= 0)
        {
            plant.SwitchState(plant.ReadyState);
        }
    }
    public override void UpdateState(PlantStateManager plant)
    {
        //checks if plant is watered today
        if (plant.IsWatered)
        {
            plant.CanBeWatered = false;
        }
    }
}
