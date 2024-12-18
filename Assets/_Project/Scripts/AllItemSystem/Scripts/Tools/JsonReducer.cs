using System;
using System.Text.RegularExpressions;

namespace Tools
{
	public static class JsonReducer
	{
		private const string ArraysPattern = @""".*""\:\s\[\]((\,)|(?=\n\s+\}))";
		private const string ObjectsPattern = @""".*""\:\s""""((\,)|(?=\n\s+\}))";
		private const string ZeroIntPattern = @""".*""\:\s+0((\,)|(?=\n\s+\}))";
		private const string ZeroFloatPattern = @""".*""\:\s+0\.0((\,)|(?=\n\s+\}))";
		private const string FalsePattern = @""".*""\:\s+false((\,)|(?=\n\s+\}))";
		private const string EmptyObjectPattern = @""".*""\:\s*\{(\n\s*)\}((\,)|(?=\n\s+\}))";
		private const string EmptyLinesPattern = @"^\s+$[\r\n]*";
		private const string CommaPattern = @"(\,)(?=\n\s*\})";
		private const string PivotPatternTemplate = @"(?<!(_imgformat_)(.*\n){3}\s+)(""Pivot""\:\s\{\n\s+""x""\:\s0.0\,\n\s+""y""\:\s0.0\n\s+\})";
		
		public static string Reduce(string jsonString, bool cleanZeroValues = true, bool cleanFalseValues = true, params string[] objectNamesToClear)
		{
			string s = CleanEmptyString(jsonString);
			
			if (cleanZeroValues)
			{
				s = CleanZeros(s);
			}
			s = CleanEmptyString(s);
			
			if (cleanFalseValues)
			{
				s = CleanFalse(s);
			}
			s = CleanEmptyString(s);
			
			s = CleanArrays(s);
			s = CleanEmptyString(s);
			
			s = CleanObjects(s);
			s = CleanEmptyString(s);

			if (objectNamesToClear.Length == 0)
			{
				s = CleanEmptyObjects(s);
			}
			else
			{
				s = CleanEmptyNamedObjects(s, objectNamesToClear);
			}
			s = CleanEmptyString(s);
			
			s = CleanCommas(s);

			return s;
		}

		public static string ReduceResources(string jsonString, string[] imagesFormats)
		{
			string pivotPattern = MakePivotPattern(imagesFormats);
			string s = CleanPivot(jsonString, pivotPattern);
			s = CleanArrays(s);
			s = CleanObjects(s);
			s = CleanEmptyString(s);
			s = CleanCommas(s);
			return s;
		}

		private static string MakePivotPattern(string[] imagesFormats)
		{
			string substitute = String.Join("|", imagesFormats);
			return PivotPatternTemplate.Replace("_imgformat_", substitute);
		}

		private static string CleanArrays(string jsonString)
		{
			return Regex.Replace(jsonString, ArraysPattern, string.Empty);
		}

		private static string CleanObjects(string jsonString)
		{
			return Regex.Replace(jsonString, ObjectsPattern, string.Empty);
		}

		private static string CleanZeros(string jsonString)
		{
			string s = Regex.Replace(jsonString, ZeroIntPattern, string.Empty);
			return Regex.Replace(s, ZeroFloatPattern, string.Empty);
		}
		
		private static string CleanFalse(string jsonString)
		{
			return Regex.Replace(jsonString, FalsePattern, string.Empty);
		}
		
		private static string CleanEmptyObjects(string jsonString, string pattern = EmptyObjectPattern)
		{
			string s = jsonString;
			Regex regex = new Regex(pattern);
			while (regex.IsMatch(s))
			{
				s = Regex.Replace(s, pattern, string.Empty);
			}
			
			return s;
		}
		
		private static string CleanEmptyNamedObjects(string jsonString, string[] objectNames)
		{
			string substitute = $@"""{objectNames[0]}""";
			for (int i = 1; i < objectNames.Length; i++)
			{
				substitute += "|" + $@"""{objectNames[i]}""";
			}

			substitute = "(" + substitute + ")";
			string pattern = EmptyObjectPattern.Replace(@""".*""", substitute);
			
			return CleanEmptyObjects(jsonString, pattern);
		}
		
		private static string CleanCommas(string jsonString)
		{
			return Regex.Replace(jsonString, CommaPattern, string.Empty);
		}

		private static string CleanEmptyString(string jsonString)
		{
			return Regex.Replace(jsonString, EmptyLinesPattern, string.Empty, RegexOptions.Multiline);
		}

		private static string CleanPivot(string jsonString, string pivotPattern)
		{
			return Regex.Replace(jsonString, pivotPattern, string.Empty);
		}
	}
}