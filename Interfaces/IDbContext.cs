using RealEstateRefactored.Models;

namespace RealEstateRefactored.Interfaces
{
    public interface IDbContext
    {
        List<Table> Tables { get; set; }
    }
}
