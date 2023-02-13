using RealEstateRefactored.Interfaces;
using RealEstateRefactored.Models;

namespace RealEstateRefactored.Infrastructure
{
    public class DbContext : IDbContext
    {
        public List<Table> Tables { get; set; }
    }
}
