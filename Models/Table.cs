namespace RealEstateRefactored.Models {
     /// <summary>
     /// The model that represents the table in the database.
     /// </summary>
    public class Table {
        public string Name { get; set; }

        public int MaxColumnIndex { get; set; }

        public int MaxRowIndex { get; set; }
        
        public List<Column> Columns { get; set; }
        
        public List<Row> Rows { get; set; }
    }
}
