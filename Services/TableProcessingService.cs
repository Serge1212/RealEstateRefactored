using RealEstateRefactored.Interfaces;
using System.Runtime.Serialization.Formatters.Binary;

namespace RealEstateRefactored.Services {
  public class TableProcessingService : ITableProcessingService {
    protected List<Table> Tables { get; set; }

    public void Load(string filename = "base.dat") {
      BinaryFormatter formatter = new BinaryFormatter();
      using FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
      formatter.Serialize(fs, Tables);
    }

    public void Save(string filename = "base.dat") {
      throw new NotImplementedException();
    }
  }
}
