namespace RealEstateRefactored.Models
{
    [Serializable]
    public class Row
    {
        /// <summary>
        /// Row index
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Records that the Row contains.
        /// </summary>
        public List<UniversalRecord> Records { get; set; } = new List<UniversalRecord>();
    }
}
