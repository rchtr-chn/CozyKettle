using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BoilWaterMinigame : MonoBehaviour
{
    public Slider boilingWaterSlider;

    [SerializeField] float boilingPointMin = 0.7f;
    [SerializeField] float boilingPointMax = 0.9f;

    Coroutine BoilingPointCoroutine;

    [SerializeField] float onPointTimer = 2.0f;
    [SerializeField] float overPointTimer = 4.0f;
    bool isOverPoint = false;

    public bool timerReached = false;

    private void OnEnable()
    {
        boilingWaterSlider.value = 0f;
        timerReached = false;
        isOverPoint = false;
    }

    private void Update()
    {
        if(!timerReached)
        {
            CheckBoilTemp();
        }
    }

    void CheckBoilTemp()
    {
        //check if point is over min temp
        if (boilingWaterSlider.value >= boilingPointMin)
        {
            //if timer hasnt been initiated and point under max, start on point timer
            if (boilingWaterSlider.value <= boilingPointMax)
            {
                if (BoilingPointCoroutine == null)
                {
                    BoilingPointCoroutine = StartCoroutine(BoilingOnPoint(onPointTimer, isOverPoint));
                }
                else if (isOverPoint)
                {
                    isOverPoint = false;
                    StopCoroutine(BoilingPointCoroutine);
                    BoilingPointCoroutine = null;
                }
            }
            else if (boilingWaterSlider.value > boilingPointMax)
            {
                if(!isOverPoint && BoilingPointCoroutine != null)
                {
                    StopCoroutine(BoilingPointCoroutine);
                    BoilingPointCoroutine = null;

                    isOverPoint = true;
                    BoilingPointCoroutine = StartCoroutine(BoilingOnPoint(overPointTimer, isOverPoint));
                }
            }
        }
        else
        {
            //reset on point timer outside min zones
            if(BoilingPointCoroutine != null)
            {
                StopCoroutine(BoilingPointCoroutine);
                BoilingPointCoroutine = null;
            }
            Debug.Log("Cooking under boiling point");
        }
    }

    IEnumerator BoilingOnPoint(float duration, bool isOverPoint)
    {
        float timer = duration;

        while(timer > 0)
        {
            if(!isOverPoint)
            {
                Debug.Log("boiling on point");
            }
            else
            {
                Debug.Log("boiling over point");
            }

            timer -= Time.deltaTime;
            yield return null;
        }

        timerReached = true;
        BoilingPointCoroutine = null;
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }
}
