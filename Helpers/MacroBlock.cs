using ClangSharp;
using ClangSharp.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosDocGen.Helpers
{
	public class MacroBlock
	{
		public MacroExpansion BeginMacro { get; private set; }
		public MacroExpansion EndMacro { get; private set; }

		public List<MacroExpansion> InnerMacros { get; private set; } = new List<MacroExpansion>();

		public MacroBlock(MacroExpansion begin, MacroExpansion end, CXTranslationUnit tu)
		{
			BeginMacro = begin;
			EndMacro = end;

			GetInnerMacros(tu);
		}

		public MacroBlock(Macro begin, Macro end, CXTranslationUnit tu)
		{
			BeginMacro = begin.CMacro;
			EndMacro = end.CMacro;

			GetInnerMacros(tu);
		}


		private void GetInnerMacros(CXTranslationUnit cxTU)
		{
			var tu = TranslationUnit.GetOrCreate(cxTU);
			var decl = tu.TranslationUnitDecl;

			var allMacros = decl.CursorChildren.OfType<MacroExpansion>();
			InnerMacros = allMacros.SkipWhile(x => x != BeginMacro).Skip(1).TakeWhile(x => x != EndMacro).ToList();
		}
	}
}
