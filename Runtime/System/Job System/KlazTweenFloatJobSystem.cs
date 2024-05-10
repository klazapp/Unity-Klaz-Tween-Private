// #if KLAZAPP_ENABLE_JOBSYSTEM
// using Unity.Burst;
// #endif
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace com.Klazapp.Utility
{
// #if KLAZAPP_ENABLE_JOBSYSTEM
//     [BurstCompile]
// #endif
    public struct KlazTweenFloatJobSystem : IJobParallelFor
    {
        [ReadOnly] 
        public NativeArray<int> ids;
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
        public NativeArray<EaseType> easeTypes;
        
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

            var normalizedTime = math.clamp((elapsedTime - delays[index]) / durations[index], 0f, 1f);

            // Apply easing based on the easeType for this tween
            // Retrieve the ease type and apply easing function
            var easedProgress = Easing.SetEasingByEaseType(easeTypes[index], normalizedTime);

            isCompleted[index] = easedProgress >= 1f;

            //Use easedProgress instead of normalizedTime for the interpolation
            currentValues[index] = math.lerp(startValues[index], endValues[index], easedProgress);
        }
    }
}
