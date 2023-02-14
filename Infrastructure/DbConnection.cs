using RealEstateRefactored.Interfaces;
using RealEstateRefactored.Models;
using System.Runtime.Serialization.Formatters.Binary;

namespace RealEstateRefactored.Infrastructure
{
    [Serializable]
    /// <inheritdoc/>
    public class DbConnection : IDbConnection
    {
        private readonly IDbContext _context;

        public DbConnection(IDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public void Load(string filename = "base.dat")
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
            if (fs.Length > 0)
                _context.Tables = (List<Table>)formatter.Deserialize(fs);
        }

        /// <inheritdoc/>
        public void Save(string filename = "base.dat")
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
            formatter.Serialize(fs, _context.Tables);
        }
    }
}
