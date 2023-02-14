using RealEstateRefactored.Interfaces;
using RealEstateRefactored.Models;

namespace RealEstateRefactored.Infrastructure
{
    /// <inheritdoc/>
    public class DbContext : IDbContext
    {
        /// <inheritdoc/>
        public List<Table> Tables { get; set; } = new List<Table>();
    }
}
