using ClangSharp;
using ClangSharp.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrataDocGen.Helpers
{
	public class Macro
	{
		static readonly string[] IgnoredMacroCharacters =
		{
			",",
			"(",
			")"
		};

		public MacroExpansion CMacro { get; private set; }
		public string Name { get; private set; }
		public List<string> Parameters { get; private set; }

		public Macro(MacroExpansion macroEx, string name, IEnumerable<string> parameters)
		{
			CMacro = macroEx;
			Name = name;
			Parameters = parameters.ToList();
		}

		public static Macro ParseMacro(CXTranslationUnit tu, MacroExpansion macroEx)
		{
			// Tokenize and get the result of the macro
			var tokens = tu.Tokenize(macroEx.Extent);
			var cont = new List<string>();

			foreach (var token in tokens)
			{
				var spelling = token.GetSpelling(tu).CString;

				// Are we an ignored character?
				if (Array.IndexOf(IgnoredMacroCharacters, spelling.Substring(0)) > -1)
					continue;

				// Remove extra ""'s before adding them to the list
				spelling = spelling.Replace("\"", "");

				cont.Add(spelling);
			}

			var macro = new Macro(macroEx, cont[0], cont.Skip(1));

			return macro;
		}
	}
}
