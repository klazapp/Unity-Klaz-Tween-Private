namespace com.Klazapp.Utility
{
    public struct KlazTweenBaseComponent<T>
    {
        #region Variables
        public int id;
        public T currentValue;
        public T startValue;
        public T endValue;
        public float duration;
        public float startTime;

        public float delay;
        
        public EaseType easeType;
        #endregion
        
        #region Lifecycle Flow
        public KlazTweenBaseComponent(int id, T currentValue, T startValue, T endValue, float duration, float startTime, float delay, EaseType easeType)
        {
            this.id = id;
            this.currentValue = currentValue;
            this.startValue = startValue;
            this.endValue = endValue;
            this.duration = duration;
            this.startTime = startTime;
            this.delay = delay;
            this.easeType = easeType;
        }
        #endregion
        
        #region Public Access
        public int GetId()
        {
            return this.id;
        }

        public EaseType GetEaseType()
        {
            return easeType;
        }
        
        public (int id, T currentValue, T startValue, T endValue, float duration, float startTime, float delay, EaseType easeType) GetKlazTweenBaseComponents()
        {
            return (id, currentValue, startValue, endValue, duration, startTime, delay, easeType);
        }
        
        public T GetCurrentValue()
        {
            return currentValue;
        }
        
        public void SetKlazTweenBaseComponents((int id, T currentValue, T startValue, T endValue, float duration, float startTime, float delay, EaseType easeType) klazTweenBaseComponents)
        {
            this.id = klazTweenBaseComponents.id;
            this.currentValue = klazTweenBaseComponents.currentValue;
            this.startValue = klazTweenBaseComponents.startValue;
            this.endValue = klazTweenBaseComponents.endValue;
            this.duration = klazTweenBaseComponents.duration;
            this.startTime = klazTweenBaseComponents.startTime;
            this.delay = klazTweenBaseComponents.delay;
            this.easeType = klazTweenBaseComponents.easeType;
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