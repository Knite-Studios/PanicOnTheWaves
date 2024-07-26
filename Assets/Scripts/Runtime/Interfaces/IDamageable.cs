namespace Interfaces
{
    public interface IDamageable
    {
        /// <summary>
        /// A read-only property that returns the current health of the object.
        /// </summary>
        public int CurrentHealth { get; }
        
        /// <summary>
        /// Subtracts the damage from the object's health.
        /// </summary>
        /// <param name="damage">The amount of damage to subtract.</param>
        void TakeDamage(int damage);
        
        // /// <summary>
        // /// Adds the amount to the object's health.
        // /// </summary>
        // /// <param name="amount">The amount to add.</param>
        // void Heal(int amount);
    }
}