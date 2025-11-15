using UnityEngine;

public static class PlayerStaticData
{
    public static float money = 500f;

    public static float GetMoney()
    {
        return money;
    }

    public static void SetMoney(float amount)
    {
        money = amount;
    }

    public static void AddMoney(float amount)
    {
        money += amount;
    }

    public static void SubtractMoney(float amount)
    {
        money -= amount;
    }
}
