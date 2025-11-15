using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [Header("Time Settings")]
    private float _timer = 0f;
    private readonly float _timerCap = 480f; // 8 minutes in seconds
    private float _startShift = 9f;
    private Color _timeUpColor = Color.red;

    [Header("ScriptReferences")]
    [SerializeField] private CustomerManager _customerManager; // Assign in inspector

    [Header("Component Properties")]
    [SerializeField] private Text _timerText; // Assign in inspector

    bool IsTimeUp => _timer >= _timerCap;

    public UnityEvent OnTimeUp;
    public UnityEvent OnEndOfDay;
    private bool _timeUpTriggered = false;
    private bool _endOfDayTriggered = false;

    void Update()
    {
        if (!IsTimeUp)
        {
            _timer += Time.deltaTime;
            _timerText.text = FormatTime(_timer);
        }
        else if (!_endOfDayTriggered)
        {
            if (!_timeUpTriggered)
            {
                _timeUpTriggered = true;
                OnTimeUp.Invoke();
            }

            if (_customerManager.AllSeatsEmpty() == true && IsTimeUp)
            {
                _endOfDayTriggered = true;
                // All customers have left, get summary list
                OnEndOfDay.Invoke();
            }
        }

    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        return string.Format("{0:00}:{1:00}", minutes + _startShift, seconds);
    }

    public void UpdateTextColor()
    {
        _timerText.color = _timeUpColor;
    }
}
