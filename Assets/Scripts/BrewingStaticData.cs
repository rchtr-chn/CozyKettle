using System.Collections.Generic;
using UnityEngine;

public static class BrewingStaticData
{
    public enum Items
    {
        BlackHerb,
        GreenHerb,
        MatchaHerb,
        OolongHerb,
        InstantBlack,
        InstantGreen,
        InstantMatcha,
        InstantOolong,
        Honey,
        Lemon,
        Milk,
        BlackSeed,
        GreenSeed,
        MatchaSeed,
        OolongSeed
    }

    public static Dictionary<Items, int> ItemQuantities = new Dictionary<Items, int>()
    {
        { Items.BlackHerb, 10},
        { Items.GreenHerb, 10},
        { Items.MatchaHerb, 10},
        { Items.OolongHerb, 10},
        { Items.InstantBlack, 10},
        { Items.InstantGreen, 10},
        { Items.InstantMatcha, 10},
        { Items.InstantOolong, 10},
        { Items.Honey, 10},
        { Items.Lemon, 10},
        { Items.Milk, 10},
        { Items.BlackSeed, 10},
        { Items.GreenSeed, 10},
        { Items.MatchaSeed, 10},
        { Items.OolongSeed, 10}
    };
    public static int GetItemQuantity(Items item)
    {
        return ItemQuantities[item];
    }

    public static void SetItemQuantity(Items item, int quantity)
    {
        ItemQuantities[item] = quantity;
    }

    public static void AddItemQuantity(Items item, int amount)
    {
        ItemQuantities[item] += amount;
    }

    public static void SubtractItemQuantity(Items item, int amount)
    {
        ItemQuantities[item] -= amount;
    }

    public static void DisplayAllItemQuantities()
    {
        foreach (var kvp in ItemQuantities)
        {
            Debug.Log($"{kvp.Key}: {kvp.Value}");
        }
    }
}
