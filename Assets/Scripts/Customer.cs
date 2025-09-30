using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Customer : MonoBehaviour, IDropHandler
{
    [SerializeField] Beverage[] _availableBeverages;
    private Beverage _beverageRequested;

    private void Awake()
    {
        if( _availableBeverages != null)
        {
            _availableBeverages = Resources.LoadAll<Beverage>("OBJs/Beverages");
        }
    }
    private void Start()
    {
        int rand = Random.Range(0, _availableBeverages.Length - 1);
        _beverageRequested = _availableBeverages[rand];
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject givenObj = eventData.pointerDrag;
        if (givenObj != null)
        {
            BeverageDisplay givenBeverage = givenObj.GetComponent<BeverageDisplay>();
            if (givenBeverage != null && CompareBeverages(_beverageRequested, givenBeverage.BeverageSO))
            {
                Destroy(eventData.pointerDrag);
            }
        }
    }

    bool CompareBeverages(Beverage requested, Beverage given)
    {
        if(requested.itemName == given.itemName)
        {
            return true;
        }
        return false;
    }
}
