using System;
using System.Collections.Generic;

namespace rimpsut
{
	/// <summary>
	/// Quick and dirty crap to let the programmer type less. He's lazy.
	/// </summary>
	public static class helpers
	{
		public static void Foreach<T> (this IEnumerable<T> list, Action<T> a)
		{
			foreach (var item in list)
			{
				a (item);
			}
		}
	}
}

