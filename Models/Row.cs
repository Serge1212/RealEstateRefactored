﻿namespace RealEstateRefactored.Models
{
    [Serializable]
    public class Row
    {
        public int Index { get; }
        public List<UniversalRecord> Records { get; set; } = new List<UniversalRecord>();
        public Row(int index)
        {
            Index = index;
            Records = new List<UniversalRecord>();
        }
    }
}
