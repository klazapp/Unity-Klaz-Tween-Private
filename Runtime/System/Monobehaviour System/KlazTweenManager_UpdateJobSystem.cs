using System.Collections.Generic;
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
                currentValues = floatNativeArrays.currentValues,
                startValues = floatNativeArrays.startValues,
                endValues = floatNativeArrays.endValues,
                durations = floatNativeArrays.duration,
                startTimes = floatNativeArrays.startTime,
                isCompleted = floatNativeArrays.isCompleted,
                currentTime = Time.time,
                delays = floatNativeArrays.delays,
            };

            for (var i = 0; i < floatNativeArrays.isCompleted.Length; i++)
            {
                Debug.Log("pre updating i = " + i + ", is compelted = " + floatNativeArrays.isCompleted[i] + " , floatTweens.Count = " + floatTweens.Count);
            }
            //Schedule and complete job
            var jobHandle = tweenJob.Schedule(floatTweens.Count, 64);
            jobHandle.Complete();

            //Retrieve data from job system
            for (var i = 0; i < floatTweens.Count; i++)
            {
                if (floatTweens[i] is not KlazTween<float> tween)
                    continue;

                Debug.Log("float tween id = " + floatTweens[i].myId  + ", job scompelte= " + tweenJob.isCompleted[i] + ", job durat = " + tweenJob.durations[i]);
                var jobComponentData = (tweenJob.currentValues[i], tweenJob.startValues[i], tweenJob.endValues[i], tweenJob.durations[i], tweenJob.startTimes[i], tweenJob.isCompleted[i], tweenJob.delays[i]);
                tween.RetrieveFromJob(jobComponentData);
            }
            
            for (var i = 0; i < floatNativeArrays.isCompleted.Length; i++)
            {
                Debug.Log("post updating i = " + i + ", is compelted = " + floatNativeArrays.isCompleted[i] + " , floatTweens.Count = " + floatTweens.Count);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateFloat2TweensWithJobSystem()
        {
            //Create and schedule job
            var tweenJob = new KlazTweenFloat2JobSystem
            {
                currentValues = float2NativeArrays.currentValues,
                startValues = float2NativeArrays.startValues,
                endValues = float2NativeArrays.endValues,
                durations = float2NativeArrays.duration,
                startTimes = float2NativeArrays.startTime,
                isCompleted = float2NativeArrays.isCompleted,
                currentTime = Time.time,
                delays = float2NativeArrays.delays,
            };

            var jobHandle = tweenJob.Schedule(float2Tweens.Count, 64);
            jobHandle.Complete();

            for (var i = 0; i < float2Tweens.Count; i++)
            {
                if (float2Tweens[i] is not KlazTween<float2> tween)
                    continue;

                var jobComponentData = (tweenJob.currentValues[i], tweenJob.startValues[i], tweenJob.endValues[i], tweenJob.durations[i], tweenJob.startTimes[i], tweenJob.isCompleted[i], tweenJob.delays[i]);
                tween.RetrieveFromJob(jobComponentData);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateFloat3TweensWithJobSystem()
        {
            //Create and schedule job
            var tweenJob = new KlazTweenFloat3JobSystem
            {
                currentValues = float3NativeArrays.currentValues,
                startValues = float3NativeArrays.startValues,
                endValues = float3NativeArrays.endValues,
                durations = float3NativeArrays.duration,
                startTimes = float3NativeArrays.startTime,
                isCompleted = float3NativeArrays.isCompleted,
                currentTime = Time.time,
                delays = float3NativeArrays.delays,
            };

            var jobHandle = tweenJob.Schedule(float3Tweens.Count, 64);
            jobHandle.Complete();

            for (var i = 0; i < float3Tweens.Count; i++)
            {
                if (float3Tweens[i] is not KlazTween<float3> tween)
                    continue;

                var jobComponentData = (tweenJob.currentValues[i], tweenJob.startValues[i], tweenJob.endValues[i], tweenJob.durations[i], tweenJob.startTimes[i], tweenJob.isCompleted[i], tweenJob.delays[i]);
                tween.RetrieveFromJob(jobComponentData);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateFloat4TweensWithJobSystem()
        {
            //Create and schedule job
            var tweenJob = new KlazTweenFloat4JobSystem
            {
                currentValues = float4NativeArrays.currentValues,
                startValues = float4NativeArrays.startValues,
                endValues = float4NativeArrays.endValues,
                durations = float4NativeArrays.duration,
                startTimes = float4NativeArrays.startTime,
                isCompleted = float4NativeArrays.isCompleted,
                currentTime = Time.time,
                delays = float4NativeArrays.delays,
            };

            var jobHandle = tweenJob.Schedule(float4Tweens.Count, 64);
            jobHandle.Complete();

            for (var i = 0; i < float4Tweens.Count; i++)
            {
                if (float4Tweens[i] is not KlazTween<float4> tween)
                    continue;

                var jobComponentData = (tweenJob.currentValues[i], tweenJob.startValues[i], tweenJob.endValues[i], tweenJob.durations[i], tweenJob.startTimes[i], tweenJob.isCompleted[i], tweenJob.delays[i]);
                tween.RetrieveFromJob(jobComponentData);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateQuaternionTweensWithJobSystem()
        {
            //Create and schedule job
            var tweenJob = new KlazTweenQuaternionJobSystem
            {
                currentValues = quaternionNativeArrays.currentValues,
                startValues = quaternionNativeArrays.startValues,
                endValues = quaternionNativeArrays.endValues,
                durations = quaternionNativeArrays.duration,
                startTimes = quaternionNativeArrays.startTime,
                isCompleted = quaternionNativeArrays.isCompleted,
                currentTime = Time.time,
                delays = quaternionNativeArrays.delays,
            };

            var jobHandle = tweenJob.Schedule(quaternionTweens.Count, 64);
            jobHandle.Complete();

            for (var i = 0; i < quaternionTweens.Count; i++)
            {
                if (quaternionTweens[i] is not KlazTween<quaternion> tween)
                    continue;

                var jobComponentData = (tweenJob.currentValues[i], tweenJob.startValues[i], tweenJob.endValues[i], tweenJob.durations[i], tweenJob.startTimes[i], tweenJob.isCompleted[i], tweenJob.delays[i]);
                tween.RetrieveFromJob(jobComponentData);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateColorTweensWithJobSystem()
        {
            //Create and schedule job
            var tweenJob = new KlazTweenColorJobSystem
            {
                currentValues = colorNativeArrays.currentValues,
                startValues = colorNativeArrays.startValues,
                endValues = colorNativeArrays.endValues,
                durations = colorNativeArrays.duration,
                startTimes = colorNativeArrays.startTime,
                isCompleted = colorNativeArrays.isCompleted,
                currentTime = Time.time,
                delays = colorNativeArrays.delays,
            };

            var jobHandle = tweenJob.Schedule(colorTweens.Count, 64);
            jobHandle.Complete();

            for (var i = 0; i < colorTweens.Count; i++)
            {
                if (colorTweens[i] is not KlazTween<Color> tween)
                    continue;

                var jobComponentData = (tweenJob.currentValues[i], tweenJob.startValues[i], tweenJob.endValues[i], tweenJob.durations[i], tweenJob.startTimes[i], tweenJob.isCompleted[i], tweenJob.delays[i]);
                tween.RetrieveFromJob(jobComponentData);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateColor32TweensWithJobSystem()
        {
            //Create and schedule job
            var tweenJob = new KlazTweenColor32JobSystem
            {
                currentValues = color32NativeArrays.currentValues,
                startValues = color32NativeArrays.startValues,
                endValues = color32NativeArrays.endValues,
                durations = color32NativeArrays.duration,
                startTimes = color32NativeArrays.startTime,
                isCompleted = color32NativeArrays.isCompleted,
                currentTime = Time.time,
                delays = color32NativeArrays.delays,
            };

            var jobHandle = tweenJob.Schedule(color32Tweens.Count, 64);
            jobHandle.Complete();

            for (var i = 0; i < color32Tweens.Count; i++)
            {
                if (color32Tweens[i] is not KlazTween<Color32> tween)
                    continue;

                var jobComponentData = (tweenJob.currentValues[i], tweenJob.startValues[i], tweenJob.endValues[i], tweenJob.durations[i], tweenJob.startTimes[i], tweenJob.isCompleted[i], tweenJob.delays[i]);
                tween.RetrieveFromJob(jobComponentData);
            }
        }
        #endregion
    }
}