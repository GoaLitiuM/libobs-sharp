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

using System.Reflection;
using System.Windows.Forms;

namespace test.Utility
{
	public static class WinFormsHelper
	{
		public static void DoubleBufferControl(Control control)
		{
			//WinForms doesn't use double buffering by default and our controls
			//requires constant refreshing so we force double buffering for
			//those controls who are most prone to flickering.

			//optimize for remote desktop by not enabling double buffering
			if (SystemInformation.TerminalServerSession)
				return;

			//use reflection to enable double buffering on control
			typeof(Control).InvokeMember("DoubleBuffered",
				BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
				null, control, new object[] { true });
		}
	}
}