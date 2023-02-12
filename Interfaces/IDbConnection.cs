namespace RealEstateRefactored.Interfaces
{
    public interface IDbConnection
    {
        /// <summary>
        /// Saves the state of the database. Yet tables state only.
        /// </summary>
        /// <param name="filename">The path where the database file's allocated.</param>
        void Save(string filename = "base.dat");

        /// <summary>
        /// Loads the current state of the database. Yet tables state only.
        /// </summary>
        /// <param name="filename"></param>
        public void Load(string filename = "base.dat");
    }
}
