using RealEstateRefactored.Enums;
using RealEstateRefactored.Helpers;
using RealEstateRefactored.Interfaces;
using RealEstateRefactored.Models;

namespace RealEstateRefactored.Services
{
    /// <inheritdoc/>
    public class TableService : ITableService
    {
        private readonly IDbContext _context;

        public TableService(IDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public void RenameTable(string oldTableName, string newTableName)
        {
            var targetTable = _context.Tables.SingleOrDefault(t => t.Name == oldTableName);
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

            // Fill all rows with the default column value.
            PopulateRowsWithNewColumn(table, newColumn.Type);

            // Patch existing table.
            _context.Tables.SingleOrDefault(t => t.Name == tableName).Columns.Add(newColumn);
        }

        /// <inheritdoc/>
        public void RenameColumn(string tableName, string oldColumnName, string newColumnName)
        {
            _context
                .Tables
                .SingleOrDefault(t => t.Name == tableName)
                .Columns
                .SingleOrDefault(c => c.Name == oldColumnName)
                .Name = newColumnName;
        }

        /// <inheritdoc/>
        public void DeleteColumn(string tableName, string columnName)
        {
            var table = GetTable(tableName);
            var column = GetColumn(columnName, table);
            _context
                 .Tables
                 .SingleOrDefault(t => t.Name == tableName)
                 .Columns
                 .Remove(column);
        }

        /// <inheritdoc/>
        void AddRow(string tableName, List<string> columnsNames, List<string> values)
        {
            var table = GetTable(tableName);
            var maxRowIndex = ++table.MaxRowIndex;


        }

        void ITableService.AddRow(string tableName, List<string> columnsNames, List<string> values)
        {
            throw new NotImplementedException();
        }

        public void DeleteRow(Row row)
        {
            throw new NotImplementedException();
        }

        public bool CheckRowCondition(Row row, string conditionColumnName, string columnCondition, string conditionValue)
        {
            throw new NotImplementedException();
        }

        public bool CheckRowCondition(Row row1, string conditionColumnName, string columnCondition, Row row2)
        {
            throw new NotImplementedException();
        }

        public void DeleteRows(string SingleColumnName, string SingleValue)
        {
            throw new NotImplementedException();
        }

        public void SetValueWithCondition(string columnName, string columnValue, string conditionColumnName, string columnCondition, string conditionValue)
        {
            throw new NotImplementedException();
        }

        public List<string> GetColumnNames()
        {
            throw new NotImplementedException();
        }

        public Row GetRow(int index)
        {
            throw new NotImplementedException();
        }

        public void SwapRows(Row row1, Row row2)
        {
            throw new NotImplementedException();
        }

        public void ClearAllRows()
        {
            throw new NotImplementedException();
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

        private Column GetColumn(int index, Table table)
        {
            return table.Columns.SingleOrDefault(c => c.Index == index);
        }

        private Column GetColumn(string columnName, Table table)
        {
            return table.Columns.SingleOrDefault(c => c.Name == columnName);
        }

        private Table GetTable(string tableName)
        {
            return _context.Tables.SingleOrDefault(t => t.Name == tableName);
        }

        public void CreateTable(string tableName, List<string> columnNames, List<string> columnTypes)
        {
            // Create default table values.
            var columns = new List<Column>();
            var rows = new List<Row>();
            int maxColumnIndex = -1;
            int maxRowIndex = -1;


            var newTable = new Table
            {
                Name = tableName,
                Columns = columns,
                Rows = rows,
                MaxColumnIndex = maxColumnIndex,
                MaxRowIndex = maxRowIndex
            };

            // Notify the context about the new table.
            _context.Tables.Add(newTable);

            for (int i = 0; i < columnNames.Count; i++)
            {
                AddColumn(tableName, columnNames[i], columnTypes[i]);
            }
        }
    }
}
