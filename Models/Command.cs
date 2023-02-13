using RealEstateRefactored.Enums;

namespace RealEstateRefactored.Models
{
    public class Command
    {
        public bool IsValid = false;
        public string ErrorText;
        public CommandType Type;
        public string TableName;
        public string TableNameNew;
        public string TableNameJoin;
        public List<string> ColumnNames;
        public List<string> ColumnTypes;
        public List<string> ColumnValues;
        public List<string> ColumnNamesWhere;
        public List<string> ColumnConditionWhere;
        public List<string> ColumnValuesWhere;
        public List<string> ColumnNamesOrderBy;
        public List<string> OrderByTypes;
        public string SingleColumnName;
        public string SingleColumnNameNew;
        public string SingleColumnNameJoin;
        public string SingleValue;
    }
}
