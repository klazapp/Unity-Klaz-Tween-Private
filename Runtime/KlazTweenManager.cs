using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Jobs;
using Unity.Mathematics;

public class KlazTweenManager : MonoBehaviour
{
    private List<IKlazTween> floatTweens = new List<IKlazTween>();
    private List<IKlazTween> float3Tweens = new List<IKlazTween>();
    
    //Native arrays for float tween
    private KlazTweenNativeArrays<float> floatNativeArrays;
    private KlazTweenNativeArrays<float3> float3NativeArrays;
   
    private bool arraysInitialized = false;

    public bool useJobSystem;

    private void PostAwake()
    {
#if KLAZAPP_ENABLE_JOBSYSTEM
        
#endif
    }
    
    public void AddTween(IKlazTween tween)
    {
        if (tween is KlazTween<float>)
        {
            floatTweens.Add(tween);
        }
        else if (tween is KlazTween<float3>)
        {
            float3Tweens.Add(tween);
        }
        InitializeArrays();
    }

    void Update()
    {
        
#if KLAZAPP_ENABLE_JOBSYSTEM
        var totalTweenCount = GetTotalTweenCount();
        if(totalTweenCount >= 10)
        {
            useJobSystem = true;
            UpdateFloatTweensWithJobSystem();
            UpdateFloat3TweensWithJobSystem();
            ApplyTweenUpdates();
        }
        else
        {
            useJobSystem = false;
            UpdateTweensRegularly(floatTweens);
            UpdateTweensRegularly(float3Tweens);
        }
#else
        useJobSystem = false;
        UpdateTweensRegularly(floatTweens);
        UpdateTweensRegularly(float3Tweens);
#endif

        ProcessCompletedTweens(floatTweens);
        ProcessCompletedTweens(float3Tweens);
    }


    public void CreateJob(KlazTweenNativeArrays<float> klazTweenNativeArrays)
    {
        
    }
    
    private void UpdateTweensWithJobSystem<T>() where T : struct
    {
        //Create and schedule job
        var tweenJob = new KlazTweenFloatJob
        {
            currentValues = floatNativeArrays.currentValues,
            startValues = floatNativeArrays.startValues,
            endValues = floatNativeArrays.endValues,
            durations = floatNativeArrays.duration,
            startTimes = floatNativeArrays.startTime,
            isCompleted = floatNativeArrays.isCompleted,
            currentTime =  Time.time,
        };

        //Schedule and complete job
        var jobHandle = tweenJob.Schedule(floatTweens.Count, 64); // Adjust the second parameter as needed
        jobHandle.Complete();
    
        //Retrieve data from job system
        for (var i = 0; i < floatTweens.Count; i++)
        {
            if (floatTweens[i] is not KlazTween<float> tween)
                continue;

            var jobComponentData = (tweenJob.currentValues[i], tweenJob.startValues[i], tweenJob.endValues[i], tweenJob.durations[i], tweenJob.startTimes[i], tweenJob.isCompleted[i]);
            tween.RetrieveFromJob(jobComponentData);
        }
    }
     
    private void UpdateFloatTweensWithJobSystem()
    {
        //Create and schedule job
        var tweenJob = new KlazTweenFloatJob
        {
            currentValues = floatNativeArrays.currentValues,
            startValues = floatNativeArrays.startValues,
            endValues = floatNativeArrays.endValues,
            durations = floatNativeArrays.duration,
            startTimes = floatNativeArrays.startTime,
            isCompleted = floatNativeArrays.isCompleted,
            currentTime =  Time.time,
        };

        //Schedule and complete job
        var jobHandle = tweenJob.Schedule(floatTweens.Count, 64); // Adjust the second parameter as needed
        jobHandle.Complete();
    
        //Retrieve data from job system
        for (var i = 0; i < floatTweens.Count; i++)
        {
            if (floatTweens[i] is not KlazTween<float> tween)
                continue;

            var jobComponentData = (tweenJob.currentValues[i], tweenJob.startValues[i], tweenJob.endValues[i], tweenJob.durations[i], tweenJob.startTimes[i], tweenJob.isCompleted[i]);
            tween.RetrieveFromJob(jobComponentData);
        }
        // var jobHandle = tweenJob.Schedule(floatTweens.Count, 64);
        // jobHandle.Complete();
        //
        // for (var i = 0; i < floatTweens.Count; i++)
        // {
        //     floatCompletedFlags[i] = tweenJob.CompletedFlags[i];
        //     floatCurrentValues[i] = tweenJob.CurrentValues[i];
        //     floatStartTimes[i] = tweenJob.StartTimes[i];
        //     floatDurations[i] = tweenJob.Durations[i];
        //     floatEndValues[i] = tweenJob.EndValues[i];
        //     floatStartValues[i] = tweenJob.StartValues[i];
        // }
        //
        // for (var i = 0; i < floatTweens.Count; i++)
        // {
        //     // Assuming floatTweens[i] is of type KlazTween<float>
        //     if (floatTweens[i] is KlazTween<float> tween)
        //     {
        //         tween.UpdateCurrentValueFromJob(floatCurrentValues[i], floatCompletedFlags[i]);
        //     }
        // }
    }

