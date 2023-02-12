using RealEstateRefactored.Models;

namespace RealEstateRefactored.Interfaces
{
    public interface ITableService
    {
        /// <summary>
        /// Renames 
        /// </summary>
        /// <param name="newTableName"></param>
        void RenameTable(string newTableName);
        Column FindColumn(int index);
        Column FindColumn(string columnName);
        void AddColumn(string columnName, string type);
        void RenameColumn(string oldColumnName, string newColumnName);
        void DeleteColumn(string columnName);
        void AddRow(List<string> columnsNames, List<string> values);
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
