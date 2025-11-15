using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _featuredImage; //assign in inspector
    [SerializeField] private GameObject _featuredName; //assign in inspector
    [SerializeField] private GameObject _featuredDesc; //assign in inspector

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _featuredImage.SetActive(false);
        _featuredName.SetActive(false);
        _featuredDesc.SetActive(false);
    }
}
