using RealEstateRefactored.Models;

namespace RealEstateRefactored.Interfaces.Strategies.Base
{
    /// <summary>
    /// The base command strategy for all specific commands strategies.
    /// </summary>
    public interface ICommandStrategy
    {
        void Invoke(Command command);
    }
}
