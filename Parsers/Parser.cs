using StrataDocGen.Helpers;
using ClangSharp;
using ClangSharp.Interop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StrataDocGen.Parsers
{
	public class Parser
	{
		public CXTranslationUnit CXTranslationUnit { get; private set; }
		public TranslationUnit TranslationUnit { get; private set; }
		public TranslationUnitDecl TUDecl { get; private set; }

		protected JsonSerializerOptions JsonOptions = new JsonSerializerOptions
		{
			WriteIndented = true
		};

		public Parser(CXTranslationUnit tu)
		{
			CXTranslationUnit = tu;
			TranslationUnit = TranslationUnit.GetOrCreate(tu);
			TUDecl = TranslationUnit.TranslationUnitDecl;
		}

		public virtual void WriteToJson()
		{
			if (!Directory.Exists("./docs"))
				Directory.CreateDirectory("./docs");
		}

		// Tokenize macro and return a list of spellings without grammar characters
		public Macro ParseMacro(MacroExpansion macroEx)
		{
			var macro = Macro.ParseMacro(CXTranslationUnit, macroEx);
			return macro;
		}

		public IEnumerable<MacroExpansion> GetMacrosBetweenLocations()
		{
			var cont = new List<MacroExpansion>();
			return cont;
		}
	}
}
