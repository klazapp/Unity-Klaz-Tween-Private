using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Jobs;
using Unity.Mathematics;

namespace com.Klazapp.Utility
{
    //[TodoHeader("Create high performance generic job system, which will eliminate complexities here")]
    public partial class KlazTweenManager
    {
        #region Modules
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateFloatTweensWithJobSystem()
        {
            //Create and schedule job
            var tweenJob = new KlazTweenFloatJobSystem
            {
                ids = floatNativeArrays.ids,
                currentValues = floatNativeArrays.currentValues,
                startValues = floatNativeArrays.startValues,
                endValues = floatNativeArrays.endValues,
                durations = floatNativeArrays.duration,
                startTimes = floatNativeArrays.startTime,
                isCompleted = floatNativeArrays.isCompleted,
                currentTime = Time.time,
                delays = floatNativeArrays.delays,
                easeTypes = floatNativeArrays.easeTypes,
            };

            //Schedule and complete job
            var jobHandle = tweenJob.Schedule(floatTweens.Count, 64);
            jobHandle.Complete();

            foreach (var (id, tween) in floatTweens)
            {
                if (tween is not KlazTween<float> klazTween)
                    continue;
                
                var tweenJobIds = tweenJob.ids;

                for (var i = 0; i < tweenJobIds.Length; i++)
                {
                    if (tweenJobIds[i] != id)
                        continue;
                    
                    var jobComponentData = (tweenJob.ids[i], tweenJob.currentValues[i], tweenJob.startValues[i], tweenJob.endValues[i], tweenJob.durations[i], tweenJob.startTimes[i], tweenJob.isCompleted[i], tweenJob.delays[i], tweenJob.easeTypes[i]);
                    klazTween.RetrieveFromJob(jobComponentData);
                    break;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateFloat2TweensWithJobSystem()
        {
            //Create and schedule job
            var tweenJob = new KlazTweenFloat2JobSystem
            {
                ids = float2NativeArrays.ids,
                currentValues = float2NativeArrays.currentValues,
                startValues = float2NativeArrays.startValues,
                endValues = float2NativeArrays.endValues,
                durations = float2NativeArrays.duration,
                startTimes = float2NativeArrays.startTime,
                isCompleted = float2NativeArrays.isCompleted,
                currentTime = Time.time,
                delays = float2NativeArrays.delays,
                easeTypes = float2NativeArrays.easeTypes,
            };

            var jobHandle = tweenJob.Schedule(float2Tweens.Count, 64);
            jobHandle.Complete();

            foreach (var (id, tween) in float2Tweens)
            {
                if (tween is not KlazTween<float2> klazTween)
                    continue;
                
                var tweenJobIds = tweenJob.ids;

                for (var i = 0; i < tweenJobIds.Length; i++)
                {
                    if (tweenJobIds[i] != id)
                        continue;
                    
                    var jobComponentData = (tweenJob.ids[i], tweenJob.currentValues[i], tweenJob.startValues[i], tweenJob.endValues[i], tweenJob.durations[i], tweenJob.startTimes[i], tweenJob.isCompleted[i], tweenJob.delays[i], tweenJob.easeTypes[i]);
                    klazTween.RetrieveFromJob(jobComponentData);
                    break;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateFloat3TweensWithJobSystem()
        {
            //Create and schedule job
            var tweenJob = new KlazTweenFloat3JobSystem
            {
                ids = float3NativeArrays.ids,
                currentValues = float3NativeArrays.currentValues,
                startValues = float3NativeArrays.startValues,
                endValues = float3NativeArrays.endValues,
                durations = float3NativeArrays.duration,
                startTimes = float3NativeArrays.startTime,
                isCompleted = float3NativeArrays.isCompleted,
                currentTime = Time.time,
                delays = float3NativeArrays.delays,
                easeTypes = float3NativeArrays.easeTypes,
            };

            var jobHandle = tweenJob.Schedule(float3Tweens.Count, 64);
            jobHandle.Complete();

            foreach (var (id, tween) in float3Tweens)
            {
                if (tween is not KlazTween<float3> klazTween)
                    continue;
                
                var tweenJobIds = tweenJob.ids;

                for (var i = 0; i < tweenJobIds.Length; i++)
                {
                    if (tweenJobIds[i] != id)
                        continue;
                    
                    var jobComponentData = (tweenJob.ids[i], tweenJob.currentValues[i], tweenJob.startValues[i], tweenJob.endValues[i], tweenJob.durations[i], tweenJob.startTimes[i], tweenJob.isCompleted[i], tweenJob.delays[i], tweenJob.easeTypes[i]);
                    klazTween.RetrieveFromJob(jobComponentData);
                    break;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateFloat4TweensWithJobSystem()
        {
            //Create and schedule job
            var tweenJob = new KlazTweenFloat4JobSystem
            {
                ids = float4NativeArrays.ids,
                currentValues = float4NativeArrays.currentValues,
                startValues = float4NativeArrays.startValues,
                endValues = float4NativeArrays.endValues,
                durations = float4NativeArrays.duration,
                startTimes = float4NativeArrays.startTime,
                isCompleted = float4NativeArrays.isCompleted,
                currentTime = Time.time,
                delays = float4NativeArrays.delays,
                easeTypes = float4NativeArrays.easeTypes,
            };

            var jobHandle = tweenJob.Schedule(float4Tweens.Count, 64);
            jobHandle.Complete();

            foreach (var (id, tween) in float4Tweens)
            {
                if (tween is not KlazTween<float4> klazTween)
                    continue;
                
                var tweenJobIds = tweenJob.ids;

                for (var i = 0; i < tweenJobIds.Length; i++)
                {
                    if (tweenJobIds[i] != id)
                        continue;
                    
                    var jobComponentData = (tweenJob.ids[i], tweenJob.currentValues[i], tweenJob.startValues[i], tweenJob.endValues[i], tweenJob.durations[i], tweenJob.startTimes[i], tweenJob.isCompleted[i], tweenJob.delays[i], tweenJob.easeTypes[i]);
                    klazTween.RetrieveFromJob(jobComponentData);
                    break;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateQuaternionTweensWithJobSystem()
        {
            //Create and schedule job
            var tweenJob = new KlazTweenQuaternionJobSystem
            {
                ids = quaternionNativeArrays.ids,
                currentValues = quaternionNativeArrays.currentValues,
                startValues = quaternionNativeArrays.startValues,
                endValues = quaternionNativeArrays.endValues,
                durations = quaternionNativeArrays.duration,
                startTimes = quaternionNativeArrays.startTime,
                isCompleted = quaternionNativeArrays.isCompleted,
                currentTime = Time.time,
                delays = quaternionNativeArrays.delays,
                easeTypes = quaternionNativeArrays.easeTypes,
            };

            var jobHandle = tweenJob.Schedule(quaternionTweens.Count, 64);
            jobHandle.Complete();

            foreach (var (id, tween) in quaternionTweens)
            {
                if (tween is not KlazTween<quaternion> klazTween)
                    continue;
                
                var tweenJobIds = tweenJob.ids;

                for (var i = 0; i < tweenJobIds.Length; i++)
                {
                    if (tweenJobIds[i] != id)
                        continue;
                    
                    var jobComponentData = (tweenJob.ids[i], tweenJob.currentValues[i], tweenJob.startValues[i], tweenJob.endValues[i], tweenJob.durations[i], tweenJob.startTimes[i], tweenJob.isCompleted[i], tweenJob.delays[i], tweenJob.easeTypes[i]);
                    klazTween.RetrieveFromJob(jobComponentData);
                    break;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateColor32TweensWithJobSystem()
        {
            //Create and schedule job
            var tweenJob = new KlazTweenColor32JobSystem
            {
                ids = color32NativeArrays.ids,
                currentValues = color32NativeArrays.currentValues,
                startValues = color32NativeArrays.startValues,
                endValues = color32NativeArrays.endValues,
                durations = color32NativeArrays.duration,
                startTimes = color32NativeArrays.startTime,
                isCompleted = color32NativeArrays.isCompleted,
                currentTime = Time.time,
                delays = color32NativeArrays.delays,
                easeTypes = color32NativeArrays.easeTypes,
            };

            var jobHandle = tweenJob.Schedule(color32Tweens.Count, 64);
            jobHandle.Complete();

            foreach (var (id, tween) in color32Tweens)
            {
                if (tween is not KlazTween<Color32> klazTween)
                    continue;
                
                var tweenJobIds = tweenJob.ids;

                for (var i = 0; i < tweenJobIds.Length; i++)
                {
                    if (tweenJobIds[i] != id)
                        continue;
                    
                    var jobComponentData = (tweenJob.ids[i], tweenJob.currentValues[i], tweenJob.startValues[i], tweenJob.endValues[i], tweenJob.durations[i], tweenJob.startTimes[i], tweenJob.isCompleted[i], tweenJob.delays[i], tweenJob.easeTypes[i]);
                    klazTween.RetrieveFromJob(jobComponentData);
                    break;
                }
            }
        }
        #endregion
        
        private void UpdateDouble2TweensWithJobSystem()
        {
            //Create and schedule job
            var tweenJob = new KlazTweenDouble2JobSystem
            {
                ids = double2NativeArrays.ids,
                currentValues = double2NativeArrays.currentValues,
                startValues = double2NativeArrays.startValues,
                endValues = double2NativeArrays.endValues,
                durations = double2NativeArrays.duration,
                startTimes = double2NativeArrays.startTime,
                isCompleted = double2NativeArrays.isCompleted,
                currentTime = Time.time,
                delays = double2NativeArrays.delays,
                easeTypes = double2NativeArrays.easeTypes,
            };

            //Schedule and complete job
            var jobHandle = tweenJob.Schedule(double2Tweens.Count, 64);
            jobHandle.Complete();

            foreach (var (id, tween) in double2Tweens)
            {
                if (tween is not KlazTween<double2> klazTween)
                    continue;
                
                var tweenJobIds = tweenJob.ids;

                for (var i = 0; i < tweenJobIds.Length; i++)
                {
                    if (tweenJobIds[i] != id)
                        continue;
                    
                    var jobComponentData = (tweenJob.ids[i], tweenJob.currentValues[i], tweenJob.startValues[i], tweenJob.endValues[i], tweenJob.durations[i], tweenJob.startTimes[i], tweenJob.isCompleted[i], tweenJob.delays[i], tweenJob.easeTypes[i]);
                    klazTween.RetrieveFromJob(jobComponentData);
                    break;
                }
            }
        }
    }
}