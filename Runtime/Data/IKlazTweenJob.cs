namespace com.Klazapp.Utility
{
    public interface IKlazTweenJob<T>
    {
        (int id, T currentValue, T startValue, T endValue, float duration, float startTime, bool isCompleted, float delay, EaseType easeType) PrepareForJob();

        void RetrieveFromJob((int id, T currentValue, T startValue, T endValue, float duration, float startTime, bool isCompleted, float delay, EaseType easeType) tweenJobComponent);
    }
}