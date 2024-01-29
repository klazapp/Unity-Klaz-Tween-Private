using Unity.Collections;
using Unity.Mathematics;

namespace com.Klazapp.Utility
{
     public class KlazTweenNativeArrays<T> where T : struct
     {
          #region Variables
          public NativeArray<T> currentValues;
          public NativeArray<T> startValues;
          public NativeArray<T> endValues;
          public NativeArray<float> duration;
          public NativeArray<float> startTime;
          public NativeArray<bool> isCompleted;
          
          //Ease Type
          public NativeArray<EaseType> easeTypes;
          
          //Delays
          public NativeArray<float> delays;
          
          //Id
          public NativeArray<int> ids;
          #endregion

          #region Public Access
          public void InitializeNativeArrays(int length)
          {
               currentValues = new NativeArray<T>(length, Allocator.Persistent);
               startValues = new NativeArray<T>(length, Allocator.Persistent);
               endValues = new NativeArray<T>(length, Allocator.Persistent);
               duration = new NativeArray<float>(length, Allocator.Persistent);
               startTime = new NativeArray<float>(length, Allocator.Persistent);
               isCompleted = new NativeArray<bool>(length, Allocator.Persistent);
               easeTypes = new NativeArray<EaseType>(length, Allocator.Persistent);
               delays = new NativeArray<float>(length, Allocator.Persistent);
               ids = new NativeArray<int>(length, Allocator.Persistent);
          }
          
          public static NativeArray<T> ResizeNativeArray<T>(NativeArray<T> original, int newSize, Allocator allocator) where T : struct
          {
               var resizedArray = new NativeArray<T>(newSize, allocator);

               //Copy data from the original array to the new array
               var elementsToCopy = math.min(original.Length, newSize);
               
               NativeArray<T>.Copy(original, resizedArray, elementsToCopy);

               //Dispose the original array if necessary
               if (original.IsCreated)
               {
                    original.Dispose();
               }

               return resizedArray;
          }

          public void DisposeNativeArrays()
          {
               if (currentValues.IsCreated)
                    currentValues.Dispose();
               if (startValues.IsCreated)
                    startValues.Dispose();
               if (endValues.IsCreated)
                    endValues.Dispose();
               if (duration.IsCreated)
                    duration.Dispose();
               if (startTime.IsCreated)
                    startTime.Dispose();
               if (isCompleted.IsCreated)
                    isCompleted.Dispose();
               if (easeTypes.IsCreated)
                    easeTypes.Dispose();
               if (delays.IsCreated)
                    delays.Dispose();
               if (ids.IsCreated)
                    ids.Dispose();
          }

          public void SetComponentForJobByIndex((int id, T currentValue, T startValue, T endValue, float duration, float startTime, bool isCompleted, float delay, EaseType easeType) nativeArrayComponent, int index)
          {
               currentValues[index] = nativeArrayComponent.currentValue;
               startValues[index] = nativeArrayComponent.startValue;
               endValues[index] = nativeArrayComponent.endValue;

               duration[index] = nativeArrayComponent.duration;
               startTime[index] = nativeArrayComponent.startTime;
               isCompleted[index] = nativeArrayComponent.isCompleted;

               easeTypes[index] = nativeArrayComponent.easeType;
               delays[index] = nativeArrayComponent.delay;

               ids[index] = nativeArrayComponent.id;
          }
          #endregion
     }
}