using RealEstateRefactored.Enums;

namespace RealEstateRefactored.Helpers
{
    /// <summary>
    /// The helper class for converting types from a source type to a target type.
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        /// Converts string representation of data type to enum.
        /// </summary>
        /// <param name="dataType">The target type to be converted.</param>
        /// <returns>The enum value.</returns>
        public static DataType ToEnum(string dataType) => (DataType)Enum.Parse(typeof(DataType), dataType);

        /// <summary>
        /// Gets type depending on value of <see cref="DataType"/>.
        /// </summary>
        /// <param name="dataType">The enum value to be converted.</param>
        /// <returns>The value of type <see cref="Type"/></returns>
        public static Type GetType(DataType dataType)
        {
            return dataType switch
            {
                DataType.Int => typeof(int),
                DataType.Text => typeof(string),
                DataType.Bool => typeof(bool),
                DataType.Double => typeof(double),
                DataType.Decimal => typeof(decimal),
                _ => typeof(object)
            };
        }
    }
}
