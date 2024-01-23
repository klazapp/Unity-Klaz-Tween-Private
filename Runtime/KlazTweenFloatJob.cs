#if KLAZAPP_ENABLE_JOBSYSTEM
using Unity.Burst;
#endif
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

#if KLAZAPP_ENABLE_JOBSYSTEM
[BurstCompile]
#endif
public struct KlazTweenFloatJob : IJobParallelFor
{
    [WriteOnly]
    public NativeArray<float> currentValues;
    [ReadOnly] 
    public NativeArray<float> startValues;
    [ReadOnly] 
    public NativeArray<float> endValues;
    
    [ReadOnly]
    public NativeArray<float> durations;
    [ReadOnly] 
    public NativeArray<float> startTimes;
    public NativeArray<bool> isCompleted;
    public float currentTime;

    public void Execute(int index)
    {
        if (isCompleted[index])
            return;
        
        var progress = (currentTime - startTimes[index]) / durations[index];

        isCompleted[index] = progress >= 1f;

        currentValues[index] = math.lerp(startValues[index], endValues[index], progress);
    }
}
