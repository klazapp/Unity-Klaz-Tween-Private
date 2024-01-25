namespace com.Klazapp.Utility
{
    public interface IKlazTweenJob<T>
    {
        (T currentValue, T startValue, T endValue, float duration, float startTime, bool isCompleted, float delay) PrepareForJob();

        void RetrieveFromJob((T currentValue, T startValue, T endValue, float duration, float startTime, bool isCompleted, float delay) tweenJobComponent);
    }
}