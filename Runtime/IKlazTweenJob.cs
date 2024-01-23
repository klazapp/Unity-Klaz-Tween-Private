public interface IKlazTweenJob<T>
{
    (T currentValue, T startValue, T endValue, float duration, float startTime, bool isCompleted) PrepareForJob();
    void RetrieveFromJob((T currentValue, T startValue, T endValue, float duration, float startTime, bool isCompleted) tweenJobComponent);
    //void UpdateCurrentValueFromJob<T>(T value, bool completed);
}
