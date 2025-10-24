using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    [Header("Time Settings")]
    private float _timer = 470f;
    private readonly float _timerCap = 480f; // 8 minutes in seconds

    [Header("ScriptReferences")]
    [SerializeField] private CustomerManager _customerManager; // Assign in inspector

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
        }
        else if(!_endOfDayTriggered)
        {
            if(!_timeUpTriggered)
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
}
