/***************************************************************************
	Copyright (C) 2014-2015 by Ari Vuollet <ari.vuollet@kapsi.fi>

	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
	as published by the Free Software Foundation; either version 2
	of the License, or (at your option) any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program; if not, see <http://www.gnu.org/licenses/>.
***************************************************************************/

/*  Original implementation by Richard Prinz 2007-2009
	http://www.codeproject.com/Articles/19274/A-printf-implementation-in-C
	Licensed under The MIT License. */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OBS.Utility
{
	public static class Printf
	{
		/// <summary> Determines whether the specified value is of numeric type. </summary>
		/// <param name="o"> The object to check. </param>
		public static bool IsNumericType(object o)
		{
			return (o is byte ||
				o is sbyte ||
				o is short ||
				o is ushort ||
				o is int ||
				o is uint ||
				o is long ||
				o is ulong ||
				o is float ||
				o is double ||
				o is decimal);
		}

		/// <summary> Determines whether the specified value is positive. </summary>
		/// <param name="value"> The value. </param>
		/// <param name="zeroIsPositive"> Treat value 0 as positive. </param>
		public static bool IsPositive(object value, bool zeroIsPositive)
		{
			switch (Type.GetTypeCode(value.GetType()))
			{
				case TypeCode.SByte:
					return (zeroIsPositive ? (sbyte)value >= 0 : (sbyte)value > 0);
				case TypeCode.Int16:
					return (zeroIsPositive ? (short)value >= 0 : (short)value > 0);
				case TypeCode.Int32:
					return (zeroIsPositive ? (int)value >= 0 : (int)value > 0);
				case TypeCode.Int64:
					return (zeroIsPositive ? (long)value >= 0 : (long)value > 0);
				case TypeCode.Single:
					return (zeroIsPositive ? (float)value >= 0 : (float)value > 0);
				case TypeCode.Double:
					return (zeroIsPositive ? (double)value >= 0 : (double)value > 0);
				case TypeCode.Decimal:
					return (zeroIsPositive ? (decimal)value >= 0 : (decimal)value > 0);
				case TypeCode.Byte:
					return (zeroIsPositive ? true : (byte)value > 0);
				case TypeCode.UInt16:
					return (zeroIsPositive ? true : (ushort)value > 0);
				case TypeCode.UInt32:
					return (zeroIsPositive ? true : (uint)value > 0);
				case TypeCode.UInt64:
					return (zeroIsPositive ? true : (ulong)value > 0);
				case TypeCode.Char:
					return (zeroIsPositive ? true : (char)value != '\0');
				default:
					return false;
			}
		}

		/// <summary> Converts the boxed type to its corresponding unsigned type. </summary>
		/// <param name="value"> The value. </param>
		public static object ToUnsigned(object value)
		{
			TypeCode code = Type.GetTypeCode(value.GetType());
			switch (code)
			{
				case TypeCode.SByte:
					return (byte)((sbyte)value);
				case TypeCode.Int16:
					return (ushort)((short)value);
				case TypeCode.Int32:
					return (uint)((int)value);
				case TypeCode.Int64:
					return (ulong)((long)value);
				case TypeCode.Single:
					return (UInt32)((float)value);
				case TypeCode.Double:
					return (ulong)((double)value);
				case TypeCode.Decimal:
					return (ulong)((decimal)value);
				case TypeCode.Byte:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
					return value;
				default:
					return null;
			}
		}

		/// <summary> Converts the boxed type to integer type </summary>
		/// <param name="value"> The value. </param>
		/// <param name="round"> Round floating-point values to nearest integer. </param>
		public static object ToInteger(object value, bool round)
		{
			switch (Type.GetTypeCode(value.GetType()))
			{
				case TypeCode.SByte:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Byte:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
					return value;

				case TypeCode.Single:
					return (round ? (int)Math.Round((float)value) : (int)((float)value));
				case TypeCode.Double:
					return (round ? (long)Math.Round((double)value) : (long)((double)value));
				case TypeCode.Decimal:
					return (round ? Math.Round((decimal)value) : (decimal)value);

				default:
					return null;
			}
		}

		public static int UnboxToInt(object value, bool round)
		{
			switch (Type.GetTypeCode(value.GetType()))
			{
				case TypeCode.SByte:
					return (int)((sbyte)value);
				case TypeCode.Int16:
					return (int)((short)value);
				case TypeCode.Int32:
					return (int)((int)value);
				case TypeCode.Int64:
					return (int)Convert.ToInt32(value);

				case TypeCode.Byte:
					return (int)((byte)value);
				case TypeCode.UInt16:
					return (int)((ushort)value);
				case TypeCode.UInt32:
					return (int)((uint)value);
				case TypeCode.UInt64:
					return (int)((ulong)Convert.ToUInt32(value));

				case TypeCode.Single:
					return (round ? (int)Math.Round((float)value) : (int)((float)value));
				case TypeCode.Double:
					return (round ? (int)Math.Round((double)value) : (int)((double)value));
				case TypeCode.Decimal:
					return (round ? (int)Math.Round((decimal)value) : (int)((decimal)value));

				default:
					return 0;
			}
		}

		public static void printf(string format, params object[] args)
		{
			Console.Write(Printf.sprintf(format, args));
		}

		public static void fprintf(TextWriter dest, string format, params object[] args)
		{
			dest.Write(Printf.sprintf(format, args));
		}

		public static void puts(string format)
		{
			Console.WriteLine(format);
		}

		public static void fputs(TextWriter dest, string format)
		{
			dest.WriteLine(format);
		}

		public static string[] GetFormatSpecifiers(string format)
		{
			if (format.IndexOf('%') == -1)
				return null;

			// find specifiers from format string 
			List<int> indices = new List<int>();
			for (int j = 0; j < format.Length; j++)
			{
				j = format.IndexOf('%', j);

				if (j == -1)
					break;

				indices.Add(j);

				if (format[j + 1] == '%') // ignore "%%"
					j++;
			}

			if (indices.Count == 0)
				return null;

			List<string> formats = new List<string>(indices.Count);
			for (int mi = 0; mi < indices.Count; mi++)
			{
				string formatSpecifier = format.Substring(indices[mi], (mi + 1 < indices.Count ? indices[mi + 1] : format.Length) - indices[mi]);
				if (!string.IsNullOrWhiteSpace(formatSpecifier))
					formats.Add(formatSpecifier);
			}

			return formats.ToArray();
		}

		public class FormatSpecificationInfo
		{
			public string specification;
			//public int parameter;
			public char type;
			public int width;
			public int precision;
			public FormatFlags flags;
		};

		[Flags]
		public enum FormatFlags
		{
			// Type length 
			IsLong = 0x0001,        // l
			IsLongLong = 0x0002,    // ll
			IsShort = 0x0004,       // h
			IsChar = 0x0008,        // hh
			IsLongDouble = 0x0016,  // L

			// Flags 
			LeftAlign = 0x0100,     // '-' left align within the width
			Sign = 0x0200,          // '+' use - or + signs for signed types
			Alternate = 0x0400,     // '#' prefix non-zero values with hex types
			ZeroPad = 0x0800,       // '0' pad with zeros
			Blank = 0x1000,         // ' ' pad sign with blank
			Grouping = 0x2000,      // '\' group by thousands
			ArchSize = 0x4000,      // '?' use arch precision

			// Dynamic parameters 
			DynamicWidth = 0x10000,
			DynamicPrecision = 0x20000,
		};

		public static FormatSpecificationInfo GetFormatSpecifierInfo(string specification)
		{
			if (string.IsNullOrWhiteSpace(specification))
				return null;

			FormatSpecificationInfo info = new FormatSpecificationInfo()
			{
				type = '\0',
				width = int.MinValue,
				precision = 6,
			};

			string width = "";
			string precision = "";
			int start = -1;
			int fsLength = 1;

			// TODO: parse parameter index 

			for (int i = 0; i < specification.Length && info.type == '\0'; i++)
			{
				char c = specification[i];

				switch (c)
				{
					case '%':
						if (start == -1)
							start = i;
						else
							info.type = c;
							info.specification = specification.Substring(start, i + 1 - start);
							fsLength = i + 1;
						break;

					// flags
					case '-':
						info.flags |= FormatFlags.LeftAlign;
						break;
					case '+':
						info.flags |= FormatFlags.Sign;
						break;
					case ' ':
						info.flags |= FormatFlags.Blank;
						break;
					case '#':
						info.flags |= FormatFlags.Alternate;
						break;
					case '\'':
						info.flags |= FormatFlags.Grouping;
						break;
					case '?':
						info.flags |= FormatFlags.ArchSize;
						break;
						
					// precision
					case '.':
						{
							for (int j = i + 1; j < specification.Length; j++)
							{
								if (specification[j] == '*')
									info.flags |= FormatFlags.DynamicPrecision;
								else if (char.IsNumber(specification[j]))
									precision += specification[j];
								else
									break;

								i++;
							}
						}
						break;

					// length flags
					case 'h':
						info.flags += (int)FormatFlags.IsShort;
						break;
					case 'l':
						info.flags += (int)FormatFlags.IsLong;
						break;
					case 'L':
						info.flags |= FormatFlags.IsLongDouble;
						break;
					case 'z':
					case 'j':
					case 't':
						// not supported
						break;

					// dynamic width
					case '*':
						info.flags |= FormatFlags.DynamicWidth;
						break;

					default:
						{
							if (char.IsNumber(c))
							{
								if (width == "" && c == '0')
									info.flags |= FormatFlags.ZeroPad;
								else
									width += c;
							}
							else if (char.IsLetter(c) && info.type == '\0')
							{
								info.type = c;
								info.specification = specification.Substring(start, i + 1 - start);
								fsLength = i + 1;
							}
						}
						break;
				}
			}

			// sign overrides space
			if (info.flags.HasFlag(FormatFlags.Sign) && info.flags.HasFlag(FormatFlags.Blank))
				info.flags &= ~FormatFlags.Blank;

			if (info.flags.HasFlag(FormatFlags.LeftAlign) && info.flags.HasFlag(FormatFlags.ZeroPad))
				info.flags &= ~FormatFlags.ZeroPad;

			// unsupported precision for these types
			if (info.type == 's' ||
				info.type == 'c' ||
				Char.ToUpper(info.type) == 'X' ||
				info.type == 'o')
			{
				info.precision = int.MinValue;
			}

			if (!string.IsNullOrWhiteSpace(precision))
				info.precision = Convert.ToInt32(precision);
			if (!string.IsNullOrWhiteSpace(width))
				info.width = Convert.ToInt32(width);

			return info;
		}

		public static string sprintf(string format, params object[] args)
		{
			string[] formatSpecifiers = GetFormatSpecifiers(format);
			if (formatSpecifiers == null)
				return format;

			StringBuilder f = new StringBuilder(format);
			int defaultParamIx = 0;
			int position = format.IndexOf("%");
			int specifierPosition = 0;
			int shift = 0;

			// walkthrough every found specifier
			foreach (string spec in formatSpecifiers)
			{
				var info = GetFormatSpecifierInfo(spec);
				specifierPosition = format.IndexOf("%", specifierPosition);

				if (info.type == '\0')
					continue;

				int speclen = info.specification.Length;
				position = specifierPosition + shift;
				specifierPosition += speclen;

				bool flagAlternate = info.flags.HasFlag(FormatFlags.Alternate);
				bool flagLeftToRight = info.flags.HasFlag(FormatFlags.LeftAlign);
				bool flagPositiveSign = info.flags.HasFlag(FormatFlags.Sign);
				bool flagPositiveSpace = info.flags.HasFlag(FormatFlags.Blank);
				bool flagZeroPadding = info.flags.HasFlag(FormatFlags.ZeroPad);
				bool flagGroupThousands = info.flags.HasFlag(FormatFlags.Grouping);

				char paddingCharacter = flagZeroPadding ? '0' : ' ';
				int fieldLength = info.width;
				int fieldPrecision = info.precision;
				char formatSpecifier = info.type;
				string w = String.Empty;
				int paramIx = defaultParamIx;
				object o = null;

				if (info.flags.HasFlag(FormatFlags.DynamicWidth))
				{
					fieldLength = (int)args[paramIx];
					paramIx++;
					defaultParamIx++;
				}
				if (info.flags.HasFlag(FormatFlags.ArchSize))
					fieldLength = IntPtr.Size * 2;

				if (info.flags.HasFlag(FormatFlags.DynamicPrecision))
				{
					fieldPrecision = (int)args[paramIx];
					paramIx++;
					defaultParamIx++;
				}

				// get next value parameter and convert value parameter depending on short/long indicator
				if (args != null && paramIx < args.Length)
					o = args[paramIx];

				// convert value parameters to a string depending on the formatSpecifier
				switch (formatSpecifier)
				{
					case '%':   // % character
						w = "%";
						break;
					case 'd':	// integer
					case 'i':
						w = FormatNumber((flagGroupThousands ? "n" : "d"), flagAlternate,
										fieldLength, int.MinValue, flagLeftToRight,
										flagPositiveSign, flagPositiveSpace,
										paddingCharacter, o);
						break;
					case 'o':	// octal integer - no leading zero
						w = FormatOct("o", flagAlternate,
										fieldLength, int.MinValue, flagLeftToRight,
										paddingCharacter, o);
						break;
					case 'x':   // hex integer - no leading zero
						w = FormatHex("x", flagAlternate,
										fieldLength, fieldPrecision, flagLeftToRight,
										paddingCharacter, o);
						break;
					case 'X':	// same as x but with capital hex characters
						w = FormatHex("X", flagAlternate,
										fieldLength, fieldPrecision, flagLeftToRight,
										paddingCharacter, o);
						break;
					case 'u':	// unsigned integer
						w = FormatNumber((flagGroupThousands ? "n" : "d"), flagAlternate,
										fieldLength, int.MinValue, flagLeftToRight,
										false, false,
										paddingCharacter, ToUnsigned(o));
						break;
					case 'c':	// character
						if (IsNumericType(o))
							w = Convert.ToChar(o).ToString();
						else if (o is char)
							w = ((char)o).ToString();
						else if (o is string && ((string)o).Length > 0)
							w = ((string)o)[0].ToString();
						break;
					case 's':	// string
						w = (o != null) ? o.ToString() : "(null)";
						if (fieldPrecision >= 0)
							w = w.Substring(0, fieldPrecision);

						if (fieldLength != int.MinValue)
							if (flagLeftToRight)
								w = w.PadRight(fieldLength, paddingCharacter);
							else
								w = w.PadLeft(fieldLength, paddingCharacter);
						break;
					case 'f':	// double
						w = FormatNumber((flagGroupThousands ? "n" : "f"), flagAlternate,
										fieldLength, fieldPrecision, flagLeftToRight,
										flagPositiveSign, flagPositiveSpace,
										paddingCharacter, o);
						break;
					case 'e':	// double / exponent
						w = FormatNumber("e", flagAlternate,
										fieldLength, fieldPrecision, flagLeftToRight,
										flagPositiveSign, flagPositiveSpace,
										paddingCharacter, o);
						break;
					case 'E':	// double / exponent
						w = FormatNumber("E", flagAlternate,
										fieldLength, fieldPrecision, flagLeftToRight,
										flagPositiveSign, flagPositiveSpace,
										paddingCharacter, o);
						break;
					case 'g':	// double / exponent
						w = FormatNumber("g", flagAlternate,
										fieldLength, fieldPrecision, flagLeftToRight,
										flagPositiveSign, flagPositiveSpace,
										paddingCharacter, o);
						break;
					case 'G':	// double / exponent
						w = FormatNumber("G", flagAlternate,
										fieldLength, fieldPrecision, flagLeftToRight,
										flagPositiveSign, flagPositiveSpace,
										paddingCharacter, o);
						break;
					case 'p':	// pointer
						if (o is IntPtr)
							w = "0x" + ((IntPtr)o).ToString("x");
						break;
					case 'n':	// number of characters so far
						w = FormatNumber("d", flagAlternate,
										fieldLength, int.MinValue, flagLeftToRight,
										flagPositiveSign, flagPositiveSpace,
										paddingCharacter, f.Length);
						break;
					default:
						//continue;
						break;
				}

				// replace format parameter with parameter value
				f.Remove(position, speclen);
				f.Insert(position, w);
				shift += w.Length - speclen;

				// skip specifiers with no parameters
				if (formatSpecifier == 'n' || formatSpecifier == '%')
					continue;

				defaultParamIx++;
			}

			return f.ToString();
		}

		private static string FormatOct(string specifier, bool alternate,
											int fieldLength, int fieldPrecision,
											bool leftToRight,
											char padding, object value)
		{
			if (!IsNumericType(value))
				return String.Empty;

			string w = Convert.ToString(UnboxToInt(value, true), 8);

			if (leftToRight || padding == ' ')
			{
				if (alternate && w != "0")
					w = "0" + w;
				if (fieldLength != int.MinValue)
					w = String.Format("{0," + (leftToRight ? "-" : String.Empty) + fieldLength.ToString() + "}", w);
			}
			else
			{
				if (fieldLength != int.MinValue)
					w = w.PadLeft(fieldLength - (alternate && w != "0" ? 1 : 0), padding);
				if (alternate && w != "0")
					w = "0" + w;
			}

			return w;
		}

		private static string FormatHex(string specifier, bool alternate,
											int fieldLength, int fieldPrecision,
											bool leftToRight,
											char padding, object value)
		{
			if (!IsNumericType(value))
				return String.Empty;

			string w = String.Format("{0:" + specifier + (fieldPrecision != int.MinValue ?
											fieldPrecision.ToString() :
											String.Empty) + "}", value);

			if (leftToRight || padding == ' ')
			{
				if (alternate)
					w = (specifier == "x" ? "0x" : "0X") + w;
				if (fieldLength != int.MinValue)
					w = String.Format("{0," + (leftToRight ? "-" : String.Empty) + fieldLength.ToString() + "}", w);
			}
			else
			{
				if (fieldPrecision != int.MinValue && fieldPrecision < fieldLength)
					padding = ' ';
				if (fieldLength != int.MinValue)
					w = w.PadLeft(fieldLength - (alternate ? 2 : 0), padding);
				if (alternate)
					w = (specifier == "x" ? "0x" : "0X") + w;
			}

			return w;
		}

		private static string FormatNumber(string specifier, bool alternate,
											int fieldLength, int fieldPrecision,
											bool leftToRight,
											bool positiveSign, bool positiveSpace,
											char padding, object value)
		{
			if (!IsNumericType(value))
				return String.Empty;

			string w = String.Empty;

			if (fieldPrecision == int.MinValue)
			{
				if (specifier == "d" && value is double)
					w = ((int)(double)value).ToString();
				else
					w = value.ToString();
			}
			else
			{
				string numberFormat;
				if (Char.ToUpper(specifier[0]) == 'E')
					numberFormat = "{0:0." + new string('0', fieldPrecision) + specifier + "+00}";
				else
					numberFormat = "{0:" + specifier + fieldPrecision.ToString() + "}";

				if (numberFormat.Contains("d") && value is double)
					w = String.Format(numberFormat, (int)(double)value);
				else
					w = String.Format(numberFormat, value);
			}

			if (leftToRight || padding == ' ')
			{
				if (IsPositive(value, true))
					w = (positiveSign ?
							"+" : (positiveSpace ? " " : String.Empty)) + w;
				if (fieldLength != int.MinValue)
					w = String.Format("{0," + (leftToRight ? "-" : String.Empty) + fieldLength.ToString() + "}", w);
			}
			else
			{
				if (w.StartsWith("-"))
					w = w.Substring(1);
				if (fieldLength != int.MinValue)
					w = w.PadLeft(fieldLength - 1, padding);
				if (IsPositive(value, true))
					w = (positiveSign ?
							"+" : (positiveSpace ?
									" " : (fieldLength != int.MinValue && w.Length < fieldLength ?
											padding.ToString() : String.Empty))) + w;
				else
					w = "-" + w;
			}

			return w;
		}
	}
}