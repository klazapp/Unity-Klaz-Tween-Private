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
        
        public bool IsDelayCompleted { get; set; }
        public bool IsStarted { get; set; }
        public bool IsCompleted { get; set; }
        #endregion

        public int GetId()
        {
            return klazTweenBaseComponent.GetId();
        }
        #region Lifecycle Flow
        public KlazTween(int id, T startValue, T endValue, float duration, Action<T> onUpdate, Func<T, T, float, T> lerpFunc, float delay = 0, EaseType easeType = EaseType.Linear, KlazTweenCallback onStart = null, KlazTweenCallback onComplete = null)
        {
            klazTweenBaseComponent = new KlazTweenBaseComponent<T>(id, startValue, startValue, endValue, duration, Time.time, delay, easeType);

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
            var (id, _, startValue, endValue, duration, startTime, __, easeType) = klazTweenBaseComponent.GetKlazTweenBaseComponents();

            var currentTime = Time.time;
            var normalizedTime = math.clamp((currentTime - startTime) / duration, 0f, 1f);

            // Retrieve the ease type and apply easing function
            var easedTime = Easing.SetEasingByEaseType(easeType, normalizedTime);

            if (easedTime >= 1.0f)
            {
                InvokeComplete();
            }

            var lerpFunc = klazTweenBehaviourComponent.GetLerpFunc();
            var currentValue = lerpFunc(startValue, endValue, easedTime);

            var onUpdate = klazTweenBehaviourComponent.GetOnUpdate();
            onUpdate?.Invoke(currentValue);
            
            klazTweenBaseComponent.SetKlazTweenBaseComponents((id, currentValue, startValue, endValue, duration, startTime, __, easeType));
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

            var onComplete = klazTweenBehaviourComponent.GetOnComplete();
            onComplete?.Invoke();
            IsCompleted = true;
        }
        #endregion

        #region IKlazTweenJob
        public (int id, T currentValue, T startValue, T endValue, float duration, float startTime, bool isCompleted, float delay, EaseType easeType) PrepareForJob()
        {
            var (id, currentValue, startValue, endValue, duration, startTime, delay, easeType) = klazTweenBaseComponent.GetKlazTweenBaseComponents();
            return (id, currentValue, startValue, endValue, duration, startTime, IsCompleted, delay, easeType);
        }

        public void RetrieveFromJob((int id, T currentValue, T startValue, T endValue, float duration, float startTime, bool isCompleted, float delay, EaseType easeType) tweenJobComponent)
        {
            klazTweenBaseComponent.SetKlazTweenBaseComponents((tweenJobComponent.id, tweenJobComponent.currentValue, tweenJobComponent.startValue, tweenJobComponent.endValue, tweenJobComponent.duration, tweenJobComponent.startTime, tweenJobComponent.delay, tweenJobComponent.easeType));

            if (tweenJobComponent.isCompleted)
            {
                InvokeComplete();
            }
        }
        #endregion
    }
}
