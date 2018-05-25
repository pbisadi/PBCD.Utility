using System;

namespace PBCD.Extensions.List
{
	public static class StringExtension
	{
		public static string R(this string s, int start, int end)
		{
			if (Math.Abs(start) > s.Length)
				throw new ArgumentOutOfRangeException(string.Format("The length of string is {0}, start value is out of range.", s.Length));
			if (Math.Abs(end) > s.Length)
				throw new ArgumentOutOfRangeException(string.Format("The length of string is {0}, end value is out of range.", s.Length));
			if (end <= 0) end = s.Length + end;
			if (start <= 0) start = s.Length + start;

			return s.Substring(start, end - start);
		}
	}
}
