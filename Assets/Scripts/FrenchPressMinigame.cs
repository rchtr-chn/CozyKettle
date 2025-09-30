using System.Collections;
using UnityEngine;

public class FrenchPressMinigame : MonoBehaviour
{
    public bool FullyPressed = false;
    [SerializeField] GameObject _beverageOutput;
    [SerializeField] RectTransform _presser;
    Vector2 initialPosition = new Vector2(0, 175f);
    Vector2 finishedPosition = new Vector2(0, -50f);
    Coroutine pressCoroutine;

    private void OnEnable()
    {
        _presser.anchoredPosition = initialPosition;
        FullyPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_presser.anchoredPosition.y < initialPosition.y && !FullyPressed)
        {
            _presser.anchoredPosition += new Vector2(0, 5f) * Time.deltaTime;
        }
        if (_presser.anchoredPosition.y <= finishedPosition.y && !FullyPressed)
        {
            FullyPressed = true;
            if (pressCoroutine == null)
            {
                pressCoroutine = StartCoroutine(WaitAndEndMinigame());
            }
        }
    }

    IEnumerator WaitAndEndMinigame()
    {
        yield return new WaitForSeconds(2f);
        pressCoroutine = null;
        this.gameObject.SetActive(false);
    }
}
