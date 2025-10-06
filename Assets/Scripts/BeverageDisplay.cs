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
        _beverageName = BeverageSO.BeverageName;
        _beverageDescription = BeverageSO.BeverageDescription;
    }
}
