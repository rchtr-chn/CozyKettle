using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Customer : MonoBehaviour, IDropHandler
{
    public CustomerManager CustomerManager;
    public Beverage BeverageRequested;
    [SerializeField] private GameObject _speechBubble;
    public string SpeechBubbleText;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject givenObj = eventData.pointerDrag;
        if (givenObj != null)
        {
            BeverageDisplay givenBeverage = givenObj.GetComponent<BeverageDisplay>();
            Debug.Log("Customer received a beverage: " + givenBeverage.BeverageSO.BeverageName);
            if (givenBeverage != null && CompareBeverages(BeverageRequested, givenBeverage.BeverageSO))
            {
                Debug.Log("Correct beverage given to customer!");
                Destroy(eventData.pointerDrag);
                StartCoroutine(CustomerManager.WalkUpAndLeave(gameObject));
            }
        }
    }

    public void PromptRequest()
    {
        GameObject obj = Instantiate(_speechBubble, gameObject.transform);
        TMP_Text text = obj.GetComponentInChildren<TMP_Text>();
        text.text = SpeechBubbleText;
    }

    bool CompareBeverages(Beverage requested, Beverage given)
    {
        if(requested.BeverageName == given.BeverageName)
        {
            Debug.Log("Beverages match: " + requested.BeverageName);
            return true;
        }
        Debug.Log("Beverages do not match. Requested: " + requested.BeverageName + ", Given: " + given.BeverageName);
        return false;
    }
}
