using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace rimpsut
{
	public class Solver
	{

		public List<Line> Solve (List<Line> lines, Line result)
		{
			var closed = new List<Line> ();
			var open = new List<Line> ();
			var cameFrom = new Dictionary<Line, Line> ();

			lines = FilterLines (lines, result);

			open.AddRange (lines);

			// Starting scores
			foreach (var item in open)
			{
				item.Score = result.DifferenceScore (item);
			}

			for (int i = 0; i < lines.Count; i++)
			{
				for (int ii = 0; ii < lines.Count; ii++)
				{
					var newLine = lines [i] + lines [ii];
					open.Add (newLine);
					newLine.Score = result.DifferenceScore (newLine);
					if (newLine.Score == 0)
					{
						Debug.WriteLine ("Early victory! Found it during warm-up");
						return null;
					}
				}
			}

			open.Sort ((a, b) => a.Score - b.Score);

			// Why not just use while(true) here?
			while (open.Count > 0)
			{
				var current = GetCheapest (open);

				closed.Add (current);

				for (int i = 0; i < lines.Count; i++)
				{
					var newLine = current + lines [i];

					if (closed.Contains (newLine))
						continue;

					var score = result.DifferenceScore (newLine);

					newLine.Score = score;

					//Console.WriteLine ("Current score: {0}", score);

					if (score == 0)
					{
						Debug.WriteLine ("Victory!");
						Debug.WriteLine ("Tested combinations: {0}\nBacklog left: {1}", closed.Count, open.Count);
						return null;
					}

					//cameFrom [newLine] = current;
					open.Add (newLine);
				}

				open.Sort ((a, b) => a.Score - b.Score);
			}

			Console.WriteLine ("wat");

			return null;
		}

		public Line GetCheapest (List<Line> lines)
		{
			Line cheapest = lines [0];

			lines.RemoveAt (0);

			return cheapest;
		}

		/// <summary>
		/// Pre-filter the lines so that it takes out of those lines that will definitely not be used in the final path
		/// </summary>
		/// <returns>The lines.</returns>
		/// <param name="lines">Lines.</param>
		/// <param name="target">Target.</param>
		List<Line> FilterLines (List<Line> lines, Line target)
		{
			return lines;
		}

	}
}

