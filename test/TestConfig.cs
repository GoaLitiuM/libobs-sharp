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

		private void OKButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void CancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
