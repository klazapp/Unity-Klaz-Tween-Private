using Unity.Collections;

public interface IKlazTween
{
    void OnUpdate();
    void ApplyUpdate();
    bool IsCompleted { get; set; }
    void InvokeComplete();
}