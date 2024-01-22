using System;
using UnityEngine;

public delegate void TweenCallback();

//TODO : Manager should be a singleton
//TODO : All current values should be clamped from start to end, particularly job system values
//TODO : Try to find a way to create a generic job system
//TODO : Use Job system should not be enabled if burst compile package is not installed
//TODO : BurstCompile function attribute should not be enabled if burst compile package is not installed
//TODO : Add Delay
//TODO : Add Easing
//TODO : Create custom inspector
//TODO : Check performance on event and action callbacks
//TODO : Start, current, end should be generic to account for multiple different types of data to lerp
//TODO : Add other functionalities as required
//TODO : Add namespace
public class KlazTween
{
    public float CurrentValue { get; private set; }
    public float StartValue;
    public float EndValue;
    public float Duration;
    public float StartTime;
    public bool IsCompleted;

    private Action<float> onUpdate;
    public event TweenCallback OnStart;
    public event TweenCallback OnComplete;

    public KlazTween(float startValue, float endValue, float duration, Action<float> onUpdate, TweenCallback onStart = null, TweenCallback onComplete = null)
    {
        StartValue = startValue;
        EndValue = endValue;
        Duration = duration;
        this.onUpdate = onUpdate;
        OnStart = onStart;
        OnComplete = onComplete;
        StartTime = Time.time;
        IsCompleted = false;
        CurrentValue = startValue;

        InvokeStart();
    }
    
    public void Update()
    {
        if (IsCompleted)
            return;

        float currentTime = Time.time;
        float normalizedTime = (currentTime - StartTime) / Duration;

        if (normalizedTime >= 1.0f)
        {
            normalizedTime = 1.0f;
            InvokeComplete();
        }

        CurrentValue = Mathf.Lerp(StartValue, EndValue, normalizedTime);
        onUpdate?.Invoke(CurrentValue);
    }

    public void ApplyUpdate(float value)
    {
        onUpdate?.Invoke(value);
    }

    private void InvokeStart()
    {
        OnStart?.Invoke();
    }

    public void InvokeComplete()
    {
        if (IsCompleted) 
            return;
        
        OnComplete?.Invoke();
        IsCompleted = true;
    }
}
