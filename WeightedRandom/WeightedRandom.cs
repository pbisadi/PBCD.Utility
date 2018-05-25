using PBCD.Algorithms.Sorts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCD.Algorithms.Randomization
{
	public static class WeightedRandom
	{
		private class Flagable<T>
		{
			public T obj;
			public bool Flag = false;
		}

		static Random _rnd = Shuffler.ThreadSafeRandom.ThisThreadsRandom;

		/// <summary>
		/// Randomly select specified number of items from the list.
		/// The items with higher weight are in the result with higher probability
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items"></param>
		/// <param name="weight"></param>
		/// <returns></returns>
		public static IList<T> WeightedChoice<T>(this IList<T> items, int selectCount, Func<T, double> weight)
		{
			if (selectCount < 1 || selectCount > items.Count)
				throw new ArgumentOutOfRangeException("The size of the selection must be a positive number equal or less than the size of the list");

			if (selectCount == items.Count)
				return items;

			items.Shuffle();

			List<Flagable<T>> _list = ConvertToFlagable(items);

			var result = new List<Flagable<T>>();
			while (selectCount > 0)
			{
				var selected = SelectItems(_list, weight, selectCount);
				selectCount -= selected.Count;
				result.AddRange(selected);
				_list.RemoveAll(item => item.Flag == true); //remove those that are already selected
			}

			return result.Select(item => item.obj).ToList<T>();
		}

		private static List<Flagable<T>> ConvertToFlagable<T>(IList<T> items)
		{
			var result = new List<Flagable<T>>();
			foreach (var item in items)
			{
				result.Add(new Flagable<T>() { obj = item, Flag = false });
			}
			return result;
		}

		private static IList<Flagable<T>> SelectItems<T>(IList<Flagable<T>> items, Func<T, double> weight, int selectCount)
		{
			if (selectCount == 0) return new List<Flagable<T>>(); //recursive base case

			double sum = items.Where(item => item.Flag == false).Sum(item => weight(item.obj));
			double chunk = sum / selectCount;
			double rnd = _rnd.NextDouble() * chunk;


			for (int i = 0; i < items.Count; i++)
			{
				if (rnd < weight(items[i].obj))
				{
					items[i].Flag = true;
					rnd += chunk;
					if (--selectCount == 0) break; // when selected enough then stop
				}
				else
					rnd -= weight(items[i].obj);
				if (rnd < 0) rnd += ((int)(-rnd / chunk) + 1) * chunk; // to skip pieces that are greater than chunk size
				if (rnd < 0) rnd += ((int)(-rnd / chunk) + 1) * chunk; // to skipp pieaces that are greater than chunk size
			}
			return items.Where(item => item.Flag == true).ToList<Flagable<T>>();
		}

		public static int WeightedPick<T>(this IList<T> items, Func<T, double> weight)
		{
			Debug.Assert(items.All(i => weight(i) >= 0), "All weights must be non-negative.");
			double sum = items.Select(i => weight(i)).Sum();
			if (sum == 0) return _rnd.Next(0, items.Count());

			double rnd = _rnd.NextDouble() * sum;

			for (int i = 0; i < items.Count(); i++)
			{
				if (rnd < weight(items[i]))
				{
					return i;
				}
				rnd -= weight(items[i]);
			}
			Debug.Assert(false, "should never get here");
			return -1;
		}

		public static int WeightedPick(this int[] weights)
		{
			Debug.Assert(!weights.Any(w => w < 0), "All weights must be non-negative.");
			int sum = weights.Sum();
			if (sum == 0) return _rnd.Next(0, weights.Length);

			int rnd = _rnd.Next(0, sum);
			for (int i = 0; i < weights.Length; i++)
			{
				if (rnd < weights[i])
				{
					return i;
				}
				rnd -= weights[i];
			}
			Debug.Assert(false, "should never get here");
			return -1;
		}

		public static int WeightedPick(this double[] weights)
		{
			Debug.Assert(!weights.Any(w => w < 0), "All weights must be non-negative.");
			double sum = weights.Sum();
			if (sum == 0) return _rnd.Next(0, weights.Length);

			double rnd = _rnd.NextDouble() * sum;
			for (int i = 0; i < weights.Length; i++)
			{
				if (rnd < weights[i])
				{
					return i;
				}
				rnd -= weights[i];
			}
			Debug.Assert(false, "should never get here");
			return -1;
		}
	}
}
