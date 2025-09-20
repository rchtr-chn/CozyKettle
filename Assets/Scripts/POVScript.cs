using System;
using System.Collections;
using UnityEngine;

public class POVScript : MonoBehaviour
{
    public RectTransform rectTransform;

    Vector3 defaultPos = new Vector3(0f, -420f, 0f);
    Vector3 brewingDeskPos = new Vector3(0f, 420f, 0f);
    public bool isLookingDown = false;

    Vector2 mousePos;

    Coroutine lerpCoroutine;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        CheckPlayerPOV();
    }

    private void CheckPlayerPOV()
    {
        mousePos = Input.mousePosition;

        if (mousePos.y < 100f && !isLookingDown && lerpCoroutine == null)
        {
            Debug.Log("Looking down!");
            lerpCoroutine = StartCoroutine(LerpPOV(brewingDeskPos));
        }
        else if (mousePos.y > 990f && isLookingDown && lerpCoroutine == null)
        {
            Debug.Log("Looking up!");
            lerpCoroutine = StartCoroutine(LerpPOV(defaultPos));
        }
    }

    IEnumerator LerpPOV(Vector3 target)
    {
        isLookingDown = !isLookingDown;

        float timer = 0f;
        float duration = 0.7f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, target, timer);
            yield return null;
        }
        lerpCoroutine = null;
    }
}
