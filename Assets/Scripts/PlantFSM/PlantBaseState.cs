using UnityEngine;

public abstract class PlantBaseState
{
    public string ID { get; protected set; }
    public abstract void EnterState(PlantStateManager plant);
    public abstract void UpdateState(PlantStateManager plant);
}
