using System;
using System.Collections.Generic;

namespace rimpsut
{
	public class Line
	{
		public List<Symbol> Symbols
		{
			get;
			protected set;
		}

		public int Score
		{
			get;
			set;
		}

		public Line ()
		{
			Symbols = new List<Symbol> ();
			Score = int.MaxValue;
		}

		public Line (string line) : this ()
		{
			var symbols = line.Split (' ');
			foreach (var item in symbols)
			{
				Symbols.Add (new Symbol (item));
			}
		}

		public Line (IEnumerable<Symbol> symbols)
		{
			Score = int.MaxValue;
			Symbols = new List<Symbol> (symbols);
		}

		public int DifferenceScore (Line other)
		{
			int score = 0;
			int count = 0;
			foreach (var symbol in Symbols)
			{
				for (int i = count; i < other.Symbols.Count; i++)
				{
					var compared = other.Symbols [i];

					if (symbol.Characters == compared.Characters && symbol.Negative == compared.Negative)
					{
						break;
					}

					score += (i + 1);
				}
				count++;
			}
			return score * Math.Max (1, Math.Max (other.Symbols.Count - Symbols.Count, Symbols.Count - other.Symbols.Count));
		}

		public Line (Line line)
		{
			Symbols = new List<Symbol> (line.Symbols);
		}

		public override bool Equals (object obj)
		{
			var line = obj as Line;
			return line != null ? Equals (line) : false;
		}

		public override int GetHashCode ()
		{
			int hashcode = 7;
			for (int i = 0; i < Symbols.Count; i++)
			{
				hashcode *= Symbols [i].GetHashCode ();
			}
			return hashcode;
		}

		public bool Equals (Line other)
		{
			if (Symbols.Count != other.Symbols.Count)
				return false;

			for (int i = 0; i < Symbols.Count; i++)
			{
				var a = Symbols [i];
				var b = other.Symbols [i];

				if (a.Characters != b.Characters || a.Negative != b.Negative)
					return false;
			}

			return true;
		}

		// For conveniency
		public static Line operator + (Line lhs, Line rhs)
		{
			return Line.Add (lhs, rhs);
		}

		/// <summary>
		/// Add two lines together
		/// </summary>
		/// <param name="line">Line.</param>
		public static Line Add (Line lhs, Line rhs)
		{
			// Add lhs line to the output
			Line output = new Line (lhs.Symbols);

			// Go through all symbols from the rhs list
			for (int i = 0; i < rhs.Symbols.Count; i++)
			{
				// Get the current symbol that is being added
				var current = rhs.Symbols [i];

				// Boolean to detect if we wanted to break the inner loop early
				var earlyBreak = false; 

				for (int ii = 0; ii < output.Symbols.Count; ii++)
				{
					// Get the symbol to compare against
					var compared = output.Symbols [ii];

					// Check if they are not the same symbols
					if (current.Characters != compared.Characters)
						continue;

					// We will not be adding the current symbol to the output list, so set this here
					earlyBreak = true;

					if (current.Negative != compared.Negative)
					{
						// Remove from the output line, because the symbols negate each other
						output.Symbols.RemoveAt (ii);
					}

					break;
				}

				// Check if we wanted to end the loop early
				if (earlyBreak)
					continue;

				// Add the symbol to the output
				output.Symbols.Add (current);
			}

			return output;
		}

		public override string ToString ()
		{
			var symbols = "";
			var converted = Symbols.ConvertAll (s => (s.Negative ? "-" : "") + s.Characters + " ");
			foreach (var item in converted)
			{
				symbols += item;
			}
			return string.Format ("[Line: Symbols={0}]", symbols);
		}
	}


}

