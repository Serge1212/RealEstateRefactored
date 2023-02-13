using RealEstateRefactored.Models;

namespace RealEstateRefactored.Interfaces
{
    /// <summary>
    /// The service for operating with database table.
    /// </summary>
    public interface ITableService
    {
        /// <summary>
        /// Renames the table.
        /// </summary>
        /// <param name="newTableName">The target table to rename.</param>
        void RenameTable(string oldTableName, string newTableName);

        /// <summary>
        /// Gets the column of the table.
        /// </summary>
        /// <param name="tableName">Target table to get a column from.</param>
        /// <param name="columnName">Name of target column.</param>
        /// <returns>The column of the table.</returns>
        Column GetColumn(string tableName, string columnName);

        /// <summary>
        /// Adds a column to the existing table.
        /// </summary>
        /// <param name="tableName">Target table where the column is added.</param>
        /// <param name="columnName">The name of column to be added.</param>
        /// <param name="type">The type of new column to be added.</param>
        void AddColumn(string tableName, string columnName, string type);

        /// <summary>
        /// Renames existing column in the existing table.
        /// </summary>
        /// <param name="oldColumnName">The column name to be renamed to.</param>
        /// <param name="newColumnName">New column name.</param>
        void RenameColumn(string tableName, string oldColumnName, string newColumnName);

        /// <summary>
        /// Deletes the existing column from the existing table.
        /// </summary>
        /// <param name="tableName">The name of the table where the column will removed.</param>
        /// <param name="columnName">The name of column to be removed.</param>
        void DeleteColumn(string tableName, string columnName);
        
        /// <summary>
        /// Adds new rows to the existing table.
        /// </summary>
        /// <param name="columnsNames">The target columns that INSERT statement involved.</param>
        /// <param name="values">The values of the columns.</param>
        void AddRow(string tableName, List<string> columnsNames, List<string> values);
        void DeleteRow(Row row);
        bool CheckRowCondition(Row row, string conditionColumnName, string columnCondition, string conditionValue);
        bool CheckRowCondition(Row row1, string conditionColumnName, string columnCondition, Row row2);
        void DeleteRows(string SingleColumnName, string SingleValue);
        void SetValueWithCondition(string columnName, string columnValue, string conditionColumnName, string columnCondition, string conditionValue);
        //UniversalRecord GetRecord(string columnName, int rowIndex);
        List<string> GetColumnNames();
        Row GetRow(int index);
        void SwapRows(Row row1, Row row2);
        void ClearAllRows();
    }
}
