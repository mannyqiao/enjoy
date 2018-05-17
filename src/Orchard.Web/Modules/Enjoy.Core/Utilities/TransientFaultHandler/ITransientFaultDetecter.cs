namespace Enjoy.Core
{
    public interface ITransientFaultDetecter<T>
    {
        bool Detect(T condition);
    }
}