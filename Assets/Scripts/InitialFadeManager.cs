using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InitialFadeManager : MonoBehaviour
{
    [SerializeField] Image _fadeImage; // Assign in inspector

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1f); // Optional delay before starting fade-in

        float duration = 1f; // Duration of the fade
        float elapsed = 0f;
        Color color = _fadeImage.color;
        color.a = 1f;
        _fadeImage.color = color;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsed / duration);
            _fadeImage.color = color;
            yield return null;
        }
        color.a = 0f;
        _fadeImage.color = color;
    }

    private void Awake()
    {
        StartCoroutine(FadeIn());
    }
}
