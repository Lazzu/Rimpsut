using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace rimpsut
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var lines = new List<Line> {
				new Line ("E8 KS JF -QR -NL -KR"),
				new Line ("KS ZD E8 FC WY E7 -L3 -CH -EJ"),
				new Line ("L3 ZD JF FC WY XJ P5 -NL -V3"),
				new Line ("PZ WY V3 KR"),
			};

			var resultLine = new Line ("E8 KS JF ZD FC WY E7 XJ P5 PZ KR");

			var solver = new Solver ();

			Stopwatch sw = new Stopwatch ();


			sw.Start ();

			solver.Solve (lines, resultLine);

			sw.Stop ();

			Debug.WriteLine ("Search took {0}h {1}min {2}s {3}ms",
			                 sw.Elapsed.Hours,
			                 sw.Elapsed.Minutes,
			                 sw.Elapsed.Seconds,
			                 sw.Elapsed.Milliseconds);

			Debug.WriteLine ("With {0} lines to search.", lines.Count);

			//Debug.WriteLine (lines [0] + lines [2] + lines [4] + lines [7] /* + lines [0] + lines [1] + lines [0]*/);


		}
	}
}
