using RealEstateRefactored.Enums;

namespace RealEstateRefactored.Models
{
    /// <summary>
    /// The model class for detailed command purpose.
    /// </summary>
    public class Command
    {
        /// <summary>
        /// The flag that determines if the command if valid.
        /// </summary>
        public bool IsValid = false;

        /// <summary>
        /// The error text if the command is not valid.
        /// </summary>
        public string ErrorText;

        /// <summary>
        /// The type of the command. E.g. Insert into table.
        /// </summary>
        public CommandType Type;

        /// <summary>
        /// The name of related table for the command. E.g. Insert into [TableName].
        /// </summary>
        public string TableName;

        /// <summary>
        /// The new name of the table if ALTER for rename was requested.
        /// Has value NULL if not requested.
        /// </summary>
        public string TableNameNew;

        /// <summary>
        /// The name of the table to join to others.
        /// Has value NULL if not requested.
        /// </summary>
        public string TableNameJoin;

        /// <summary>
        /// Names of involved columns for table in the command.
        /// </summary>
        public List<string> ColumnNames;

        /// <summary>
        /// Types of involved columns for table in the command.
        /// </summary>
        public List<string> ColumnTypes;

        /// <summary>
        /// Values of involved columns for table in the command.
        /// </summary>
        public List<string> ColumnValues;

        /// <summary>
        /// The collection of names of columns involved for WHERE clause.
        /// Has value NULL if not requested.
        /// </summary>
        public List<string> ColumnNamesWhere;

        /// <summary>
        /// The WHERE clause conditions. E.g. ">", ">=", etc.
        /// Has value NULL if not requested.
        /// </summary>
        public List<string> ColumnConditionWhere;

        /// <summary>
        /// The values of columns for WHERE clause. E.g. "[TableName].[TableColumn] > <b>12</b>".
        /// Has value NULL if not requested.
        /// </summary>
        public List<string> ColumnValuesWhere;

        /// <summary>
        /// The column names involved for ORDER BY clause.
        /// Has value NULL if not requested.
        /// </summary>
        public List<string> ColumnNamesOrderBy;

        /// <summary>
        /// The types involved for ORDER BY clause.
        /// Has value NULL if not requested.
        /// </summary>
        public List<string> OrderByTypes;

        /// <summary>
        /// Name of the single column.
        /// </summary>
        public string SingleColumnName;

        /// <summary>
        /// Name of the new single column.
        /// </summary>
        public string SingleColumnNameNew;

        /// <summary>
        /// The name of single column involved for JOIN clause.
        /// </summary>
        public string SingleColumnNameJoin;

        /// <summary>
        /// The single value.
        /// </summary>
        public string SingleValue;
    }
}
