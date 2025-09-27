using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrenchPressMinigame : MonoBehaviour
{
    public bool fullyPressed = false;
    [SerializeField] RectTransform presser;
    Vector2 initialPosition = new Vector2(0, 175f);
    Vector2 finishedPosition = new Vector2(0, -50f);
    Coroutine pressCoroutine;

    private void OnEnable()
    {
        presser.anchoredPosition = initialPosition;
        fullyPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (presser.anchoredPosition.y < initialPosition.y && !fullyPressed)
        {
            presser.anchoredPosition += new Vector2(0, 5f) * Time.deltaTime;
        }
        if (presser.anchoredPosition.y <= finishedPosition.y && !fullyPressed)
        {
            fullyPressed = true;
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
