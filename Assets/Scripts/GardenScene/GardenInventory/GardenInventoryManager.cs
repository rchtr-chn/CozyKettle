using UnityEngine;
using UnityEngine.UI;

public class GardenInventoryManager : MonoBehaviour
{
    /*
         0 = Black Tea Seed
         1 = Green Tea Seed
         2 = Matcha Seed
         3 = Oolong Seed
    */
    public int[] SeedCounts = new int[4];

    [Header("Inventory Data Reference")]
    public GameObject[] SeedImage; // Assign in Inspector
    public GameObject[] SeedQuantity; // Assign in Inspector

    [Header("Inventory Item Showcase/Feature")]
    public Image FeaturedItemImage; // Assign in Inspector
    public Text FeaturedItemNameText; // Assign in Inspector
    public Text FeaturedItemDescText; // Assign in Inspector

    private void Start()
    {
        LoadQuantityData();
        UpdateTextQuanitity();
        CheckQuantityForDim();
    }

    //load seed quantity from InventoryData static class
    void LoadQuantityData()
    {
        SeedCounts[0] = BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.BlackSeed);
        SeedCounts[1] = BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.GreenSeed);
        SeedCounts[2] = BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.MatchaSeed);
        SeedCounts[3] = BrewingStaticData.GetItemQuantity(BrewingStaticData.Items.OolongSeed);
    }

    //update inventoryUI text quantity
    public void UpdateTextQuanitity()
    {
        SeedQuantity[0].GetComponent<Text>().text = SeedCounts[0].ToString();
        SeedQuantity[1].GetComponent<Text>().text = SeedCounts[1].ToString();
        SeedQuantity[2].GetComponent<Text>().text = SeedCounts[2].ToString();
        SeedQuantity[3].GetComponent<Text>().text = SeedCounts[3].ToString();
    }

    // Dim the slot image if the seed count is zero
    public void CheckQuantityForDim()
    {
        Image temp = SeedImage[0].GetComponent<Image>();
        if (SeedCounts[0] <= 0)
        {
            temp.color = new Color(temp.color.r, temp.color.g, temp.color.b, 0.5f);
            SeedImage[0].GetComponent<InventorySeedIcon>().OutOfStock = true;
        }

        temp = SeedImage[1].GetComponent<Image>();
        if (SeedCounts[1] <= 0)
        {
            temp.color = new Color(temp.color.r, temp.color.g, temp.color.b, 0.5f);
            SeedImage[1].GetComponent<InventorySeedIcon>().OutOfStock = true;
        }


        temp = SeedImage[2].GetComponent<Image>();
        if (SeedCounts[2] <= 0)
        {
            temp.color = new Color(temp.color.r, temp.color.g, temp.color.b, 0.5f);
            SeedImage[2].GetComponent<InventorySeedIcon>().OutOfStock = true;
        }

        temp = SeedImage[3].GetComponent<Image>();
        if (SeedCounts[3] <= 0)
        {
            temp.color = new Color(temp.color.r, temp.color.g, temp.color.b, 0.5f);
            SeedImage[3].GetComponent<InventorySeedIcon>().OutOfStock = true;
        }
    }

    public void SaveSeedData()
    {
        BrewingStaticData.SetItemQuantity(BrewingStaticData.Items.BlackSeed, SeedCounts[0]);
        BrewingStaticData.SetItemQuantity(BrewingStaticData.Items.GreenSeed, SeedCounts[1]);
        BrewingStaticData.SetItemQuantity(BrewingStaticData.Items.MatchaSeed, SeedCounts[2]);
        BrewingStaticData.SetItemQuantity(BrewingStaticData.Items.OolongSeed, SeedCounts[3]);
    }

    public void DisplayFeatured(Sprite img, string name, string desc)
    {
        if(FeaturedItemImage.gameObject.activeSelf == false)
            FeaturedItemImage.gameObject.SetActive(true);
        if (FeaturedItemNameText.gameObject.activeSelf == false)
            FeaturedItemNameText.gameObject.SetActive(true);
        if (FeaturedItemDescText.gameObject.activeSelf == false)
            FeaturedItemDescText.gameObject.SetActive(true);

        FeaturedItemImage.sprite = img;
        FeaturedItemNameText.text = name;
        FeaturedItemDescText.text = desc;
    }
}
