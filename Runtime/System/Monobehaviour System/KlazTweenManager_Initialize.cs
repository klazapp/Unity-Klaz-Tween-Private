using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Mathematics;

namespace com.Klazapp.Utility
{
    public partial class KlazTweenManager : MonoBehaviour
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
                "Unity.Mathematics.quaternion" => (Func<T, T, float, T>)
                    (Delegate)(Func<quaternion, quaternion, float, quaternion>)KlazTweenLerp.Slerp,
                "UnityEngine.Color32" => (Func<T, T, float, T>)
                    (Delegate)(Func<Color32, Color32, float, Color32>)KlazTweenLerp.Lerp,
                "UnityEngine.Color" => (Func<T, T, float, T>)(Delegate)(Func<Color, Color, float, Color>)KlazTweenLerp
                    .Lerp,
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
                    float3Tweens.Add(tweenId, tween);
                    break;
                case KlazTween<float3>:
                    float3Tweens.Add(tweenId, tween);
                    break;
                case KlazTween<float4>:
                    float3Tweens.Add(tweenId, tween);
                    break;
                case KlazTween<quaternion>:
                    float3Tweens.Add(tweenId, tween);
                    break;
                case KlazTween<Color>:
                    float3Tweens.Add(tweenId, tween);
                    break;
                case KlazTween<Color32>:
                    float3Tweens.Add(tweenId, tween);
                    break;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InitializeNativeArrays()
        {
            //Initialize arrays for float tweens
            InitializeNativeArrays(floatTweens.Values, floatNativeArrays);

            //Initialize arrays for float2 tweens
            InitializeNativeArrays(float2Tweens.Values, float3NativeArrays);
            
            //Initialize arrays for float3 tweens
            InitializeNativeArrays(float3Tweens.Values, float3NativeArrays);
            
            //Initialize arrays for float4 tweens
            InitializeNativeArrays(float4Tweens.Values, float3NativeArrays);
            
            //Initialize arrays for quaternion tweens
            InitializeNativeArrays(quaternionTweens.Values, float3NativeArrays);
            
            //Initialize arrays for color tweens
            InitializeNativeArrays(colorTweens.Values, float3NativeArrays);
            
            //Initialize arrays for color32 tweens
            InitializeNativeArrays(color32Tweens.Values, float3NativeArrays);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InitializeNativeArrays<T>(IReadOnlyCollection<IKlazTween> tweens, KlazTweenNativeArrays<T> nativeArrays) where T : struct
        {
            if (tweens.Count <= 0)
                return;

            nativeArrays.InitializeNativeArrays(tweens.Count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void PrepareTweenForJob()
        {
            //Prepare for job for float tweens
            PrepareTweenForJob(floatTweens.Values, floatNativeArrays);

            //Prepare for job for float2 tweens
            PrepareTweenForJob(float2Tweens.Values, float3NativeArrays);
            
            //Prepare for job for float3 tweens
            PrepareTweenForJob(float3Tweens.Values, float3NativeArrays);
            
            //Prepare for job for float4 tweens
            PrepareTweenForJob(float4Tweens.Values, float3NativeArrays);
            
            //Prepare for job for quaternion tweens
            PrepareTweenForJob(quaternionTweens.Values, float3NativeArrays);
            
            //Prepare for job for color tweens
            PrepareTweenForJob(colorTweens.Values, float3NativeArrays);
            
            //Prepare for job for color32 tweens
            PrepareTweenForJob(color32Tweens.Values, float3NativeArrays);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void PrepareTweenForJob<T>(List<IKlazTween> tweens, KlazTweenNativeArrays<T> nativeArrays) where T : struct
        {
            if (tweens.Count <= 0)
                return;

            for (var i = 0; i < tweens.Count; i++)
            {
                if (tweens[i] is not KlazTween<T> tween)
                    continue;

                var tweenComponentForJob = tween.PrepareForJob();
                nativeArrays.SetComponentForJobByIndex(tweenComponentForJob, i);
            }
        }
        #endregion
    }
}