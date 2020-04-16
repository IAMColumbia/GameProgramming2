public interface IPoolable
{
    bool IsAlive { get; set; }
    void InitializePool();
}