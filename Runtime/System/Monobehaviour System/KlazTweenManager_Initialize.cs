using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Mathematics;

namespace com.Klazapp.Utility
{
    public partial class KlazTweenManager : MonoSingletonGlobal<KlazTweenManager>
    {
        #region Modules
        //Gets lerp function by type T
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Func<T, T, float, T> GetLerpFuncByType<T>()
        {
            var typeFullName = typeof(T).FullName;

            return typeFullName switch
            {
                "System.Single" => (Func<T, T, float, T>)(Delegate)(Func<float, float, float, float>)KlazTweenLerp.Lerp,
                "Unity.Mathematics.float2" => (Func<T, T, float, T>)
                    (Delegate)(Func<float2, float2, float, float2>)KlazTweenLerp.Lerp,
                "Unity.Mathematics.float3" => (Func<T, T, float, T>)
                    (Delegate)(Func<float3, float3, float, float3>)KlazTweenLerp.Lerp,
                "Unity.Mathematics.float4" => (Func<T, T, float, T>)
                    (Delegate)(Func<float4, float4, float, float4>)KlazTweenLerp.Lerp,
                "Unity.Mathematics.double2" => (Func<T, T, float, T>)
                    (Delegate)(Func<double2, double2, float, double2>)KlazTweenLerp.Lerp,
                "Unity.Mathematics.quaternion" => (Func<T, T, float, T>)
                    (Delegate)(Func<quaternion, quaternion, float, quaternion>)KlazTweenLerp.Slerp,
                "UnityEngine.Color32" => (Func<T, T, float, T>)
                    (Delegate)(Func<Color32, Color32, float, Color32>)KlazTweenLerp.Lerp,
                _ => throw new InvalidOperationException($"No lerp function available for type {typeFullName}"),
            };
        }

        //Add tween to appropriate list
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddTween(IKlazTween tween)
        {
            switch (tween)
            {
                case KlazTween<float>:
                    floatTweens.Add(tweenId, tween);
                    break;
                case KlazTween<float2>:
                    float2Tweens.Add(tweenId, tween);
                    break;
                case KlazTween<float3>:
                    float3Tweens.Add(tweenId, tween);
                    break;
                case KlazTween<float4>:
                    float4Tweens.Add(tweenId, tween);
                    break;
                case KlazTween<quaternion>:
                    quaternionTweens.Add(tweenId, tween);
                    break;
                case KlazTween<Color32>:
                    color32Tweens.Add(tweenId, tween);
                    break;
                case KlazTween<double2>:
                    double2Tweens.Add(tweenId, tween);
                    break;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InitializeNativeArrays()
        {
            //Initialize arrays for float tweens
            InitializeNativeArrays(floatTweens, floatNativeArrays);

            //Initialize arrays for float2 tweens
            InitializeNativeArrays(float2Tweens, float2NativeArrays);
            
            //Initialize arrays for float3 tweens
            InitializeNativeArrays(float3Tweens, float3NativeArrays);
            
            //Initialize arrays for float4 tweens
            InitializeNativeArrays(float4Tweens, float4NativeArrays);
            
            //Initialize arrays for quaternion tweens
            InitializeNativeArrays(quaternionTweens, quaternionNativeArrays);
            
            //Initialize arrays for color32 tweens
            InitializeNativeArrays(color32Tweens, color32NativeArrays);
            
            //Initialize arrays for double2 tweens
            InitializeNativeArrays(double2Tweens, double2NativeArrays);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InitializeNativeArrays<T>(ICollection tweens, KlazTweenNativeArrays<T> nativeArrays) where T : struct
        {
            if (tweens.Count <= 0)
                return;

            nativeArrays.InitializeNativeArrays(tweens.Count);
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void PrepareTweenForJob()
        {
            //Prepare for job for float tweens
            PrepareTweenForJob(floatTweens, floatNativeArrays);

            //Prepare for job for float2 tweens
            PrepareTweenForJob(float2Tweens, float2NativeArrays);
            
            //Prepare for job for float3 tweens
            PrepareTweenForJob(float3Tweens, float3NativeArrays);
            
            //Prepare for job for float4 tweens
            PrepareTweenForJob(float4Tweens, float4NativeArrays);
            
            //Prepare for job for quaternion tweens
            PrepareTweenForJob(quaternionTweens, quaternionNativeArrays);
            
            //Prepare for job for color32 tweens
            PrepareTweenForJob(color32Tweens, color32NativeArrays);
            
            //Prepare for job for double2 tweens
            PrepareTweenForJob(double2Tweens, double2NativeArrays);
        }

        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // private static void PrepareTweenForJob<T>(List<IKlazTween> tweens, KlazTweenNativeArrays<T> nativeArrays) where T : struct
        // {
        //     if (tweens.Count <= 0)
        //         return;
        //
        //     for (var i = 0; i < tweens.Count; i++)
        //     {
        //         if (tweens[i] is not KlazTween<T> tween)
        //             continue;
        //
        //         var tweenComponentForJob = tween.PrepareForJob();
        //         nativeArrays.SetComponentForJobByIndex(tweenComponentForJob, i);
        //     }
        // }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PrepareTweenForJob<T>(Dictionary<int, IKlazTween> tweens, KlazTweenNativeArrays<T> nativeArrays) where T : struct
        {
            if (tweens.Count <= 0)
                return;
            
            var index = 0;
            foreach (var ikTween in tweens.Values)
            {
                if (ikTween is not KlazTween<T> tween) 
                    continue;
                
                var tweenComponentForJob = tween.PrepareForJob();
                nativeArrays.SetComponentForJobByIndex(tweenComponentForJob, index);

                index++;
            }
        }
        #endregion
    }
}