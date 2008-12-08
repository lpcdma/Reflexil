﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision: 1951 $</version>
// </file>

using System;

namespace ICSharpCode.SharpDevelop.Dom
{
	[Flags]
	public enum ConversionFlags
	{
		/// <summary>
		/// Convert only the name.
		/// </summary>
		None = 0,
		/// <summary>
		/// Show the parameter list
		/// </summary>
		ShowParameterList      = 1,
		/// <summary>
		/// Show names for parameters
		/// </summary>
		ShowParameterNames     = 2,
		/// <summary>
		/// Show the accessibility (private, public, etc.)
		/// </summary>
		ShowAccessibility      = 4,
		/// <summary>
		/// Show the definition key word (class, struct, Sub, Function, etc.)
		/// </summary>
		ShowDefinitionKeyWord  = 8,
		/// <summary>
		/// Show the fully qualified name for the member
		/// </summary>
		UseFullyQualifiedMemberNames = 0x10,
		/// <summary>
		/// Show modifiers (virtual, override, etc.)
		/// </summary>
		ShowModifiers          = 0x20,
		/// <summary>
		/// Show the inheritance declaration
		/// </summary>
		ShowInheritanceList    = 0x40,
		
		IncludeHtmlMarkup      = 0x80,
		/// <summary>
		/// Show the return type
		/// </summary>
		ShowReturnType = 0x100,
		/// <summary>
		/// Use fully qualified names for return type and parameters.
		/// </summary>
		UseFullyQualifiedTypeNames = 0x200,
		/// <summary>
		/// Include opening brace (or equivalent) for methods or classes;
		/// or semicolon (or equivalent) for field, events.
		/// For properties, a block indicating if there is a getter/setter is included.
		/// </summary>
		IncludeBody = 0x400,
		/// <summary>
		/// Show the list of type parameters on method and class declarations.
		/// Type arguments for parameter/return types are always shown.
		/// </summary>
		ShowTypeParameterList = 0x800,
		
		StandardConversionFlags = ShowParameterNames |
			ShowAccessibility |
			ShowParameterList |
			ShowReturnType |
			ShowModifiers |
			ShowTypeParameterList |
			ShowDefinitionKeyWord,
		
		All = 0xfff,
	}
	
	public interface IAmbience
	{
		ConversionFlags ConversionFlags {
			get;
			set;
		}
		
		string Convert(IClass c);
		string ConvertEnd(IClass c);
		
		string Convert(IField field);
		string Convert(IProperty property);
		string Convert(IEvent e);
		
		string Convert(IMethod m);
		string ConvertEnd(IMethod m);
		
		string Convert(IParameter param);
		string Convert(IReturnType returnType);
		
		string WrapAttribute(string attribute);
		string WrapComment(string comment);
		
		string GetIntrinsicTypeName(string dotNetTypeName);
	}
	
	public abstract class AbstractAmbience : IAmbience
	{
		#if DEBUG
		int ownerThread = System.Threading.Thread.CurrentThread.ManagedThreadId;
		#endif
		
		[System.Diagnostics.Conditional("DEBUG")]
		protected void CheckThread()
		{
			#if DEBUG
			if (ownerThread != System.Threading.Thread.CurrentThread.ManagedThreadId)
				throw new Exception("Ambience may only be used by the thread that created it");
			#endif
		}
		
		ConversionFlags conversionFlags = ConversionFlags.StandardConversionFlags;
		
		public ConversionFlags ConversionFlags {
			get {
				return conversionFlags;
			}
			set {
				CheckThread();
				conversionFlags = value;
			}
		}
		
		public bool ShowReturnType {
			get {
				return (conversionFlags & ConversionFlags.ShowReturnType) == ConversionFlags.ShowReturnType;
			}
		}
		
		public bool ShowAccessibility {
			get {
				return (conversionFlags & ConversionFlags.ShowAccessibility) == ConversionFlags.ShowAccessibility;
			}
		}
		
		public bool ShowParameterNames {
			get {
				return (conversionFlags & ConversionFlags.ShowParameterNames) == ConversionFlags.ShowParameterNames;
			}
		}
		
		public bool UseFullyQualifiedTypeNames {
			get {
				return (conversionFlags & ConversionFlags.UseFullyQualifiedTypeNames) == ConversionFlags.UseFullyQualifiedTypeNames;
			}
		}
		
		public bool ShowDefinitionKeyWord {
			get {
				return (conversionFlags & ConversionFlags.ShowDefinitionKeyWord) == ConversionFlags.ShowDefinitionKeyWord;
			}
		}
		
		public bool ShowParameterList {
			get {
				return (conversionFlags & ConversionFlags.ShowParameterList) == ConversionFlags.ShowParameterList;
			}
		}
		
		public bool ShowModifiers {
			get {
				return (conversionFlags & ConversionFlags.ShowModifiers) == ConversionFlags.ShowModifiers;
			}
		}
		
		public bool ShowInheritanceList {
			get {
				return (conversionFlags & ConversionFlags.ShowInheritanceList) == ConversionFlags.ShowInheritanceList;
			}
		}
		
		public bool IncludeHtmlMarkup {
			get {
				return (conversionFlags & ConversionFlags.IncludeHtmlMarkup) == ConversionFlags.IncludeHtmlMarkup;
			}
		}
		
		public bool UseFullyQualifiedMemberNames {
			get {
				return (conversionFlags & ConversionFlags.UseFullyQualifiedMemberNames) == ConversionFlags.UseFullyQualifiedMemberNames;
			}
		}
		
		public bool IncludeBody {
			get {
				return (conversionFlags & ConversionFlags.IncludeBody) == ConversionFlags.IncludeBody;
			}
		}
		
		public bool ShowTypeParameterList {
			get {
				return (conversionFlags & ConversionFlags.ShowTypeParameterList) == ConversionFlags.ShowTypeParameterList;
			}
		}
		
		public abstract string Convert(IClass c);
		public abstract string ConvertEnd(IClass c);
		public abstract string Convert(IField c);
		public abstract string Convert(IProperty property);
		public abstract string Convert(IEvent e);
		public abstract string Convert(IMethod m);
		public abstract string ConvertEnd(IMethod m);
		public abstract string Convert(IParameter param);
		public abstract string Convert(IReturnType returnType);
		
		public abstract string WrapAttribute(string attribute);
		public abstract string WrapComment(string comment);
		public abstract string GetIntrinsicTypeName(string dotNetTypeName);
	}
}