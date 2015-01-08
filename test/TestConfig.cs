/***************************************************************************
	Copyright (C) 2014-2015 by Nick Thijssen <lamah83@gmail.com>
	
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
using System.Windows.Forms;

namespace test
{
	public partial class TestConfig : Form
	{
		public TestConfig()
		{
			InitializeComponent();
			PopulateForm();
		}

		~TestConfig()
		{
			if (this.DialogResult == DialogResult.OK)
			{
				// pump settings to obslib
			}						
		}

		private void PopulateForm()
		{
			// insert obs service retrieval
			// insert audio device retrieval
			// insert video adapter retrieval

			// dont wanna do databinding
			// just populate the dropdown box with the service / server objects 
			// give it a ToString() override to return the service display name
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
