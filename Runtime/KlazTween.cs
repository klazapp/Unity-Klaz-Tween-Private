using System;
using Unity.Collections;
using UnityEngine;

public delegate void TweenCallback();

//TODO : Add Delay
//TODO : Add Easing
//TODO : Add custom inspector
//TODO : Check performance on event and action callbacks
//TODO : Start, current, end should be generic to account for multiple different types of data to lerp
//TODO : Add other functionalities as required
public class KlazTween<T> : IKlazTween, IKlazTweenJob<T> where T : struct
{
    public T currentValue;
    public T startValue;
    public T endValue;
    public float duration;
    public float startTime;

    private Action<T> onUpdate;
    public event TweenCallback OnStart;
    public event TweenCallback OnComplete;
    private Func<T, T, float, T> lerpFunc; // Function to interpolate between two values of type T

    public KlazTween(T startValue, T endValue, float duration, Action<T> onUpdate, Func<T, T, float, T> lerpFunc, TweenCallback onStart = null, TweenCallback onComplete = null)
    {
        this.startValue = startValue;
        this.endValue = endValue;
        this.duration = duration;
        this.onUpdate = onUpdate;
        this.lerpFunc = lerpFunc; // Interpolation function
        OnStart = onStart;
        OnComplete = onComplete;
        startTime = Time.time;
        IsStarted = false;
        IsCompleted = false;
        currentValue = startValue;

        InvokeStart();
    }
    
    public void OnUpdate()
    {
        if (IsCompleted)
            return;

        float currentTime = Time.time;
        float normalizedTime = (currentTime - startTime) / duration;

        if (normalizedTime >= 1.0f)
        {
            normalizedTime = 1.0f;
            InvokeComplete();
        }

        currentValue = lerpFunc(startValue, endValue, normalizedTime);
        onUpdate?.Invoke(currentValue);
    }
    
    public bool IsStarted { get; set; }
    public bool IsCompleted { get; set; }

    public void ApplyUpdate()
    {
        onUpdate?.Invoke(currentValue);
    }

    private void InvokeStart()
    {
        if (IsStarted)
            return;
        
        OnStart?.Invoke();
        IsStarted = true;
    }

    public void InvokeComplete()
    {
        if (IsCompleted) 
            return;
        
        OnComplete?.Invoke();
        IsCompleted = true;
    }
    
    // Func<float, float, float, float> lerpFloat = (start, end, t) => math.lerp(start, end, t);
    // Func<float3, float3, float, float3> lerpVector3 = (start, end, t) => math.lerp(start, end, t);
    
    //Job system
    public (T currentValue, T startValue, T endValue, float duration, float startTime, bool isCompleted) PrepareForJob()
    {
        return (currentValue, startValue, endValue, duration, startTime, IsCompleted);
    }

    public void RetrieveFromJob((T currentValue, T startValue, T endValue, float duration, float startTime, bool isCompleted) tweenJobComponent)
    {
        currentValue = tweenJobComponent.currentValue;
        startValue = tweenJobComponent.startValue;
        endValue = tweenJobComponent.endValue;
        duration = tweenJobComponent.duration;
        startTime = tweenJobComponent.startTime;
        IsCompleted = tweenJobComponent.isCompleted;
    }
    
    // public void UpdateCurrentValueFromJob<TValue>(TValue value, bool completed)
    // {
    //     if (value is T typedValue)
    //     {
    //         currentValue = typedValue;
    //
    //         if (completed)
    //         {
    //             InvokeComplete();
    //         }
    //     }
    //     else
    //     {
    //         throw new InvalidOperationException("Value type mismatch in UpdateCurrentValueFromJob");
    //     }
    // }
}
