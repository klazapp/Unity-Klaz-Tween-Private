#if KLAZAPP_ENABLE_JOBSYSTEM
using Unity.Burst;
#endif
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace com.Klazapp.Utility
{
#if KLAZAPP_ENABLE_JOBSYSTEM
    [BurstCompile]
#endif
    public struct KlazTweenFloatJobSystem : IJobParallelFor
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
        
        [ReadOnly] 
        public NativeArray<float> delays;

        [ReadOnly] 
        public float currentTime;

        public void Execute(int index)
        {
            if (isCompleted[index])
                return;

            var elapsedTime = currentTime - startTimes[index];
        
            //Delay not yet elapsed
            if (elapsedTime < delays[index])
                return;

            var progress = math.clamp((elapsedTime - delays[index]) / durations[index], 0f, 1f);

            isCompleted[index] = progress >= 1f;

            Debug.Log("index = " + index + ", elapsed time = " + elapsedTime + ", delays[index]= " + delays[index] + ", progress = " + progress + ", is compelted = " + isCompleted[index]);

            currentValues[index] = math.lerp(startValues[index], endValues[index], progress);
        }
    }
}