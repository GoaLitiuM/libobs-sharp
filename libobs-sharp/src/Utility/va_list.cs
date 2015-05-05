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

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace OBS.Utility
{
	public class va_list : IDisposable
	{
		internal IntPtr instance;    //unmanaged pointer to va_list

		// pointers to allocated unmanaged memory
		private List<IntPtr> allocPtrs = new List<IntPtr>();

		public va_list(IntPtr ptr)
		{
			instance = ptr;
		}

		public va_list(object[] args)
		{
			// small valuetypes (char, short) are always promoted to int
			// so the offset is always incremented by at least size of int

			// calculate total size for allocation
			int totalSize = 0;
			foreach (object obj in args)
			{
				Type type = obj.GetType();
				int size = (type == typeof(string)) ? Marshal.SizeOf(typeof(IntPtr)) :
					Math.Max(Marshal.SizeOf(type), Marshal.SizeOf(typeof(Int32)));
				totalSize += size;
			}

			if (totalSize == 0)
				return;

			instance = Marshal.AllocHGlobal(totalSize);
			allocPtrs.Add(instance);

			// write objects to unmanaged memory
			int offset = 0;
			foreach (object obj in args)
			{
				Type type = obj.GetType();
				int size = (type == typeof(string)) ? Marshal.SizeOf(typeof(IntPtr)) : Marshal.SizeOf(type);

				if (type == typeof(string))
				{
					IntPtr s = Marshal.StringToHGlobalAnsi((string)obj);
					Marshal.WriteIntPtr(instance, offset, s);
					allocPtrs.Add(s);
				}
				else
				{
					byte[] bytes = null;
					int boff = 0;

					if (type == typeof(float))
						bytes = BitConverter.GetBytes((double)(float)obj);
					else if (type == typeof(double))
						bytes = BitConverter.GetBytes((float)obj);
					else if (type == typeof(char))
						bytes = BitConverter.GetBytes((char)obj);
					else if (type == typeof(byte))
						bytes = BitConverter.GetBytes((byte)obj);
					else if (type == typeof(sbyte))
						bytes = BitConverter.GetBytes((sbyte)obj);
					else if (type == typeof(Int16))
						bytes = BitConverter.GetBytes((Int16)obj);
					else if (type == typeof(UInt16))
						bytes = BitConverter.GetBytes((UInt16)obj);
					else if (type == typeof(Int32))
						bytes = BitConverter.GetBytes((Int32)obj);
					else if (type == typeof(UInt32))
						bytes = BitConverter.GetBytes((UInt32)obj);
					else if (type == typeof(Int64))
						bytes = BitConverter.GetBytes((Int64)obj);
					else if (type == typeof(UInt64))
						bytes = BitConverter.GetBytes((UInt64)obj);
					else if (type == typeof(bool))
						bytes = BitConverter.GetBytes((bool)obj);

					foreach (byte b in bytes)
						Marshal.WriteByte(instance, offset + boff++, b);
				}

				offset += Math.Max(Marshal.SizeOf(typeof(IntPtr)), size);
			}
		}

		public void Dispose()
		{
			foreach (IntPtr rel in allocPtrs)
				Marshal.FreeHGlobal(rel);

			allocPtrs.Clear();
			instance = IntPtr.Zero;
		}

		/// <summary> Returns unmanaged pointer to argument list. </summary>
		public IntPtr GetPointer()
		{
			return instance;
		}

		/// <summary> Returns array of objects with help of printf format string. </summary>
		/// <param name="msg"> printf format string. </param>
		public object[] GetObjectsByFormat(string format)
		{
			return GetObjectsByFormat(format, this);
		}

		/// <summary>
		/// Returns array of objects read from va_list with help of printf format string.
		/// </summary>
		/// <param name="msg"> printf format string. </param>
		/// <param name="args"> va_list of function parameters. </param>
		public static unsafe object[] GetObjectsByFormat(string format, va_list va_list)
		{
			string[] formatSpecifiers = Printf.GetFormatSpecifiers(format);
			if (formatSpecifiers == null || va_list == null || va_list.GetPointer() == IntPtr.Zero)
				return null;

			IntPtr args = va_list.GetPointer();
			List<object> objects = new List<object>(formatSpecifiers.Length);

			int offset = 0;
			foreach (string spec in formatSpecifiers)
			{
				var info = Printf.GetFormatSpecifierInfo(spec);
				if (info.type == '\0')
					continue;

				// dynamic width and precision arguments
				// these are stored in stack before the actual value
				if (info.flags.HasFlag(Printf.FormatFlags.DynamicWidth))
				{
					int widthArg = Marshal.ReadInt32(args, offset);
					objects.Add(widthArg);
					offset += Marshal.SizeOf(typeof(IntPtr));
				}
				if (info.flags.HasFlag(Printf.FormatFlags.DynamicPrecision))
				{
					int precArg = Marshal.ReadInt32(args, offset);
					objects.Add(precArg);
					offset += Marshal.SizeOf(typeof(IntPtr));
				}

				int iSize = info.flags.HasFlag(Printf.FormatFlags.IsLongLong)
					? Marshal.SizeOf(typeof(Int64)) : Marshal.SizeOf(typeof(IntPtr));

				// marshal objects from pointer
				switch (info.type)
				{
					// 8/16-bit integers
					// char / wchar_t (promoted to int)
					case 'c':
						char c = (char)Marshal.ReadByte(args, offset);
						objects.Add(c);
						offset += Marshal.SizeOf(typeof(Int32));
						break;

					// signed integers
					case 'd':
					case 'i':
						{
							if (info.flags.HasFlag(Printf.FormatFlags.IsShort)) // h
							{
								short sh = (short)Marshal.ReadInt32(args, offset);
								objects.Add(sh);
								offset += Marshal.SizeOf(typeof(Int32));
							}
							else if (info.flags.HasFlag(Printf.FormatFlags.IsLongLong)) // ll
							{
								long l = Marshal.ReadInt64(args, offset);
								objects.Add(l);
								offset += iSize;
							}
							else // int and long types
							{
								int i = Marshal.ReadInt32(args, offset);
								objects.Add(i);
								offset += iSize;
							}
						}
						break;

					// unsigned integers
					case 'u':
					case 'o':
					case 'x':
					case 'X':
						{
							if (info.flags.HasFlag(Printf.FormatFlags.IsShort)) // h
							{
								ushort su = (ushort)Marshal.ReadInt32(args, offset);
								objects.Add(su);
								offset += Marshal.SizeOf(typeof(Int32));
							}
							else if (info.flags.HasFlag(Printf.FormatFlags.IsLongLong)) // ll
							{
								ulong lu = (ulong)(long)Marshal.ReadInt64(args, offset);
								objects.Add(lu);
								offset += iSize;
							}
							else // uint and ulong types
							{
								uint u = (uint)Marshal.ReadInt32(args, offset);
								objects.Add(u);
								offset += iSize;
							}
						}
						break;

					// floating-point types
					case 'f':
					case 'F':
					case 'e':
					case 'E':
					case 'g':
					case 'G':
						{
							if (info.flags.HasFlag(Printf.FormatFlags.IsLongDouble))  // L
							{
								// not really supported but read it as long
								long lfi = Marshal.ReadInt64(args, offset);
								double d = *(double*)(void*)&lfi;
								objects.Add(d);
								offset += Marshal.SizeOf(typeof(double));
							}
							else // double
							{
								long lfi = Marshal.ReadInt64(args, offset);
								double d = *(double*)(void*)&lfi;
								objects.Add(d);
								offset += Marshal.SizeOf(typeof(double));
							}
						}
						break;

					// string
					case 's':
						{
							string s = null;

							if (info.flags.HasFlag(Printf.FormatFlags.IsLong))
								s = Marshal.PtrToStringUni(Marshal.ReadIntPtr(args, offset));
							else
								s = Marshal.PtrToStringAnsi(Marshal.ReadIntPtr(args, offset));

							objects.Add(s);
							offset += Marshal.SizeOf(typeof(IntPtr));
						}
						break;

					// pointer
					case 'p':
						IntPtr ptr = Marshal.ReadIntPtr(args, offset);
						objects.Add(ptr);
						offset += Marshal.SizeOf(typeof(IntPtr));
						break;

					// non-marshallable types, ignored
					case ' ':
					case '%':
					case 'n':
						break;

					default:
						throw new ApplicationException("printf specifier '%" + info.type + "' not supported");
				}
			}

			return objects.ToArray();
		}
	}
}