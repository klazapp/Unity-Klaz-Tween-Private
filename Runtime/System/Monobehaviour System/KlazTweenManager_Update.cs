using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
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
            UpdateRegularTweens(color32Tweens);
            UpdateRegularTweens(double2Tweens);
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
            UpdateColor32TweensWithJobSystem();
            UpdateDouble2TweensWithJobSystem();

            ApplyJobTweenUpdates(floatTweens);
            ApplyJobTweenUpdates(float2Tweens);
            ApplyJobTweenUpdates(float3Tweens);
            ApplyJobTweenUpdates(float4Tweens);
            ApplyJobTweenUpdates(quaternionTweens);
            ApplyJobTweenUpdates(color32Tweens);
            ApplyJobTweenUpdates(double2Tweens);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ProcessCompletedTweens()
        {
            ProcessCompletedTweens(floatTweens);
            ProcessCompletedTweens(float2Tweens);
            ProcessCompletedTweens(float3Tweens);
            ProcessCompletedTweens(float4Tweens);
            ProcessCompletedTweens(quaternionTweens);
            ProcessCompletedTweens(color32Tweens);
            ProcessCompletedTweens(double2Tweens);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void UpdateRegularTweens(Dictionary<int, IKlazTween> tweens)
        {
            if (tweens.Count <= 0)
                return;
            
            for (var i = 0; i < tweens.Values.Count; i++)
            {
                foreach (var ikTween in tweens.Values)
                {
                    ikTween.OnUpdate();
                }
            }
        }
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ApplyJobTweenUpdates(Dictionary<int, IKlazTween> tweens)
        {
            if (tweens.Count <= 0)
                return;
           
            for (var i = 0; i < tweens.Values.Count; i++)
            {
                foreach (var ikTween in tweens.Values)
                {
                    if (!ikTween.IsCompleted)
                    {
                        ikTween.ApplyJobUpdate();
                    }
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ProcessCompletedTweens(Dictionary<int, IKlazTween> tweens)
        {
            // if (tweens.Count <= 0)
            //     return;
            //
            // var completedIds = new List<int>();
            //
            // // First, identify all completed tweens
            // foreach (var (id, tween) in tweens)
            // {
            //     if (tween.IsCompleted)
            //     {
            //         completedIds.Add(id);
            //     }
            // }
            //
            // // Then, remove them from the dictionary
            // foreach (var completedId in completedIds)
            // {
            //     tweens.Remove(completedId);
            // }
            //TestAdjust(tweens, floatNativeArrays);
        }

        private void TestAdjust<T>(Dictionary<int, IKlazTween> tweens, KlazTweenNativeArrays<T> nativeArrays) where T : struct
        {
            // Count how many are not completed

            //Exceeded max limit
            //Remove all completed,
            //If no completed found, increase limit
            if (tweens.Count >= 5)
            {
                Debug.Log("exceeded limit");
                var completedIds = new List<int>();
                foreach (var (id, tween) in tweens)
                {
                    if (tween.IsCompleted)
                    {
                        completedIds.Add(id);
                    }
                }

                //No completed found
                //Increase limit
                if (completedIds.Count <= 0)
                {
                    //Increase limit
                    
                    //Increase native array limit
                    ResizeAllArrays<float>(floatNativeArrays, 200, Allocator.Persistent);

                    //Increase tweens limit
                    //Tweens are dictionary so dont have to increase llimit
                }
                else
                {
                    //Found completed
                    //Remove from native arrays
                    RemoveElementsMatchingCompletedIds(floatNativeArrays, completedIds, Allocator.Persistent);
                    
                    //Remove from tween
                    foreach (var completedId in completedIds)
                    {
                        tweens.Remove(completedId);
                    }
                }
            }
        }
        
        public void RemoveElementsMatchingCompletedIds<T>(KlazTweenNativeArrays<T> klazTweenNativeArrays, List<int> completedIds, Allocator allocator) where T : struct
        {
            // First, determine the new size by counting non-completed IDs
            int newSize = 0;
            for (int i = 0; i < klazTweenNativeArrays.ids.Length; i++)
            {
                if (!completedIds.Contains(klazTweenNativeArrays.ids[i]))
                {
                    newSize++;
                }
            }

            // Allocate new NativeArrays with the new size
            NativeArray<T> newCurrentValues = new NativeArray<T>(newSize, allocator);
            NativeArray<T> newStartValues = new NativeArray<T>(newSize, allocator);
            NativeArray<T> newEndValues = new NativeArray<T>(newSize, allocator);
            NativeArray<float> newDuration = new NativeArray<float>(newSize, allocator);
            NativeArray<float> newStartTime = new NativeArray<float>(newSize, allocator);
            NativeArray<bool> newIsCompleted = new NativeArray<bool>(newSize, allocator);
            NativeArray<float> newDelays = new NativeArray<float>(newSize, allocator);
            NativeArray<int> newIds = new NativeArray<int>(newSize, allocator);

            // Copy over the elements that are not completed
            int index = 0;
            for (int i = 0; i < klazTweenNativeArrays.ids.Length; i++)
            {
                if (!completedIds.Contains(klazTweenNativeArrays.ids[i]))
                {
                    newCurrentValues[index] = klazTweenNativeArrays.currentValues[i];
                    newStartValues[index] = klazTweenNativeArrays.startValues[i];
                    newEndValues[index] = klazTweenNativeArrays.endValues[i];
                    newDuration[index] = klazTweenNativeArrays.duration[i];
                    newStartTime[index] = klazTweenNativeArrays.startTime[i];
                    newIsCompleted[index] = klazTweenNativeArrays.isCompleted[i];
                    newDelays[index] = klazTweenNativeArrays.delays[i];
                    newIds[index] = klazTweenNativeArrays.ids[i];
                    index++;
                }
            }

            // Dispose of the old NativeArrays
            klazTweenNativeArrays.currentValues.Dispose();
            klazTweenNativeArrays.startValues.Dispose();
            klazTweenNativeArrays.endValues.Dispose();
            klazTweenNativeArrays.duration.Dispose();
            klazTweenNativeArrays.startTime.Dispose();
            klazTweenNativeArrays.isCompleted.Dispose();
            klazTweenNativeArrays.delays.Dispose();
            klazTweenNativeArrays.ids.Dispose();

            // Update the references to the new NativeArrays
            klazTweenNativeArrays.currentValues = newCurrentValues;
            klazTweenNativeArrays.startValues = newStartValues;
            klazTweenNativeArrays.endValues = newEndValues;
            klazTweenNativeArrays.duration = newDuration;
            klazTweenNativeArrays.startTime = newStartTime;
            klazTweenNativeArrays.isCompleted = newIsCompleted;
            klazTweenNativeArrays.delays = newDelays;
            klazTweenNativeArrays.ids = newIds;
        }
        
        
        // Method to resize all NativeArrays while retaining data
        public void ResizeAllArrays<T>(KlazTweenNativeArrays<T> klazTweenNativeArrays, int newSize, Allocator allocator) where T : struct
        {
            klazTweenNativeArrays.currentValues = ResizeNativeArray(klazTweenNativeArrays.currentValues, newSize, allocator);
            klazTweenNativeArrays.startValues = ResizeNativeArray(klazTweenNativeArrays.startValues, newSize, allocator);
            klazTweenNativeArrays.endValues = ResizeNativeArray(klazTweenNativeArrays.endValues, newSize, allocator);
            klazTweenNativeArrays.duration = ResizeNativeArray(klazTweenNativeArrays.duration, newSize, allocator);
            klazTweenNativeArrays.startTime = ResizeNativeArray(klazTweenNativeArrays.startTime, newSize, allocator);
            klazTweenNativeArrays.isCompleted = ResizeNativeArray(klazTweenNativeArrays.isCompleted, newSize, allocator);
            klazTweenNativeArrays.delays = ResizeNativeArray(klazTweenNativeArrays.delays, newSize, allocator);
            klazTweenNativeArrays.ids = ResizeNativeArray(klazTweenNativeArrays.ids, newSize, allocator);
        }

        private NativeArray<TValue> ResizeNativeArray<TValue>(NativeArray<TValue> originalArray, int newSize, Allocator allocator) where TValue : struct
        {
            NativeArray<TValue> newArray = new NativeArray<TValue>(newSize, allocator);
            int elementsToCopy = Math.Min(originalArray.Length, newSize);
            for (int i = 0; i < elementsToCopy; i++)
            {
                newArray[i] = originalArray[i];
            }
            originalArray.Dispose(); // Dispose of the original array
            return newArray;
        }
    }
}