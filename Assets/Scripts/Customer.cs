using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Customer : MonoBehaviour, IDropHandler
{
    public CustomerManager CustomerManager;
    public Beverage BeverageRequested;
    [SerializeField] private GameObject _speechBubble;
    public string SpeechBubbleText;
    private GameObject _currentSpeechBubble;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject givenObj = eventData.pointerDrag;
        if (givenObj != null)
        {
            BeverageDisplay givenBeverage = givenObj.GetComponent<BeverageDisplay>();
            if (givenBeverage != null && CompareBeverages(BeverageRequested, givenBeverage.BeverageSO))
            {
                Destroy(eventData.pointerDrag);
                if (_currentSpeechBubble != null)
                {
                    Destroy(_currentSpeechBubble);
                }
                StartCoroutine(CustomerManager.WalkUpAndLeave(gameObject));
            }
        }
    }

    public void PromptRequest()
    {
        GameObject obj = Instantiate(_speechBubble, gameObject.transform);
        TMP_Text text = obj.GetComponentInChildren<TMP_Text>();
        text.text = SpeechBubbleText;

        _currentSpeechBubble = obj;
    }

    bool CompareBeverages(Beverage requested, Beverage given)
    {
        if(requested.BeverageName == given.BeverageName)
        {
            return true;
        }
        return false;
    }
}
