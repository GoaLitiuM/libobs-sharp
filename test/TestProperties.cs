/***************************************************************************
	Copyright (C) 2014 by Nick Thijssen <lamah83@gmail.com>
	
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
using System.Drawing;
using System.Windows.Forms;
using OBS;

namespace test
{
	public partial class TestProperties : Form
	{
		public ObsSource Source;
		public Panel previewPanel;	//TODO: remove

		private TestProperties()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Create a property dialog for an existing source
		/// </summary>
		/// <param name="source">Source of type ObsSource</param>
		public TestProperties(ObsSource source)
			: this()
		{
			Source = source;
			_properties = Obs.GetSourceProperties(source);
		}

		private ObsProperty[] _properties;

		private void TestProperties_Load(object sender, EventArgs e)
		{
			GenerateControls();
			InitPreview(propertyPanel.Controls[0].Width, propertyPanel.Controls[0].Width, this.Handle);
		}
		public class ComboboxItem
		{
			public ComboboxItem(string text, object value)
			{
				Text = text;
				Value = value;
			}

			public string Text { get; set; }
			public object Value { get; set; }

			public override string ToString()
			{
				return Text;
			}
		}

		private void GenerateControls()
		{
			// initialize table layout
			propertyPanel.RowStyles.Clear();
			propertyPanel.RowCount = _properties.Length + 1;
			propertyPanel.AutoSize = true;

			// add preview panel
			propertyPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

			previewPanel = new Panel();
			previewPanel.Enabled = true;
			previewPanel.Visible = true;
			propertyPanel.Controls.Add(previewPanel, 1, 0);

			for (var i = 0; i < _properties.Length; i++)
			{
				propertyPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
				ObsProperty property = _properties[i];

				Label label = new Label
				{
					Text = property.Description,
					TextAlign = ContentAlignment.MiddleRight,
					AutoSize = true,
					Dock = DockStyle.Fill,
					Visible = property.Visible
				};
				propertyPanel.Controls.Add(label, 0, i+1);

				Control control = null;
				switch (property.Type)
				{
					case ObsPropertyType.Bool:
						{
							CheckBox checkbox = new CheckBox
							{
								Text = ""
							};

							control = checkbox;
							break;
						}
					case ObsPropertyType.Int:
						{
							NumericUpDown numeric = new NumericUpDown
							{
								Minimum = property.IntMin,
								Maximum = property.IntMax,
								Increment = property.IntStep,
								DecimalPlaces = 0
							};

							control = numeric;
							break;
						}
					case ObsPropertyType.Float:
						{
							NumericUpDown numeric = new NumericUpDown
							{
								Minimum = (decimal)property.FloatMin,
								Maximum = (decimal)property.FloatMax,
								Increment = (decimal)property.FloatStep,
								DecimalPlaces = 2
							};

							control = numeric;
							break;
						}
					case ObsPropertyType.Text:
						{
							TextBox textbox = new TextBox();

							switch (property.TextType)
							{
								case ObsTextType.Default:
									{
										// nothing?
										break;
									}
								case ObsTextType.Password:
									{
										textbox.PasswordChar = '*';
										break;
									}
								case ObsTextType.Multiline:
									{
										textbox.Multiline = true;
										break;
									}
							}

							control = textbox;
							break;
						}
					case ObsPropertyType.Path:
						{
							TextBox textbox = new TextBox
							{
								Width = 300
							};

							Button button = new Button
							{
								Text = "Browse..."
							};

							switch (property.PathType)
							{
								case ObsPathType.File:
									{
										// File Filter encoding
										//
										// obs encoding
										// *   "Example types 1 and 2 (*.ex1 *.ex2);;Example type 3 (*.ex3)"	
										//
										// .net encoding
										// Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*

										string filter = string.Empty;
										string[] filters = property.PathFilter.Split(
											new[]
										{
											';'
										},
											StringSplitOptions.RemoveEmptyEntries);

										foreach (var s in filters)
										{
											string mask = s.Split('(')[1].Split(')')[0].Replace(' ', ';');
											filter += string.Format("{0}|{1}|", s, mask);
										}
										filter = filter.Remove(filter.LastIndexOf('|'));

										// this is pretty yucky, someone make a nice regex!

										OpenFileDialog filedialog = new OpenFileDialog
										{
											AutoUpgradeEnabled = true,
											Filter = filter,
											InitialDirectory = property.PathDefault,
											FilterIndex = 1
										};

										button.Click += delegate
										{
											if (filedialog.ShowDialog(this) == DialogResult.OK)
											{
												textbox.Text = filedialog.FileName;
											}
										};
										break;
									}
								case ObsPathType.Directory:
									{
										FolderBrowserDialog folderdialog = new FolderBrowserDialog
										{
											SelectedPath = property.PathDefault
										};

										button.Click += delegate
										{
											if (folderdialog.ShowDialog(this) == DialogResult.OK)
											{
												textbox.Text = folderdialog.SelectedPath;
											}
										};
										break;
									}
							}

							FlowLayoutPanel panel = new FlowLayoutPanel
							{
								Margin = new Padding(0),
								Padding = new Padding(0),
								Dock = DockStyle.Fill,
								WrapContents = false,
								AutoSize = true,
								AutoSizeMode = AutoSizeMode.GrowAndShrink
							};

							panel.Controls.Add(textbox);
							panel.Controls.Add(button);

							control = panel;
							break;
						}
					case ObsPropertyType.List:
						{
							ComboBox combobox = new ComboBox();

							string[] namelist = property.GetListItemNames();
							string[] valuelist = property.GetListItemValues();

							var items = new List<ComboboxItem>();

							for (var index = 0; index < property.ListItemCount; index++)
							{
								ComboboxItem item = new ComboboxItem(namelist[index], valuelist[index]);
								items.Add(item);
							}

							combobox.Items.AddRange(items.ToArray());
							
							if (combobox.Items.Count > 0)
							{
								combobox.SelectedIndex = 0;
							}

							switch (property.ListType)
							{
								case ObsComboType.Invalid:
									{
										combobox = null;

										break;
									}
								case ObsComboType.Editable:
									{
										combobox.Width = 300;

										break;
									}
								case ObsComboType.List:
									{
										combobox.DropDownStyle = ComboBoxStyle.DropDownList;
										combobox.Width = 300;

										break;
									}
							}

							// insert populate and default selection
							if (combobox != null)
							{
								combobox.MouseHover += delegate
								{
									debugTextBox.Text =
										((ComboboxItem) combobox.Items[combobox.SelectedIndex]).Value.ToString();
								};
								control = combobox;
							}
							break;
						}
					case ObsPropertyType.Color:
						{
							Label colorlabel = new Label
							{
								Text = "Insert ColorName Here and set forecolor to it. Maybe add hex code #FFFFFF",
								ForeColor = Color.Red
							};

							Button button = new Button
							{
								Text = "Browse..."
							};

							FlowLayoutPanel panel = new FlowLayoutPanel
							{
								Margin = new Padding(0),
								Padding = new Padding(0),
								Dock = DockStyle.Fill,
								WrapContents = false,
								AutoSize = true,
								AutoSizeMode = AutoSizeMode.GrowAndShrink
							};
							panel.Controls.Add(colorlabel);
							panel.Controls.Add(button);

							control = panel;
							break;
						}
					case ObsPropertyType.Button:
						{
							Button button = new Button
							{
								Text = property.Description
							};

							button.Click += delegate
							{
								MessageBox.Show("Insert Appropriate Dialog Here");
							};

							control = button;
							break;
						}
					case ObsPropertyType.Font:
						{
							Button button = new Button
							{
								Text = "Browse..."
							};

							button.Click += delegate
							{
								MessageBox.Show("Insert Font Dialog Here");
							};

							control = button;
							break;
						}
					case ObsPropertyType.Invalid:
					default:
						{
							MessageBox.Show(string.Format("Error while trying to create controls for property {0}", property.Description));
							Close();
							break;
						}
				}

				if (control != null)
				{
					control.Enabled = property.Enabled;
					control.Visible = property.Visible;
					control.Tag = property.Name;
					propertyPanel.Controls.Add(control, 1, i+1);
				}

			}
			// TODO: check if this extra row is really needed
			propertyPanel.RowStyles.Add(new RowStyle());
			propertyPanel.Refresh();
		}

		private void TestProperties_FormClosed(object sender, FormClosedEventArgs e)
		{
			ClosePreview();
		}
	}
}
