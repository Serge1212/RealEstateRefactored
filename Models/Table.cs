namespace RealEstateRefactored.Models {
    /// <summary>
    /// The model that represents the table in the database.
    /// </summary>
    [Serializable]
    public class Table {
        /// <summary>
        /// The name of the table.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The max column index that the table contains.
        /// </summary>
        public int MaxColumnIndex { get; set; }

        /// <summary>
        /// The max index of the row that the table contains.
        /// </summary>
        public int MaxRowIndex { get; set; }
        
        /// <summary>
        /// Related columns of the table.
        /// </summary>
        public List<Column> Columns { get; set; }

        /// <summary>
        /// Related rows of the table.
        /// </summary>
        public List<Row> Rows { get; set; }
    }
}
