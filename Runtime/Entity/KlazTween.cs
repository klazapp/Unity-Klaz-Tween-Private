using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace com.Klazapp.Utility
{
    public class KlazTween<T> : IKlazTween, IKlazTweenJob<T> where T : struct
    {
        #region Variables
        private KlazTweenBaseComponent<T> klazTweenBaseComponent;
        private KlazTweenBehaviourComponent<T> klazTweenBehaviourComponent;
        
        public int Id { get; set; }

        public bool IsDelayCompleted { get; set; }
        public bool IsStarted { get; set; }
        public bool IsCompleted { get; set; }
        #endregion

        #region Lifecycle Flow
        public KlazTween(T startValue, T endValue, float duration, Action<T> onUpdate, Func<T, T, float, T> lerpFunc, float delay = 0, KlazTweenCallback onStart = null, KlazTweenCallback onComplete = null, int id = 0)
        {
            Id = id;
            
            klazTweenBaseComponent = new KlazTweenBaseComponent<T>(startValue, startValue, endValue, duration, Time.time, delay);

            klazTweenBehaviourComponent = new KlazTweenBehaviourComponent<T>();
            klazTweenBehaviourComponent.SetKlazTweenBehaviourComponent(onUpdate, lerpFunc, onStart, onComplete);

            IsDelayCompleted = false;
            IsStarted = false;
            IsCompleted = false;
        
            InvokeStart();
        }

        public void OnUpdate()
        {
            if (IsCompleted)
                return;

            ApplyDelay();

            if (!IsDelayCompleted)
                return;
            
            ApplyRegularUpdate();
        }
        #endregion

        #region IKlazTween
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ApplyDelay()
        {
            if (IsDelayCompleted) 
                return;
            
            if (Time.time < klazTweenBaseComponent.GetStartTime() + klazTweenBaseComponent.GetDelay()) 
                return;
                
            IsDelayCompleted = true;
            klazTweenBaseComponent.SetStartTime(Time.time);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ApplyRegularUpdate()
        {
            var (_, startValue, endValue, duration, startTime, __) = klazTweenBaseComponent.GetKlazTweenBaseComponents();

            var currentTime = Time.time;
            var normalizedTime = math.clamp((currentTime - startTime) / duration, 0f, 1f);

            if (normalizedTime >= 1.0f)
            {
                InvokeComplete();
            }

            var lerpFunc = klazTweenBehaviourComponent.GetLerpFunc();
            var currentValue = lerpFunc(startValue, endValue, normalizedTime);

            var onUpdate = klazTweenBehaviourComponent.GetOnUpdate();
            onUpdate?.Invoke(currentValue);
            
            klazTweenBaseComponent.SetKlazTweenBaseComponents((currentValue, startValue, endValue, duration, startTime, __));
        }
        
        public void ApplyJobUpdate()
        {
            var onUpdate = klazTweenBehaviourComponent.GetOnUpdate();
            onUpdate?.Invoke(klazTweenBaseComponent.GetCurrentValue());
        }

        public void InvokeDelayCompleted()
        {
            
        }
        
        public void InvokeStart()
        {
            if (IsStarted)
                return;

            var onStart = klazTweenBehaviourComponent.GetOnStart();
            onStart?.Invoke();
            IsStarted = true;
        }

        public void InvokeComplete()
        {
            if (IsCompleted)
                return;

            Debug.Log("tween id = " + Id + ", cinmpelted");
            var onComplete = klazTweenBehaviourComponent.GetOnComplete();
            onComplete?.Invoke();
            IsCompleted = true;
        }
        #endregion

        #region IKlazTweenJob
        public (T currentValue, T startValue, T endValue, float duration, float startTime, bool isCompleted, float delay) PrepareForJob()
        {
            var (currentValue, startValue, endValue, duration, startTime, delay) = klazTweenBaseComponent.GetKlazTweenBaseComponents();
            return (currentValue, startValue, endValue, duration, startTime, IsCompleted, delay);
        }

        public void RetrieveFromJob((T currentValue, T startValue, T endValue, float duration, float startTime, bool isCompleted, float delay) tweenJobComponent)
        {
            klazTweenBaseComponent.SetKlazTweenBaseComponents((tweenJobComponent.currentValue, tweenJobComponent.startValue, tweenJobComponent.endValue, tweenJobComponent.duration, tweenJobComponent.startTime, tweenJobComponent.delay));

            if (tweenJobComponent.isCompleted)
            {
                InvokeComplete();
            }
        }
        #endregion
    }
}
