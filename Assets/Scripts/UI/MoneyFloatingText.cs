using System.Collections;
using UnityEngine;

public class MoneyFloatingText : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(FloatAndDestroy());
    }

    IEnumerator FloatAndDestroy()
    {
        float floatDuration = 1f;
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;
        Vector3 targetPosition = startingPosition + new Vector3(0, 20f, 0);
        while (elapsedTime < floatDuration)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / floatDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
