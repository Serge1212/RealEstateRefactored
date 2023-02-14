using RealEstateRefactored.Models;

namespace RealEstateRefactored.Interfaces.Strategies.Base
{
    /// <summary>
    /// The base command strategy for all specific commands strategies.
    /// </summary>
    public interface ICommandStrategy
    {
        /// <summary>
        /// The methods that performs the strategy logic.
        /// </summary>
        /// <param name="command">The context of the strategy logic.</param>
        void Invoke(Command command);
    }
}
