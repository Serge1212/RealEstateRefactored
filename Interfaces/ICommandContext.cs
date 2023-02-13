using RealEstateRefactored.Models;

namespace RealEstateRefactored.Interfaces
{
    /// <summary>
    /// The context class that identifies a command passed by a user and determines further actions.
    /// </summary>
    public interface ICommandContext
    {
        /// <summary>
        /// Identifies the raw command that was passed from UI.
        /// </summary>
        /// <param name="command">The raw command passed from UI.</param>
        /// <returns>The command model.</returns>
        Command IdentifyCommand(string command);
    }
}
