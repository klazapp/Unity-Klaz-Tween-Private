namespace com.Klazapp.Utility
{
    public struct KlazTweenBaseComponent<T>
    {
        #region Variables
        public T currentValue;
        public T startValue;
        public T endValue;
        public float duration;
        public float startTime;

        public float delay;
        #endregion
        
        #region Lifecycle Flow
        public KlazTweenBaseComponent(T currentValue, T startValue, T endValue, float duration, float startTime, float delay)
        {
            this.currentValue = currentValue;
            this.startValue = startValue;
            this.endValue = endValue;
            this.duration = duration;
            this.startTime = startTime;
            this.delay = delay;
        }
        #endregion
        
        #region Public Access
        public (T currentValue, T startValue, T endValue, float duration, float startTime, float delay) GetKlazTweenBaseComponents()
        {
            return (currentValue, startValue, endValue, duration, startTime, delay);
        }
        
        public T GetCurrentValue()
        {
            return currentValue;
        }
        
        public void SetKlazTweenBaseComponents((T currentValue, T startValue, T endValue, float duration, float startTime, float delay) klazTweenBaseComponents)
        {
            this.currentValue = klazTweenBaseComponents.currentValue;
            this.startValue = klazTweenBaseComponents.startValue;
            this.endValue = klazTweenBaseComponents.endValue;
            this.duration = klazTweenBaseComponents.duration;
            this.startTime = klazTweenBaseComponents.startTime;
            this.delay = klazTweenBaseComponents.delay;
        }

        public float GetStartTime()
        {
            return startTime;
        }
        
        public void SetStartTime(float startTime)
        {
            this.startTime = startTime;
        }
        
        public float GetDelay()
        {
            return delay;
        }
        #endregion
    }
}