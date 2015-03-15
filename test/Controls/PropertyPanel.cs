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

using OBS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace test.Controls
{
	public partial class PropertyPanel : UserControl
	{
		public PropertyPanel(ObsProperty property, ObsData data)
		{
			InitializeComponent();

			List<Control> controls = new List<Control>();
			string name = property.Description;
			switch (property.Type)
			{
				case ObsPropertyType.Bool:
					{
						name = String.Empty;
						CheckBox checkbox = new CheckBox
						{
							Text = property.Description,
							TextAlign = ContentAlignment.MiddleLeft,
							Width = 300,
							Checked = data.GetBool(property.Name)
						};
						checkbox.CheckedChanged += (sender, args) => data.SetBool(property.Name, checkbox.Checked);

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
							DecimalPlaces = 0,
							Value = data.GetInt(property.Name)
						};
						numeric.ValueChanged += (sender, args) => data.SetInt(property.Name, (int)numeric.Value);
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
							DecimalPlaces = 2,
							Value = (decimal)data.GetDouble(property.Name)
						};
						numeric.ValueChanged += (sender, args) => data.SetDouble(property.Name, (double)numeric.Value);
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
						textbox.Text = data.GetString(property.Name);
						textbox.TextChanged += (sender, args) => data.SetString(property.Name, textbox.Text);
						controls.Add(textbox);
						break;
					}
				case ObsPropertyType.Path:
					{
						TextBox textbox = new TextBox
						{
							Width = 300,
							Text = data.GetString(property.Name)
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

									string filter = String.Empty;
									string[] filters = property.PathFilter.Split(
										new[]
									{
										';'
									},
										StringSplitOptions.RemoveEmptyEntries);

									foreach (var s in filters)
									{
										string mask = s.Split('(')[1].Split(')')[0].Replace(' ', ';');
										filter += String.Format("{0}|{1}|", s, mask);
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
						textbox.TextChanged += (sender, args) => data.SetString(property.Name, textbox.Text);
						controls.Add(textbox);
						controls.Add(button);
						break;
					}
				case ObsPropertyType.List:
					{
						ComboBox combobox = new ComboBox();

						string[] namelist = property.GetListItemNames();
						string[] valuelist = property.GetListItemValues();

						if (namelist.Length > 0)
						{
							combobox.Items.AddRange(namelist.ToArray());
							combobox.SelectedIndex = 0;
						}
						else
						{
							controls.Add(combobox);
							break;
						}

						switch (property.ListType)
						{
							case ObsComboType.Invalid:
								{
									return;
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

						// TODO: this set goes ok, but window capture doesnt properly grab the window soooo i dunno? :P
						switch (property.ListFormat)
						{
							case ObsComboFormat.Float:
								{
									combobox.SelectedIndex = Array.IndexOf(valuelist, data.GetDouble(property.Name));
									combobox.SelectedIndexChanged += (sender, args) => data.SetDouble(property.Name, Convert.ToDouble(valuelist[combobox.SelectedIndex]));
									break;
								}
							case ObsComboFormat.Int:
								{
									combobox.SelectedIndex = Array.IndexOf(valuelist, data.GetInt(property.Name));
									combobox.SelectedIndexChanged += (sender, args) => data.SetInt(property.Name, Convert.ToInt32(valuelist[combobox.SelectedIndex]));
									break;
								}
							case ObsComboFormat.String:
								{
									var value = data.GetString(property.Name);
									combobox.SelectedIndex = !string.IsNullOrEmpty(value) ? Array.IndexOf(valuelist, value) : 0;
									combobox.SelectedIndexChanged += (sender, args) => data.SetString(property.Name, valuelist[combobox.SelectedIndex]);
									break;
								}
						}

						controls.Add(combobox);
						break;
					}
				case ObsPropertyType.Color:
					{
						Color color = Color.FromArgb((int)data.GetInt(property.Name));
						TextBox textbox = new TextBox
						{
							Text = "#FFFFFF",
							ForeColor = color,
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
								color = ColorTranslator.FromHtml(textbox.Text);
								textbox.ForeColor = color;
								data.SetInt(property.Name, color.ToArgb());
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
						// TODO: how do i get the apropriate dll call here and fire it!
						button.Click += (sender, args) => MessageBox.Show("Insert Appropriate Dialog Here");

						controls.Add(button);
						break;
					}
				case ObsPropertyType.Font:
					{
						// TODO: need a GetFont / SetFont here i think :?
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
								data.SetString(property.Name, font.Name);
								font = new Font(font.Name, 25f);
								label.Font = font;
								label.Text = font.Name;
							}
						};

						controls.Add(label);
						controls.Add(button);
						break;
					}
				default:
					{
						throw new Exception(String.Format("Error while trying to create controls for property {0}", property.Description));
					}
			}

			nameLabel.Text = name;

			foreach (var control in controls)
			{
				controlPanel.Controls.Add(control);
			}

			foreach (Control control in controlPanel.Controls)
			{
				int topmargin = (controlPanel.Height - control.Height) / 2;
				Padding oldmargin = control.Margin;
				oldmargin.Top = topmargin;
				oldmargin.Bottom = topmargin;
				control.Margin = oldmargin;
			}
		}
	}
}