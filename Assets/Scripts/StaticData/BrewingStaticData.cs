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

    public static string GetHerbTasteProfile(Items herb)
    {
        switch (herb)
        {
            case Items.BlackHerb:
                return "Bold, Robust";
            case Items.GreenHerb:
                return "Soothing, Refreshing";
            case Items.MatchaHerb:
                return "Grassy, Earthy";
            case Items.OolongHerb:
                return "Floral, Fruity, Nutty";
            default:
                return "Unknown Herb";
        }
    }

    public static string GetIngredientIdealTemp(Items ingredient)
    {
        switch(ingredient)
        {
            case Items.BlackHerb:
                return "Very strong & hot";
            case Items.GreenHerb:
                return "Slightly light & cool";
            case Items.MatchaHerb:
                return "Balanced";
            case Items.OolongHerb:
                return "Slightly strong & hot";
            case Items.Honey:
                return "Warmer";
            case Items.Lemon:
                return "Much cooler";
            case Items.Milk:
                return "Slightly cooler";
            default:
                return "Unknown Ingredient";
        }
    }

    public static string GetAddonTasteProfile(Items addon)
    {
        switch (addon)
        {
            case Items.Honey:
                return "Sweet, Floral, Rich";
            case Items.Lemon:
                return "Citrusy, Tangy, Zesty";
            case Items.Milk:
                return "Creamy, Smooth, Mild";
            default:
                return "Unknown Addon";
        }
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
