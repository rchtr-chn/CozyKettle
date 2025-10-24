using System.Collections;
using UnityEngine;

public class FrenchPressMinigame : MonoBehaviour
{
    [SerializeField] private BrewingStationManager _brewingStationManager;
    public bool FullyPressed = false;
    public RectTransform PresserRT;
    public RectTransform FillingRT;
    private Vector2 _presserInitPos = new Vector2(0, 200f);
    private Vector2 _fillingInitPos = new Vector2(0, -225f);
    private Vector2 _pressedEndPos = new Vector2(0, -25f);
    public Vector2 _overlapPos = new Vector2(0, 75f);

    Coroutine _pressCoroutine;

    private void OnEnable()
    {
        PresserRT.anchoredPosition = _presserInitPos;
        FillingRT.anchoredPosition = _fillingInitPos;
        FullyPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PresserRT.anchoredPosition.y < _presserInitPos.y && !FullyPressed)
        {
            PresserRT.anchoredPosition += new Vector2(0, 5f) * Time.deltaTime;

            if(PresserRT.anchoredPosition.y < _overlapPos.y)
            {
                PresserRT.SetAsFirstSibling();
                FillingRT.anchoredPosition -= new Vector2(0, 5f) * Time.deltaTime;

            }
            else
            {
                PresserRT.SetAsLastSibling();
            }
        }
        if (PresserRT.anchoredPosition.y <= _pressedEndPos.y && !FullyPressed)
        {
            FullyPressed = true;
            if (_pressCoroutine == null)
            {
                _pressCoroutine = StartCoroutine(WaitAndEndMinigame());
            }
        }

        float presserClamp = Mathf.Clamp(PresserRT.anchoredPosition.y, _pressedEndPos.y, _presserInitPos.y);
        PresserRT.anchoredPosition = new Vector2(PresserRT.anchoredPosition.x, presserClamp);
    }

    IEnumerator WaitAndEndMinigame()
    {
        yield return new WaitForSeconds(2f);
        _pressCoroutine = null;
        this.gameObject.SetActive(false);

        _brewingStationManager.StartPourToCupMinigame();
    }
}
