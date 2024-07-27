namespace Interfaces
{
    public interface IPoolObject
    {
        /// <summary>
        /// Invoked when the object is re-used.
        /// </summary>
        void Reset();
        
        /// <summary>
        /// Invoked when the object is removed from the pool.
        /// </summary>
        void Remove();
    }
}