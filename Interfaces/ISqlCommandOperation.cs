namespace RealEstateRefactored.Interfaces
{
    /// <summary>
    /// An operation to be used within pipeline.
    /// </summary>
    public interface ISqlCommandOperation
    {
        /// <summary>
        /// Runs the functionality within an operation.
        /// </summary>
        Task Invoke();
    }
}
