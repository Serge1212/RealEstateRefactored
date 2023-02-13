using System.Diagnostics.CodeAnalysis;

namespace RealEstateRefactored.Models
{
    /// <summary>
    /// The model that represents the values of <see cref="Row"/>
    /// </summary>
    public class Record<T> : UniversalRecord where T : IComparable, IEquatable<T>
    {
        private T _value;

        public Record(int column, T Value)
        {
            ColumnIndex = column;
            _value = Value;
        }

        public Record(int column, string Value)
        {
            ColumnIndex = column;
            SetValue(Value);
        }

        public override int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public override int CompareTo([AllowNull] UniversalRecord other)
        {
            throw new NotImplementedException();
        }

        public override void GetValue(out string Value)
        {
            throw new NotImplementedException();
        }

        public override void GetValue(out int Value)
        {
            throw new NotImplementedException();
        }

        public override void GetValue(out bool Value)
        {
            throw new NotImplementedException();
        }

        public override void GetValue(out double Value)
        {
            throw new NotImplementedException();
        }

        public override void GetValue(out decimal Value)
        {
            throw new NotImplementedException();
        }

        public override void SetValue(int Value)
        {
            throw new NotImplementedException();
        }

        public override void SetValue(string Value)
        {
            throw new NotImplementedException();
        }

        public override void SetValue(bool Value)
        {
            throw new NotImplementedException();
        }

        public override void SetValue(double Value)
        {
            throw new NotImplementedException();
        }

        public override void SetValue(decimal Value)
        {
            throw new NotImplementedException();
        }
    }
}
