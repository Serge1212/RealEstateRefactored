using RealEstateRefactored.Enums;

namespace RealEstateRefactored.Models
{
    public abstract class UniversalRecord
    {
        public int ColumnIndex { get; protected set; }
        public DataType Type { get; protected set; }
    }
}
