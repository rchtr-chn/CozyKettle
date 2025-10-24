using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Customer : MonoBehaviour, IDropHandler
{
    [Header("Customer Info")]
    public CustomerManager CustomerManager;
    public Beverage BeverageRequested;
    [SerializeField] private GameObject _speechBubble;
    public string SpeechBubbleText;
    private GameObject _currentSpeechBubble;

    [Header("Sprites")]
    public Image CustomerImage; // assign in inspector
    public Sprite DefaultSprite;
    public Sprite UnsatisfiedSprite;

    [Header("SatisfactionMultiplier")]
    private float _satisfiedMultiplier = 1.2f;
    private float _unsatisfiedMultiplier = 0.8f;

    [Header("Summary Manager")]
    public SummaryManager SummaryManager;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject givenObj = eventData.pointerDrag;
        if (givenObj != null)
        {
            BeverageDisplay givenBeverage = givenObj.GetComponent<BeverageDisplay>();

            float finalBeverageCost = 0f;
            if (givenBeverage != null)
            {
                finalBeverageCost = givenBeverage.BeverageSO.BeverageCost;
            }

            bool isSatisfied = CompareBeverages(BeverageRequested, givenBeverage.BeverageSO);

            if (givenBeverage != null && isSatisfied)
            {
                finalBeverageCost *= _satisfiedMultiplier;
            }
            else if(givenBeverage != null && !isSatisfied)
            {
                CustomerImage.sprite = UnsatisfiedSprite;
                finalBeverageCost *= _unsatisfiedMultiplier;
            }

            SummaryManager.RecordCustomerServed(isSatisfied, finalBeverageCost, BeverageRequested.BeverageCost * _satisfiedMultiplier);

            Destroy(eventData.pointerDrag);
            PlayerStaticData.AddMoney(finalBeverageCost);
            if (_currentSpeechBubble != null)
            {
                Destroy(_currentSpeechBubble);
            }
            StartCoroutine(CustomerManager.WalkUpAndLeave(gameObject));
        }
    }

    public void PromptRequest()
    {
        StartCoroutine(AnimateJump());

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

    IEnumerator AnimateJump()
    {
        Vector3 originalPos = transform.position;
        Vector3 targetPos = originalPos + new Vector3(0f, 25f, 0f);
        float jumpDuration = 0.1f;
        float timer = 0f;
        while (timer < jumpDuration)
        {
            transform.position = Vector3.Lerp(originalPos, targetPos, (timer / jumpDuration));
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0f;
        while (timer < jumpDuration)
        {
            transform.position = Vector3.Lerp(targetPos, originalPos, (timer / jumpDuration));
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPos;
    }
}
