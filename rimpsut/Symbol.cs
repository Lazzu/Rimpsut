using System;
using System.Collections.Generic;

namespace rimpsut
{
	public class Symbol
	{
		public string Characters
		{
			get;
			protected set;
		}

		public bool Negative
		{
			get;
			protected set;
		}

		public Symbol (string symbol)
		{
			Negative = false;

			if (symbol.Contains ("-"))
			{
				Negative = true;
				symbol = symbol.Substring (1);
			}

			Characters = symbol;

		}

		public override bool Equals (object obj)
		{
			var symbol = obj as Symbol;
			return symbol != null ? Equals (symbol) : false;
		}

		public bool Equals (Symbol symbol)
		{
			return Characters == symbol.Characters && Negative == symbol.Negative;
		}

		public override int GetHashCode ()
		{
			return Characters.GetHashCode () * Negative.GetHashCode ();
		}
	}
}

