using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BoilWaterMinigame : MonoBehaviour
{
    public Slider BoilingWaterSlider;

    [SerializeField] private float _boilingPointMin = 0.7f;
    [SerializeField] private float _boilingPointMax = 0.9f;

    Coroutine _boilingPointCoroutine;

    [SerializeField] private float _onPointTimer = 2.0f;
    [SerializeField] private float _overPointTimer = 4.0f;
    private bool _isOverPoint = false;

    public bool TimerReached = false;

    private void OnEnable()
    {
        BoilingWaterSlider.value = 0f;
        TimerReached = false;
        _isOverPoint = false;
    }

    private void Update()
    {
        if(!TimerReached)
        {
            CheckBoilTemp();
        }
    }

    void CheckBoilTemp()
    {
        //check if point is over min temp
        if (BoilingWaterSlider.value >= _boilingPointMin)
        {
            //if timer hasnt been initiated and point under max, start on point timer
            if (BoilingWaterSlider.value <= _boilingPointMax)
            {
                if (_boilingPointCoroutine == null)
                {
                    _boilingPointCoroutine = StartCoroutine(BoilingOnPoint(_onPointTimer, _isOverPoint));
                }
                else if (_isOverPoint)
                {
                    _isOverPoint = false;
                    StopCoroutine(_boilingPointCoroutine);
                    _boilingPointCoroutine = null;
                }
            }
            else if (BoilingWaterSlider.value > _boilingPointMax)
            {
                if(!_isOverPoint && _boilingPointCoroutine != null)
                {
                    StopCoroutine(_boilingPointCoroutine);
                    _boilingPointCoroutine = null;

                    _isOverPoint = true;
                    _boilingPointCoroutine = StartCoroutine(BoilingOnPoint(_overPointTimer, _isOverPoint));
                }
            }
        }
        else
        {
            //reset on point timer outside min zones
            if(_boilingPointCoroutine != null)
            {
                StopCoroutine(_boilingPointCoroutine);
                _boilingPointCoroutine = null;
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

        TimerReached = true;
        _boilingPointCoroutine = null;
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }
}
