using System;
using System.Collections.Generic;

namespace PBCD.Range
{
    public class Range
	{
		public Range(IComparable start, IComparable end)
		{
			Start = start;
			End = end;
		}
		public IComparable Start { get; }
		public IComparable End { get; }

		public override bool Equals(object obj)
		{
			if (!(obj is Range other) 
				|| Start.CompareTo(other.Start) != 0 
				|| End.CompareTo(other.End) != 0
			) return false;
			return true;
		}

		public override int GetHashCode()
		{
			var hashCode = -1676728671;
			hashCode = hashCode * -1521134295 + EqualityComparer<IComparable>.Default.GetHashCode(Start);
			hashCode = hashCode * -1521134295 + EqualityComparer<IComparable>.Default.GetHashCode(End);
			return hashCode;
		}

		public override string ToString()
		{
			return Start.ToString() + " .. " + End.ToString();
		}
	}
}
