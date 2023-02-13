using RealEstateRefactored.Enums;
using RealEstateRefactored.Helpers;
using RealEstateRefactored.Interfaces;
using RealEstateRefactored.Models;

namespace RealEstateRefactored.Services
{
    /// <inheritdoc/>
    public class TableService : ITableService
    {
        private readonly IDbContext _dbContext;

        public TableService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public void RenameTable(string oldTableName, string newTableName)
        {
            var targetTable = _dbContext.Tables.SingleOrDefault(t => t.Name == oldTableName);
            if (targetTable != null)
            {
                targetTable.Name = newTableName;
            }
        }

        /// <inheritdoc/>
        public Column GetColumn(string tableName, string columnName)
        {
            var table = GetTable(tableName);
            if (table != null)
            {
                return table.Columns.SingleOrDefault(c => c.Name == columnName);
            }
            else
            {
                throw new ArgumentException("The table named {0} was not found", tableName);
            }
        }

        /// <inheritdoc/>
        public void AddColumn(string tableName, string columnName, string type)
        {
            // Add new column.
            var table = GetTable(tableName);
            var maxColumnIndex = ++table.MaxColumnIndex;

            var newColumn = new Column()
            {
                Name = columnName,
                Type = TypeHelper.ToEnum(type),
                Index = maxColumnIndex
            };

            table.Columns.Add(newColumn);

            // Fill all rows with the default column value.
            PopulateRowsWithNewColumn(table, newColumn.Type);
        }

        void RenameColumn(string tableName, string oldColumnName, string newColumnName)
        {

        }

        private static void PopulateRowsWithNewColumn(Table targetTable, DataType columnType)
        {
            var maxColumnIndex = targetTable.MaxColumnIndex;
            switch (columnType)
            {
                case DataType.Int:
                    targetTable.Rows.ForEach(r => r.Records.Add(new Record<int>(maxColumnIndex, 0)));
                    break;
                case DataType.Text:
                    targetTable.Rows.ForEach(r => r.Records.Add(new Record<string>(maxColumnIndex, "")));
                    break;
                case DataType.Bool:
                    targetTable.Rows.ForEach(r => r.Records.Add(new Record<bool>(maxColumnIndex, false)));
                    break;
                case DataType.Double:
                    targetTable.Rows.ForEach(r => r.Records.Add(new Record<double>(maxColumnIndex, 0)));
                    break;
                case DataType.Decimal:
                    targetTable.Rows.ForEach(r => r.Records.Add(new Record<decimal>(maxColumnIndex, 0)));
                    break;
            };
        }

        private Column GetColumn(Table table, int index)
        {
            var targetTable = _dbContext.Tables.SingleOrDefault(t => t == table);
            return targetTable.Columns.SingleOrDefault(c => c.Index == index);
        }

        private Table GetTable(string tableName)
        {
            return _dbContext.Tables.SingleOrDefault(t => t.Name == tableName);
        }

        private void SaveChanges(Table table)
        {

        }
    }
}
