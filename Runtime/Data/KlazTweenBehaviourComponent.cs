using System;

namespace com.Klazapp.Utility
{
    public struct KlazTweenBehaviourComponent<T>
    {
        #region Variables
        private Action<T> onUpdate;
        private Func<T, T, float, T> lerpFunc;
        public event KlazTweenCallback OnStart;
        public event KlazTweenCallback OnComplete;
        #endregion
        
        #region Public Access
        public void SetKlazTweenBehaviourComponent(Action<T> onUpdate, Func<T, T, float, T> lerpFunc, KlazTweenCallback onStart = null, KlazTweenCallback onComplete = null)
        {
            this.onUpdate = onUpdate;
            this.lerpFunc = lerpFunc;
            this.OnStart = onStart;
            this.OnComplete = onComplete;
        }

        public Func<T, T, float, T> GetLerpFunc()
        {
            return lerpFunc;
        }
        
        public Action<T> GetOnUpdate()
        {
            return onUpdate;
        }
        
        public KlazTweenCallback GetOnStart()
        {
            return OnStart;
        }
        
        public KlazTweenCallback GetOnComplete()
        {
            return OnComplete;
        }
        #endregion
    }
}