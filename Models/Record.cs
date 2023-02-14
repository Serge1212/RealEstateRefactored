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
    }
}
