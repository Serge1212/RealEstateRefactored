using RealEstateRefactored.Models;

namespace RealEstateRefactored.Interfaces
{
    /// <summary>
    /// The context that holds the state of the database.
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// The state of the database tables.
        /// </summary>
        public List<Table> Tables { get; set; }
    }
}
