using System.Collections;
using UnityEngine;

public class FrenchPressMinigame : MonoBehaviour
{
    [SerializeField] private BrewingStationManager _brewingStationManager;
    public bool FullyPressed = false;
    [SerializeField] RectTransform _presser;
    private Vector2 _initialPosition = new Vector2(0, 175f);
    private Vector2 _finishedPosition = new Vector2(0, -50f);
    Coroutine _pressCoroutine;

    private void OnEnable()
    {
        _presser.anchoredPosition = _initialPosition;
        FullyPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_presser.anchoredPosition.y < _initialPosition.y && !FullyPressed)
        {
            _presser.anchoredPosition += new Vector2(0, 5f) * Time.deltaTime;
        }
        if (_presser.anchoredPosition.y <= _finishedPosition.y && !FullyPressed)
        {
            FullyPressed = true;
            if (_pressCoroutine == null)
            {
                _pressCoroutine = StartCoroutine(WaitAndEndMinigame());
            }
        }
    }

    IEnumerator WaitAndEndMinigame()
    {
        yield return new WaitForSeconds(2f);
        _pressCoroutine = null;
        this.gameObject.SetActive(false);

        _brewingStationManager.CreateBeverage();
    }
}
