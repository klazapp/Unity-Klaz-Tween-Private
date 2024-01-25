using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace com.Klazapp.Utility
{
    public partial class KlazTweenManager
    {
        #region Modules
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateRegularTweens()
        {
            useJobSystem = false;
            
            UpdateRegularTweens(floatTweens);
            UpdateRegularTweens(float2Tweens);
            UpdateRegularTweens(float3Tweens);
            UpdateRegularTweens(float4Tweens);
            UpdateRegularTweens(quaternionTweens);
            UpdateRegularTweens(colorTweens);
            UpdateRegularTweens(color32Tweens);
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateJobTweens()
        {
            useJobSystem = true;
          
            UpdateFloatTweensWithJobSystem();
            UpdateFloat2TweensWithJobSystem();
            UpdateFloat3TweensWithJobSystem();
            UpdateFloat4TweensWithJobSystem();
            UpdateQuaternionTweensWithJobSystem();
            UpdateColorTweensWithJobSystem();
            UpdateColor32TweensWithJobSystem();
            
            ApplyJobTweenUpdates(floatTweens);
            ApplyJobTweenUpdates(float2Tweens);
            ApplyJobTweenUpdates(float3Tweens);
            ApplyJobTweenUpdates(float4Tweens);
            ApplyJobTweenUpdates(quaternionTweens);
            ApplyJobTweenUpdates(colorTweens);
            ApplyJobTweenUpdates(color32Tweens);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ProcessCompletedTweens()
        {
            ProcessCompletedTweens(floatTweens);
            ProcessCompletedTweens(float2Tweens);
            ProcessCompletedTweens(float3Tweens);
            ProcessCompletedTweens(float4Tweens);
            ProcessCompletedTweens(quaternionTweens);
            ProcessCompletedTweens(colorTweens);
            ProcessCompletedTweens(color32Tweens);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void UpdateRegularTweens(List<IKlazTween> tweens)
        {
            foreach (var tween in tweens)
            {
                tween.OnUpdate();
            }
        }
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ApplyJobTweenUpdates(IEnumerable<IKlazTween> tweens)
        {
            foreach (var tween in tweens)
            {
                if (!tween.IsCompleted)
                {
                    tween.ApplyJobUpdate();
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ProcessCompletedTweens(IList<IKlazTween> tweens)
        {
            var completedIds = new List<int>();
            foreach (var (id, tween) in tweens)
            {
                if (tween.IsCompleted)
                {
                    completedIds.Add(pair.Key);
                }
            }

            foreach (var id in completedIds)
            {
                tweens.Remove(id);
            }
            
            for (var i = tweens.Count - 1; i >= 0; i--)
            {
                if (!tweens[i].IsCompleted)
                    continue;
              
                tweens.RemoveAt(i);
            }
        }
    }
}