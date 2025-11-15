using System.Collections.Generic;
using UnityEngine;

public class ShopScreen : MonoBehaviour
{
    [SerializeField] private SummaryManager _summaryManager; // assign in inspector
    [SerializeField] private List<ItemSO> _products; // assign in inspector if null
    [SerializeField] private GameObject _productContentPrefab; // assign in inspector
    [SerializeField] private GameObject _contentParent; // assign in inspector
    [SerializeField] private GameObject _moneyText; // assign in inspector
    [SerializeField] private Transform _moneyGroup; // assign in inspector

    private void Awake()
    {
        if (_products == null || _products.Count == 0)
        {
            _products = new List<ItemSO>(Resources.LoadAll<ItemSO>("OBJs/AddOns"));
            _products = new List<ItemSO>(Resources.LoadAll<ItemSO>("OBJs/InstantHerbs"));
            _products = new List<ItemSO>(Resources.LoadAll<ItemSO>("OBJs/Seeds"));
        }
    }

    void Start()
    {
        InitializePrefabs();
    }

    void InitializePrefabs()
    {
        foreach (var product in _products)
        {
            GameObject productContent = Instantiate(_productContentPrefab, _contentParent.transform);
            ShopProductContent prod = productContent.GetComponent<ShopProductContent>();
            prod.SummaryManager = _summaryManager;
            prod.MoneyText = _moneyText;
            prod.MoneyGroup = _moneyGroup;
            prod.shopProductSO = product;
            prod.InitializeVisuals();
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
