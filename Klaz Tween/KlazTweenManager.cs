using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;

public class KlazTweenManager : MonoBehaviour
{
    private List<KlazTween> tweens = new List<KlazTween>();
    private NativeArray<float> startValues;
    private NativeArray<float> endValues;
    private NativeArray<float> durations;
    private NativeArray<float> startTimes;
    private NativeArray<float> currentValues;
    private NativeArray<bool> completedFlags;
    private bool arraysInitialized = false;

    public bool useJobSystem;
    
    public void AddTween(KlazTween tween)
    {
        tweens.Add(tween);
        InitializeArrays();
    }

    void Update()
    {
        if (useJobSystem)
        {
            UpdateTweensWithJobSystem();
            ApplyTweenUpdates();
        }
        else
        {
            UpdateTweensRegularly();
        }

        ProcessCompletedTweens();
    }

    private void InitializeArrays()
    {
        if (!arraysInitialized || startValues.Length < tweens.Count)
        {
            if (arraysInitialized)
            {
                DisposeArrays();
            }

            int length = tweens.Count;
            startValues = new NativeArray<float>(length, Allocator.Persistent);
            endValues = new NativeArray<float>(length, Allocator.Persistent);
            durations = new NativeArray<float>(length, Allocator.Persistent);
            startTimes = new NativeArray<float>(length, Allocator.Persistent);
            currentValues = new NativeArray<float>(length, Allocator.Persistent);
            completedFlags = new NativeArray<bool>(length, Allocator.Persistent);

            arraysInitialized = true;
        }

        for (int i = 0; i < tweens.Count; i++)
        {
            startValues[i] = tweens[i].StartValue;
            endValues[i] = tweens[i].EndValue;
            durations[i] = tweens[i].Duration;
            startTimes[i] = tweens[i].StartTime;
            completedFlags[i] = tweens[i].IsCompleted;
        }
    }

    private void UpdateTweensWithJobSystem()
    {
        KlazTweenJob tweenJob = new KlazTweenJob
        {
            StartValues = startValues,
            EndValues = endValues,
            Durations = durations,
            StartTimes = startTimes,
            CurrentValues = currentValues,
            CompletedFlags = completedFlags,
            CurrentTime = Time.time
        };

        JobHandle jobHandle = tweenJob.Schedule(tweens.Count, 64);
        jobHandle.Complete();

        for (int i = 0; i < tweens.Count; i++)
        {
            completedFlags[i] = tweenJob.CompletedFlags[i];
            currentValues[i] = tweenJob.CurrentValues[i];
        }
    }

    private void UpdateTweensRegularly()
    {
        foreach (var tween in tweens)
        {
            tween.Update();
        }
    }

    private void ApplyTweenUpdates()
    {
        for (int i = 0; i < tweens.Count; i++)
        {
            if (!tweens[i].IsCompleted)
            {
                tweens[i].ApplyUpdate(currentValues[i]);
            }
        }
    }

    private void ProcessCompletedTweens()
    {
        if (useJobSystem)
        {
            for (int i = tweens.Count - 1; i >= 0; i--)
            {
                if (completedFlags[i])
                {
                    tweens[i].InvokeComplete();
                    tweens.RemoveAt(i);
                }
            }
        }
        else
        {
            for (var i = tweens.Count - 1; i >= 0; i--)
            {
                if (tweens[i].IsCompleted)
                {
                    tweens.RemoveAt(i);
                }
            }
        }
    }

    private void DisposeArrays()
    {
        if (startValues.IsCreated) startValues.Dispose();
        if (endValues.IsCreated) endValues.Dispose();
        if (durations.IsCreated) durations.Dispose();
        if (startTimes.IsCreated) startTimes.Dispose();
        if (currentValues.IsCreated) currentValues.Dispose();
        if (completedFlags.IsCreated) completedFlags.Dispose();
    }

    void OnDestroy()
    {
        DisposeArrays();
    }
}
