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
using test.Controls;

namespace test
{
	public partial class TestProperties
	{
		public readonly ObsSource Source;

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

			// top panel ratio
			float backratio = (float)previewBackPanel.Width / previewBackPanel.Height;

			// source size
			int width = (int)Source.Width;
			int height = (int)Source.Height;

			// source ratio
			float previewratio = (float)width / height;

			// padding
			const int padding = 1;

			if (backratio > previewratio)
			{
				previewPanel.Height = height - padding;
				previewPanel.Width = (int)Math.Round(height * previewratio) - padding;
			}
			else
			{
				previewPanel.Width = width - padding;
				previewPanel.Height = (int)Math.Round(width / previewratio) - padding;
			}

			previewBackPanel.Height = (int)(previewPanel.Height * 1.25);
			previewBackPanel.Width = (int)(previewPanel.Width * 1.25);

			// Center previewpanel in backpanel
			var offsetx = (previewBackPanel.Width - previewPanel.Width) / 2;
			var offsety = (previewBackPanel.Height - previewPanel.Height) / 2;

			previewPanel.Location = new Point(offsetx, offsety);

			InitPreview(previewPanel.Width, previewPanel.Height, previewPanel.Handle);

			okButton.Click += (o, args) => Close();
			cancelButton.Click += (o, args) => Close();
		}
	
		private class ComboboxItem
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
			foreach (ObsProperty property in _properties)
			{
				List<Control> controls = new List<Control>();
				string name = property.Description;
				switch (property.Type)
				{
					case ObsPropertyType.Bool:
						{
							name = string.Empty;
							CheckBox checkbox = new CheckBox
							{
								Text = property.Description,
								TextAlign = ContentAlignment.MiddleLeft,
								Width = 300
							};

							controls.Add(checkbox);
							break;
						}
					case ObsPropertyType.Int:
						{
							NumericUpDown numeric = new NumericUpDown
							{
								Minimum = property.IntMin,
								Maximum = property.IntMax,
								Increment = property.IntStep,
								Width = 300,
								DecimalPlaces = 0
							};

							controls.Add(numeric);
							break;
						}
					case ObsPropertyType.Float:
						{
							NumericUpDown numeric = new NumericUpDown
							{
								Minimum = (decimal)property.FloatMin,
								Maximum = (decimal)property.FloatMax,
								Increment = (decimal)property.FloatStep,
								Width = 300,
								DecimalPlaces = 2
							};

							controls.Add(numeric);
							break;
						}
					case ObsPropertyType.Text:
						{
							TextBox textbox = new TextBox
							{
								Width = 300,
							};

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
										textbox.Height = 60;
										break;
									}
							}

							controls.Add(textbox);
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

										button.Click += (sender, args) =>
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

										button.Click += (sender, args) =>
										{
											if (folderdialog.ShowDialog(this) == DialogResult.OK)
											{
												textbox.Text = folderdialog.SelectedPath;
											}
										};
										break;
									}
							}

							controls.Add(textbox);
							controls.Add(button);
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
							controls.Add(combobox);
							break;
						}
					case ObsPropertyType.Color:
						{
							TextBox textbox = new TextBox
							{
								Text = "#FFFFFF",
								ForeColor = Color.Black,
								Width = 300,
								TextAlign = HorizontalAlignment.Center
							};

							Button button = new Button
							{
								Text = "Browse..."
							};

							var colordialog = new ColorDialog
							{
								AllowFullOpen = true,
								AnyColor = true,
								FullOpen = true,
								Color = textbox.ForeColor
							};

							textbox.TextChanged += (sender, args) =>
							{
								try
								{
									var color = ColorTranslator.FromHtml(textbox.Text);
									textbox.ForeColor = color;
								}
								catch
								{
									// do ~nothing~
								}
							};

							button.Click += (sender, args) =>
							{
								if (colordialog.ShowDialog(this) == DialogResult.OK)
								{
									textbox.ForeColor = colordialog.Color;
									textbox.Text = ColorTranslator.ToHtml(colordialog.Color);
								}
							};

							controls.Add(textbox);
							controls.Add(button);
							break;
						}
					case ObsPropertyType.Button:
						{
							Button button = new Button
							{
								Text = property.Description
							};

							button.Click += (sender, args) => MessageBox.Show("Insert Appropriate Dialog Here");

							controls.Add(button);
							break;
						}
					case ObsPropertyType.Font:
						{
							Label label = new Label
							{
								Text = "font",
								BorderStyle = BorderStyle.Fixed3D,
								AutoSize = false,
								TextAlign = ContentAlignment.MiddleCenter,
								Font = new Font(FontFamily.GenericSansSerif, 25F),
								Height = 60,
								Width = 300
							};

							Button button = new Button
							{
								Text = "Browse..."
							};

							button.Click += (sender, args) =>
							{
								var fontdialog = new FontDialog();
								if (fontdialog.ShowDialog() == DialogResult.OK)
								{
									var font = fontdialog.Font;
									font = new Font(font.FontFamily, 25f);
									label.Font = font;
								}
							};

							controls.Add(label);
							controls.Add(button);
							break;
						}
					default:
						{
							MessageBox.Show(string.Format("Error while trying to create controls for property {0}", property.Description));
							Close();
							break;
						}
				}

				var proppanel = new PropertyPanel(name, controls)
				{
					Enabled = property.Enabled,
					Visible = property.Visible,
					Tag = property.Name
				};

				propertyPanel.Controls.Add(proppanel);
			}
			propertyPanel.Refresh();
		}

		private void TestProperties_FormClosed(object sender, FormClosedEventArgs e)
		{
			ClosePreview();
		}
	}
}
