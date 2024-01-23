using Unity.Collections;

public class KlazTweenNativeArrays<T> where T : struct
{
     #region Variables
     public NativeArray<T> currentValues;
     public NativeArray<T> startValues;
     public NativeArray<T> endValues;
     public NativeArray<float> duration;
     public NativeArray<float> startTime;
     public NativeArray<bool> isCompleted;
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
     }
     
     public void SetComponentForJobByIndex((T currentValue, T startValue, T endValue, float duration, float startTime, bool isCompleted) nativeArrayComponent, int index)
     {
          currentValues[index] = nativeArrayComponent.currentValue;
          startValues[index] = nativeArrayComponent.startValue;
          endValues[index] = nativeArrayComponent.endValue;

          duration[index] = nativeArrayComponent.duration;
          startTime[index] = nativeArrayComponent.startTime;
          isCompleted[index] = nativeArrayComponent.isCompleted;
     }
     #endregion
}
