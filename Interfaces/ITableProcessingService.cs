namespace RealEstateRefactored.Interfaces {
  /// <summary>
  /// The processing service for working with database tables.
  /// </summary>
  public interface ITableProcessingService {
    /// <summary>
    /// Saves new a table.
    /// </summary>
    /// <param name="filename">The path where a table will be stored.</param>
    void Save(string filename = "base.dat");

    /// <summary>
    /// Loads all existing tables from specified path.
    /// </summary>
    void Load(string filename = "base.dat");
  }
}
