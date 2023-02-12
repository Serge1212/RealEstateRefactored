using RealEstateRefactored.Enums;

namespace RealEstateRefactored.Models
{
    /// <summary>
    /// The model that represents the column of database table.
    /// </summary>
    [Serializable]
    public class Column
    {
        /// <summary>
        /// The name of the column.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of the column.
        /// </summary>
        public DataTypes Type { get; set; }

        /// <summary>
        /// The index of the column.
        /// </summary>
        public int Index { get; }

        public Column(string name, string type, int index)
        {
            Name = name;
            Type = (DataTypes)Enum.Parse(typeof(DataTypes), type); //TODO: add validation to the more top level
            Index = index;
        }
    }
}
