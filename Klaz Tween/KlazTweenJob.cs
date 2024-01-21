using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
public struct KlazTweenJob : IJobParallelFor
{
    public NativeArray<float> CurrentValues;
    [ReadOnly] public NativeArray<float> StartValues;
    [ReadOnly] public NativeArray<float> EndValues;
    [ReadOnly] public NativeArray<float> Durations;
    [ReadOnly] public NativeArray<float> StartTimes;
    public NativeArray<bool> CompletedFlags;
    public float CurrentTime;

    public void Execute(int index)
    {
        if (CompletedFlags[index])
            return;
        
        float progress = (CurrentTime - StartTimes[index]) / Durations[index];

        if (progress >= 1f)
        {
            CompletedFlags[index] = true;
        }
        
        CurrentValues[index] = math.lerp(StartValues[index], EndValues[index], progress);
    }
}
