using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    // Assign these in inspector
    [Header("Script References")]
    [SerializeField] private OpenedBook book;
    [SerializeField] private Phone phone;
    [SerializeField] private BrewingPOVScript _brewingPOVScript;
    [SerializeField] private BrewingStationManager _brewingStationManager;
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private CustomerManager _customerManager;

    [Header("Object References")]
    [SerializeField] private Transform _canvas;
    [SerializeField] private Transform _middleSeatPos;
    [SerializeField] private GameObject _oldManGO;
    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private GameObject _promptUI;
    [SerializeField] private GameObject _intensitySelectMinigameGO;
    [SerializeField] private GameObject _frenchPressMinigameGO;
    [SerializeField] private GameObject _pourToCupMinigameGO;

    [Header("Settings")]
    private float _oldManSpeed = 2f;
    private Vector2 _promptTopPos = new Vector2(0f, 420f);
    private Vector2 _promptBottomPos = new Vector2(0f, -420f);

    Coroutine _textCoroutine;

    private bool _isNewGame = true;

    void Start()
    {
        if (_isNewGame)
        {
            ShowTutorial();
        }
    }

    private void ShowTutorial()
    {
        _timeManager.enabled = false;
        _customerManager.SetShopOpen(false);

        StartCoroutine(InitialTutorialDialogue());
    }

    IEnumerator InitialTutorialDialogue()
    {
        GameObject oldMan = Instantiate(_oldManGO, _customerManager.CustomerEntrancePos.position, Quaternion.identity, _middleSeatPos);
        while (Vector3.Distance(oldMan.transform.position, _middleSeatPos.position) > 0.1f)
        {
            oldMan.transform.position = Vector3.MoveTowards(oldMan.transform.position, _middleSeatPos.position, _oldManSpeed * 250f * Time.deltaTime);
            yield return null;
        }

        GameObject textBox = Instantiate(_dialogueUI, oldMan.transform);
        StartCoroutine(AnimateJump(oldMan.transform));
        TMP_Text text = textBox.GetComponentInChildren<TMP_Text>();

        _textCoroutine = StartCoroutine(DialogueTextAnimation("Welcome to the cafe! Let me show you how things work around here.", text));
        StartCoroutine(AnimateJump(oldMan.transform));

        yield return StartCoroutine(WaitForMouseClick());

        if (_textCoroutine != null)
        {
            StopCoroutine(_textCoroutine);
        }
        _textCoroutine = StartCoroutine(DialogueTextAnimation("Customers will come in and place their orders. Make sure to prepare their drinks just right!", text));
        StartCoroutine(AnimateJump(oldMan.transform));

        yield return StartCoroutine(WaitForMouseClick());

        if (_textCoroutine != null)
        {
            StopCoroutine(_textCoroutine);
        }
        _textCoroutine = StartCoroutine(DialogueTextAnimation("If you make a mistake, they might get upset and pay you less.", text));
        StartCoroutine(AnimateJump(oldMan.transform));

        yield return StartCoroutine(WaitForMouseClick());

        if (_textCoroutine != null)
        {
            StopCoroutine(_textCoroutine);
        }
        _textCoroutine = StartCoroutine(DialogueTextAnimation("This book should help you figure things out!", text));
        StartCoroutine(AnimateJump(oldMan.transform));

        yield return StartCoroutine(WaitForMouseClick());

        if (_textCoroutine != null)
        {
            StopCoroutine(_textCoroutine);
        }
        _textCoroutine = StartCoroutine(DialogueTextAnimation("Make sure to not lose it!", text));
        StartCoroutine(AnimateJump(oldMan.transform));

        yield return StartCoroutine(WaitForMouseClick());

        if (_textCoroutine != null)
        {
            StopCoroutine(_textCoroutine);
        }
        _textCoroutine = StartCoroutine(DialogueTextAnimation("It has been in the family for generations.", text));
        StartCoroutine(AnimateJump(oldMan.transform));

        yield return StartCoroutine(WaitForMouseClick());

        if (_textCoroutine != null)
        {
            StopCoroutine(_textCoroutine);
        }
        _textCoroutine = StartCoroutine(DialogueTextAnimation("Open the book if you're ever confused with the customer's requests.", text));
        StartCoroutine(AnimateJump(oldMan.transform));
        while (book.IsBookOpen == false)
            yield return null;

        GameObject prompt = Instantiate(_promptUI, _promptBottomPos, Quaternion.identity, _canvas);
        RectTransform promptRect = prompt.GetComponent<RectTransform>();
        TMP_Text promptText = prompt.GetComponentInChildren<TMP_Text>();
        promptText.text = "Click the book again to close it.";

        while (book.IsBookOpen == true)
            yield return null;

        prompt.SetActive(false);

        if (_textCoroutine != null)
        {
            StopCoroutine(_textCoroutine);
        }
        _textCoroutine = StartCoroutine(DialogueTextAnimation("Great! You're all set to start your day at the cafe.", text));
        StartCoroutine(AnimateJump(oldMan.transform));

        yield return StartCoroutine(WaitForMouseClick());

        if (_textCoroutine != null)
        {
            StopCoroutine(_textCoroutine);
        }
        _textCoroutine = StartCoroutine(DialogueTextAnimation("Anyways.. I gotta get going!", text));
        StartCoroutine(AnimateJump(oldMan.transform));

        yield return StartCoroutine(WaitForMouseClick());

        if (_textCoroutine != null)
        {
            StopCoroutine(_textCoroutine);
        }
        _textCoroutine = StartCoroutine(DialogueTextAnimation("Give me a call if you need any help.", text));
        StartCoroutine(AnimateJump(oldMan.transform));

        yield return StartCoroutine(WaitForMouseClick());

        if (_textCoroutine != null)
        {
            StopCoroutine(_textCoroutine);
        }
        _textCoroutine = StartCoroutine(DialogueTextAnimation("Good luck! and oh.. Relax!", text));
        StartCoroutine(AnimateJump(oldMan.transform));

        yield return StartCoroutine(WaitForMouseClick());

        while (Vector3.Distance(oldMan.transform.position, _customerManager.CustomerExitPos.position) > 0.1f)
        {
            oldMan.transform.position = Vector3.MoveTowards(oldMan.transform.position, _customerManager.CustomerExitPos.position, _oldManSpeed * 250f * Time.deltaTime);
            yield return null;
        }
        Destroy(oldMan);
        oldMan = null;
        textBox = null;
        text = null;

        prompt.SetActive(true);
        promptRect.anchoredPosition = _promptTopPos;
        promptText.text = "You can access your phone by hovering over and click it.";

        while(!phone.phoneOpened)
            yield return null;

        promptText.text = "This is where you can check your inventory, shop ingredients, and see how much money you got left.";
        while (phone.phoneOpened)
            yield return null;

        promptText.text = "Hover down the table to access your workstation.";
        while(!_brewingPOVScript.IsLookingDown)
            yield return null;

        promptRect.anchoredPosition = _promptBottomPos;
        promptText.text = "Use the tools here to brew the perfect drink for your customer!";

        yield return StartCoroutine(WaitForMouseClick());

        promptText.text = "Hover up the table to return to normal view.";
        while (_brewingPOVScript.IsLookingDown)
            yield return null;

        promptText.text = "Here comes your first customer! Prepare their drink according to their request.";

        yield return StartCoroutine(WaitForMouseClick());

        prompt.SetActive(false);

        Transform availableSeat = _customerManager.CheckForAvailableSeats();
        StartCoroutine(_customerManager.GetTutorialCustomer(availableSeat, 0f));
        while (!_brewingPOVScript.IsLookingDown)
            yield return null;

        prompt.SetActive(true);
        promptRect.anchoredPosition = _promptBottomPos;
        promptText.text = "First, choose a tea base to make by clicking the jar.";
        while(_brewingStationManager.SelectedHerb == null)
            yield return null;

        promptText.text = "Optionally, you could also choose an add-on if the customer requests it.";

        yield return StartCoroutine(WaitForMouseClick());

        promptText.text = "Next, fill the kettle by dragging it to the dispenser, and press START";
        while(_intensitySelectMinigameGO.activeInHierarchy == false)
            yield return null;

        promptText.text = "Finally, choose the brewing intensity based on the chosen ingredients, look up the journal book if you forgot!";
        while (_intensitySelectMinigameGO.activeInHierarchy == true)
            yield return null;

        prompt.SetActive(false);

        while (!_frenchPressMinigameGO.activeInHierarchy)
            yield return null;

        prompt.SetActive(true);
        promptText.text = "Press the french press' knob rapidly!";

        while (_frenchPressMinigameGO.activeInHierarchy)
            yield return null;

        promptText.text = "Now pour it to the cup by holding down.";

        while (_pourToCupMinigameGO.activeInHierarchy)
            yield return null;
        
        promptRect.anchoredPosition = _promptTopPos;
        promptText.text = "Great job! You've made a cup of tea. Now bring it to the customer!";

        while (_brewingPOVScript.IsLookingDown)
            yield return null;

        prompt.SetActive(false);

        while (availableSeat.childCount > 0)
            yield return null;

        prompt.SetActive(true);
        promptRect.anchoredPosition = _promptBottomPos;
        promptText.text = "You've completed the tutorial! From now on more customers will arrive until closing time (5:00 PM).";

        yield return StartCoroutine(WaitForMouseClick());

        prompt.SetActive(false);
        _timeManager.enabled = true;
        _customerManager.SetShopOpen(true);
        _isNewGame = false;
    }
    IEnumerator WaitForMouseClick()
    {
        while (Input.GetMouseButton(0))
            yield return null;
        while (!Input.GetMouseButtonDown(0))
            yield return null;
    }

    IEnumerator AnimateJump(Transform target)
    {
        Vector3 originalPos = target.position;
        Vector3 targetPos = originalPos + new Vector3(0f, 25f, 0f);
        float jumpDuration = 0.1f;
        float timer = 0f;
        while (timer < jumpDuration)
        {
            target.position = Vector3.Lerp(originalPos, targetPos, (timer / jumpDuration));
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0f;
        while (timer < jumpDuration)
        {
            target.position = Vector3.Lerp(targetPos, originalPos, (timer / jumpDuration));
            timer += Time.deltaTime;
            yield return null;
        }
        target.position = originalPos;
    }

    IEnumerator DialogueTextAnimation(string message, TMP_Text textComponent)
    {
        textComponent.text = "";
        foreach (char c in message)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
