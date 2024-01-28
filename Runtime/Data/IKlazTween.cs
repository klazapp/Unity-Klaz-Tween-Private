namespace com.Klazapp.Utility
{
    public interface IKlazTween
    {
        void OnUpdate();
        void ApplyRegularUpdate();
        void ApplyJobUpdate();

        void ApplyDelay();
        bool IsDelayCompleted { get; set; }
        void InvokeDelayCompleted();
        
        bool IsStarted { get; set; }
        void InvokeStart();
        
        bool IsCompleted { get; set; }
        void InvokeComplete();
    }
}