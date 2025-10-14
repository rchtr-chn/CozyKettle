using System.Collections.Generic;
using UnityEngine;

public class ShopScreen : MonoBehaviour
{
    [SerializeField] private List<ShopProduct> _products; // assign in inspector if null
    [SerializeField] private GameObject _productContentPrefab; // assign in inspector
    [SerializeField] private GameObject _contentParent; // assign in inspector

    void Start()
    {
        if(_products == null || _products.Count == 0)
        {
            _products = new List<ShopProduct>(Resources.LoadAll<ShopProduct>("OBJs/ShopProducts"));
        }
        foreach (var product in _products)
        {
            GameObject productContent = Instantiate(_productContentPrefab, _contentParent.transform);
            ShopProductContent prod = productContent.GetComponent<ShopProductContent>();
            prod.shopProductSO = product;
            prod.InitializeVisuals();
        }
    }
}
