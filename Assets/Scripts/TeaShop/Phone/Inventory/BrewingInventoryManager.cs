using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrewingInventoryManager : MonoBehaviour
{
    [SerializeField] private ItemSO[] _allItems; // assign in Inspector
    [SerializeField] private GameObject _inventoryItemPrefab; // assign in Inspector
    [SerializeField] private Transform _groupParent; // assign in Inspector

    [SerializeField] private Dictionary<ItemSO, BrewingStaticData.Items> _brewingItemToEnumMap = new Dictionary<ItemSO, BrewingStaticData.Items>();

    public InventoryItemDisplay[] ItemDisplays = null;

    private void Start()
    {
        _brewingItemToEnumMap = new Dictionary<ItemSO, BrewingStaticData.Items>()
        {
            { _allItems[0], BrewingStaticData.Items.BlackHerb },
            { _allItems[1], BrewingStaticData.Items.GreenHerb },
            { _allItems[2], BrewingStaticData.Items.MatchaHerb },
            { _allItems[3], BrewingStaticData.Items.OolongHerb },
            { _allItems[4], BrewingStaticData.Items.InstantBlack },
            { _allItems[5], BrewingStaticData.Items.InstantGreen },
            { _allItems[6], BrewingStaticData.Items.InstantMatcha },
            { _allItems[7], BrewingStaticData.Items.InstantOolong },
            { _allItems[8], BrewingStaticData.Items.Honey },
            { _allItems[9], BrewingStaticData.Items.Lemon },
            { _allItems[10], BrewingStaticData.Items.Milk },
            //{ _allItems[11], BrewingStaticData.Items.BlackSeed },
            //{ _allItems[12], BrewingStaticData.Items.GreenSeed },
            //{ _allItems[13], BrewingStaticData.Items.MatchaSeed },
            //{ _allItems[14], BrewingStaticData.Items.OolongSeed },
        };

        foreach (ItemSO var in _allItems)
        {
            CreateItemDataGrid(var);
        }
    }

    void CreateItemDataGrid(ItemSO itemData)
    {
        GameObject itemDataGrid = Instantiate(_inventoryItemPrefab, _groupParent);
        InventoryItemDisplay display = itemDataGrid.GetComponent<InventoryItemDisplay>();
        if (display != null)
        {
            if(ItemDisplays == null)
            {
                ItemDisplays = new InventoryItemDisplay[1];
                ItemDisplays[0] = display;
            }
            else
            {
                Array.Resize(ref ItemDisplays, ItemDisplays.Length + 1);
                ItemDisplays[ItemDisplays.Length - 1] = display;
            }
            display.itemData = _brewingItemToEnumMap[itemData];
            Sprite itemIcon = itemData.itemSprite;
            if (itemIcon != null)
            {
                display.SetIcon(itemIcon);
            }
            else
            {
                Debug.LogWarning("Item icon is null for " + itemData.ToString());
            }
            display.itemNameText.text = itemData.itemName;
            display.itemQtyText.text = BrewingStaticData.GetItemQuantity(display.itemData).ToString();
        }
    }

    public void UpdateQtyVisuals()
    {
        foreach (InventoryItemDisplay display in ItemDisplays)
        {
            display.itemQtyText.text = BrewingStaticData.GetItemQuantity(display.itemData).ToString();
        }
    }

    private void OnEnable()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.PhoneClickSFX);
    }

    private void OnDisable()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.PhoneClickSFX);
    }
}
