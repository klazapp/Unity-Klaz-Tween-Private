using System;
using UnityEngine;


//TODO : Add Delay
//TODO : Add Easing
//TODO : Add custom inspector
//TODO : Check performance on event and action callbacks
//TODO : Start, current, end should be generic to account for multiple different types of data to lerp
//TODO : Add other functionalities as required
public abstract class KlazTweenBase
{
    public float Duration { get; protected set; }
    public float StartTime { get; protected set; }
    public bool IsCompleted { get; protected set; }

    public abstract void OnUpdate();
    public abstract void ApplyUpdate();
    
    // public float CurrentValue { get; private set; }
    // public float StartValue;
    // public float EndValue;
    // public float Duration;
    // public float StartTime;
    // public bool IsCompleted;
    //
    // private Action<float> onUpdate;
    // public event TweenCallback OnStart;
    // public event TweenCallback OnComplete;
    //
    // public KlazTween(float startValue, float endValue, float duration, Action<float> onUpdate, TweenCallback onStart = null, TweenCallback onComplete = null)
    // {
    //     StartValue = startValue;
    //     EndValue = endValue;
    //     Duration = duration;
    //     this.onUpdate = onUpdate;
    //     OnStart = onStart;
    //     OnComplete = onComplete;
    //     StartTime = Time.time;
    //     IsCompleted = false;
    //     CurrentValue = startValue;
    //
    //     InvokeStart();
    // }
    //
    // public void Update()
    // {
    //     if (IsCompleted)
    //         return;
    //
    //     float currentTime = Time.time;
    //     float normalizedTime = (currentTime - StartTime) / Duration;
    //
    //     if (normalizedTime >= 1.0f)
    //     {
    //         normalizedTime = 1.0f;
    //         InvokeComplete();
    //     }
    //
    //     CurrentValue = Mathf.Lerp(StartValue, EndValue, normalizedTime);
    //     onUpdate?.Invoke(CurrentValue);
    // }
    //
    // public void ApplyUpdate(float value)
    // {
    //     onUpdate?.Invoke(value);
    // }
    //
    // private void InvokeStart()
    // {
    //     OnStart?.Invoke();
    // }
    //
    // public void InvokeComplete()
    // {
    //     if (IsCompleted) 
    //         return;
    //     
    //     OnComplete?.Invoke();
    //     IsCompleted = true;
    // }
}
