using UnityEngine;
using UnityEngine.UI;

public class BeverageDisplay : MonoBehaviour
{
    public Beverage BeverageSO;
    private Image _beverageImage;
    private string _beverageName;
    private string _beverageDescription;

    private void Awake()
    {
        _beverageImage = GetComponent<Image>();
    }

    public void UpdateBeverageInfo()
    {
        _beverageImage.sprite = BeverageSO.BeverageSprite;

        float spriteWidth = BeverageSO.BeverageSprite.rect.width;
        float spriteHeight = BeverageSO.BeverageSprite.rect.height;
        _beverageImage.rectTransform.sizeDelta = new Vector2(spriteWidth, spriteHeight) / 6;

        _beverageName = BeverageSO.BeverageName;
        _beverageDescription = BeverageSO.BeverageDescription;
    }
}
