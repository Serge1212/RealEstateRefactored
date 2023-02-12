namespace RealEstateRefactored.Models {
     /// <summary>
     /// The model that represents the table in the database.
     /// </summary>
    public class Table {
        public string Name { get; private set; }
        public int MaxColumnIndex { get; private set; }
        public int MaxRowIndex { get; private set; }
        
        private List<Column> _columns;
        
        private List<Row> _rows;
    }
}