    private void UpdateFloat3TweensWithJobSystem()
    {
        //Create and schedule job
        var tweenJob = new KlazTweenFloat3Job
        {
            currentValues = float3NativeArrays.currentValues,
            startValues = float3NativeArrays.startValues,
            endValues = float3NativeArrays.endValues,
            durations = float3NativeArrays.duration,
            startTimes = float3NativeArrays.startTime,
            isCompleted = float3NativeArrays.isCompleted,
            currentTime =  Time.time,
        };

        var jobHandle = tweenJob.Schedule(float3Tweens.Count, 64); // Adjust the second parameter as needed
        jobHandle.Complete();
    
        for (var i = 0; i < float3Tweens.Count; i++)
        {
            if (float3Tweens[i] is not KlazTween<float3> tween)
                continue;

            var jobComponentData = (tweenJob.currentValues[i], tweenJob.startValues[i], tweenJob.endValues[i], tweenJob.durations[i], tweenJob.startTimes[i], tweenJob.isCompleted[i]);
            tween.RetrieveFromJob(jobComponentData);
        }
        
        // for (var i = 0; i < float3Tweens.Count; i++)
        // {
        //     //Assuming floatTweens[i] is of type KlazTween<float>
        //     if (float3Tweens[i] is KlazTween<float3> tween)
        //     {
        //         tween.UpdateCurrentValueFromJob(float3CurrentValues[i], float3CompletedFlags[i]);
        //     }
        // }
    }


    private void InitializeArrays()
    {
        // Initialize arrays for float tweens
        InitializeTweens(floatTweens, floatNativeArrays);

        // Initialize arrays for Vector3 tweens
        InitializeTweens(float3Tweens, float3NativeArrays);
    }
    
    private static void InitializeTweens<T>(IReadOnlyList<IKlazTween> tweens, KlazTweenNativeArrays<T> nativeArrays) where T : struct
    {
        if (tweens.Count <= 0)
            return;

        nativeArrays.InitializeNativeArrays(tweens.Count);

        for (var i = 0; i < tweens.Count; i++)
        {
            if (tweens[i] is not KlazTween<T> tween)
                continue;

            var tweenComponentForJob = tween.PrepareForJob();
            nativeArrays.SetComponentForJobByIndex(tweenComponentForJob, i);
        }
    }
    
    // private void InitializeFloatTweens()
    // {
    //     if (floatTweens.Count <= 0)
    //         return;
    //     
    //     var length = floatTweens.Count;
    //     floatNativeArrays.InitializeNativeArrays(length);
    //
    //     for (var i = 0; i < length; i++)
    //     {
    //         if (floatTweens[i] is not KlazTween<float> tween) 
    //             continue;
    //         
    //         var tweenComponentForJob = tween.PrepareForJob();
    //         floatNativeArrays.SetComponentForJobByIndex(tweenComponentForJob, i);
    //     }
    // }
    //
    // private void InitializeFloat3Tweens()
    // {
    //     if (float3Tweens.Count <= 0)
    //         return;
    //     
    //     var length = float3Tweens.Count;
    //     float3NativeArrays.InitializeNativeArrays(length);
    //
    //     for (var i = 0; i < length; i++)
    //     {
    //         if (float3Tweens[i] is not KlazTween<float3> tween) 
    //             continue;
    //         
    //         var tweenComponentForJob = tween.PrepareForJob();
    //         float3NativeArrays.SetComponentForJobByIndex(tweenComponentForJob, i);
    //     }
    // }

    private void UpdateTweensRegularly(List<IKlazTween> tweens)
    {
        foreach (var tween in tweens)
        {
            tween.OnUpdate();
        }
    }

    private void ApplyTweenUpdates()
    {
        foreach (var tween in floatTweens)
        {
            if (!tween.IsCompleted)
            {
                tween.ApplyUpdate();
            }
        }

        foreach (var tween in float3Tweens)
        {
            if (!tween.IsCompleted)
            {
                tween.ApplyUpdate();
            }
        }
    }

    private void ProcessCompletedTweens(List<IKlazTween> tweens)
    {
        for (var i = tweens.Count - 1; i >= 0; i--)
        {
            if (tweens[i].IsCompleted)
            {
                tweens[i].InvokeComplete();
                tweens.RemoveAt(i);
            }
        }
    }

    private void DisposeArrays()
    {
        floatNativeArrays.DisposeNativeArrays();
        float3NativeArrays.DisposeNativeArrays();
    }

    private void OnDestroy()
    {
        DisposeArrays();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int GetTotalTweenCount()
    {
        var floatTweenCount = floatTweens.Count;
        var float3TweenCount = float3Tweens.Count;

        return floatTweenCount + float3TweenCount;
    }
}
