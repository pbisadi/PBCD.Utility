using System;
using System.Collections.Generic;

namespace PBCD.Extensions.List
{
    public static class ListExtension
	{
		/// <summary>
		/// Create a subset of the current IList based on specified range.
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="step"></param>
		/// <returns></returns>
		public static IList<T> R<T>(this IList<T> list, int start, int end, int step = 1)
		{
			if (Math.Abs(start) > list.Count)
				throw new ArgumentOutOfRangeException(string.Format("This list has {0} members, start value is out of range.", list.Count));
			if (Math.Abs(end) > list.Count)
				throw new ArgumentOutOfRangeException(string.Format("This list has {0} members, start value is out of range.", list.Count));
			if (step < 1)
				throw new ArgumentOutOfRangeException("Step must be greater than zero.");
			if (end <= 0) end = list.Count + end;
			if (start <= 0) start = list.Count + start;

			object correct_type_instance = Activator.CreateInstance(list.GetType());
			var result = correct_type_instance as IList<T>;
			for (int i = start; i < end; i+= step)
			{
				result.Add(list[i]);
			}
			return result;
		}

		

	}
}
