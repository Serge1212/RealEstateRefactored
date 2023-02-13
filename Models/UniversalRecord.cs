using RealEstateRefactored.Enums;
using System.Diagnostics.CodeAnalysis;

namespace RealEstateRefactored.Models
{
    public abstract class UniversalRecord : IComparable, IComparable<UniversalRecord>
    {
        public int ColumnIndex { get; protected set; }
        public DataType Type { get; protected set; }
        public override string ToString() => throw new InvalidOperationException();
        public abstract void SetValue(int Value);
        public abstract void SetValue(string Value);
        public abstract void SetValue(bool Value);
        public abstract void SetValue(double Value);
        public abstract void SetValue(decimal Value);
        public abstract void GetValue(out string Value);
        public abstract void GetValue(out int Value);
        public abstract void GetValue(out bool Value);
        public abstract void GetValue(out double Value);
        public abstract void GetValue(out decimal Value);
        public abstract int CompareTo(object obj);
        public abstract int CompareTo([AllowNull] UniversalRecord other);
        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();
    }
}
