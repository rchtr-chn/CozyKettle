using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    [Header("Book Item Data")]
    public List<ItemSO> Items; // Assign in Inspector
    public int CurrentIndex = 0;

    // Assign in Inspector
    [Header("Book UI Elements")]
    public GameObject OpenedBookUI;
    public Image[] ItemImage;
    public Text[] ItemNameText;
    public Text[] ItemDescriptionText;
    public Text[] StaticText;
    public Text[] ItemTasteProfile;
    public Text[] StaticIdealTempText;
    public Text[] IdealTempText;

    // Assign in Inspector
    [Header("Book Buttons")]
    public Button NextButton;
    public Button PreviousButton;

    public ItemSO[] GetCurrentItemPage()
    {
        if (Items.Count == 0) return null;
        if (Items.Count % 2 == 1 && CurrentIndex == Items.Count - 1)
        {
            return new ItemSO[] { Items[CurrentIndex], null };
        }
        else
        {
            return new ItemSO[] { Items[CurrentIndex], Items[CurrentIndex + 1] };
        }
    }

    public void UpdateUIElements(int index, ItemSO itemData)
    {
        if(itemData == null)
        {
            ItemImage[index].sprite = null;
            ItemImage[index].gameObject.SetActive(false);
            StaticText[index].text = "";
            ItemNameText[index].text = "";
            ItemDescriptionText[index].text = "";
            ItemTasteProfile[index].text = "";
            StaticIdealTempText[index].text = "";
            IdealTempText[index].text = "";
            return;
        }

        ItemImage[index].gameObject.SetActive(true);
        ItemImage[index].sprite = itemData.itemSprite;
        StaticText[index].text = "Taste profile: ";
        StaticIdealTempText[index].text = "Ideal intensity: ";
        float maxDimension = 200f; // Maximum width or height
        float width = itemData.itemSprite.rect.width;
        float height = itemData.itemSprite.rect.height;
        float scale = Mathf.Min(maxDimension / width, maxDimension / height, 1f);
        ItemImage[index].rectTransform.sizeDelta = new Vector2(width * scale, height * scale);

        ItemNameText[index].text = itemData.itemName;
        ItemDescriptionText[index].text = itemData.itemDescription;
        IdealTempText[index].text = BrewingStaticData.GetIngredientIdealTemp(itemData.item);

        if (itemData.itemType == ItemType.Herbs)
        {
            ItemTasteProfile[index].text = BrewingStaticData.GetHerbTasteProfile(itemData.item);
        }
        else if (itemData.itemType == ItemType.Addons)
        {
            ItemTasteProfile[index].text = BrewingStaticData.GetAddonTasteProfile(itemData.item);
        }
        else
        {
            ItemTasteProfile[index].text = "N/A";
        }
    }

    public void NextItem()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.PageFlipSFX);

        if (Items.Count == 0) return;
        CurrentIndex = (CurrentIndex + 2) % Items.Count;

        ItemSO[] currentPageItems = GetCurrentItemPage();
        for (int i = 0; i < 2; i++)
        {
            UpdateUIElements(i, currentPageItems[i]);
        }

    }

    public void PreviousItem()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.PageFlipSFX);

        if (Items.Count == 0) return;
        CurrentIndex = (CurrentIndex - 2 + Items.Count) % Items.Count;

        ItemSO[] currentPageItems = GetCurrentItemPage();
        for (int i = 0; i < 2; i++)
        {
            UpdateUIElements(i, currentPageItems[i]);
        }
    }

    public void InvertBookActivity()
    {
        OpenedBookUI.SetActive(!OpenedBookUI.activeSelf);
    }
}
