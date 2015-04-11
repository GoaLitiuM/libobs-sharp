/***************************************************************************
	Copyright (C) 2014-2015 by Nick Thijssen <lamah83@gmail.com>
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

using OBS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace test.Controls
{
	public partial class PropertyControl : UserControl
	{
		private Action<bool> _modifiedDelegate;

		public PropertyControl(ObsProperty property, ObsData setting, Action<bool> modifiedDelegate = null)
		{
			InitializeComponent();
			_modifiedDelegate = modifiedDelegate;

			List<Control> controls = new List<Control>();

			string description = property.Description;
			ObsPropertyType type = property.Type;

			switch (type)
			{
				case ObsPropertyType.Bool:
					{
						description = String.Empty;
						CheckBox checkbox = new CheckBox
						{
							Text = property.Description,
							TextAlign = ContentAlignment.MiddleLeft,
							Width = 300,
							Checked = setting.GetBool(property.Name)
						};

						checkbox.CheckedChanged += (sender, args) =>
						{
							setting.SetBool(property.Name, checkbox.Checked);
							PropertyChanged(property, setting);
						};

						controls.Add(checkbox);
						break;
					}
				case ObsPropertyType.Int:
				case ObsPropertyType.Float:
					{
						NumericUpDown numeric = new NumericUpDown
						{
							Width = 300,
							DecimalPlaces = 0
						};

						if (type == ObsPropertyType.Int)
						{
							int intMin = property.IntMin;
							int intMax = property.IntMax;
							long intValue = setting.GetInt(property.Name);
							intValue = Math.Max(Math.Min(intValue, intMax), intMin);

							numeric.Minimum = intMin;
							numeric.Maximum = intMax;
							numeric.Increment = property.IntStep;
							numeric.Value = intValue;

							numeric.ValueChanged += (sender, args) =>
							{
								setting.SetInt(property.Name, (int)numeric.Value);
								PropertyChanged(property, setting);
							};
						}
						else if (type == ObsPropertyType.Float)
						{
							double floatMin = property.FloatMin;
							double floatMax = property.FloatMax;
							double floatValue = setting.GetDouble(property.Name);
							floatValue = Math.Max(Math.Min(floatValue, floatMax), floatMin);

							numeric.DecimalPlaces = 2;
							numeric.Minimum = (decimal)floatMin;
							numeric.Maximum = (decimal)floatMax;
							numeric.Increment = (decimal)property.FloatStep;
							numeric.Value = (decimal)floatValue;

							numeric.ValueChanged += (sender, args) =>
							{
								setting.SetDouble(property.Name, (double)numeric.Value);
								PropertyChanged(property, setting);
							};
						}

						if (property.IntType == ObsNumberType.Slider)
						{
							//TODO: implement horizontal slider + numeric box control setup, insert slider before numeric box
						}

						controls.Add(numeric);
						break;
					}
				case ObsPropertyType.Text:
					{
						TextBox textbox = new TextBox
						{
							Width = 300,
							Text = setting.GetString(property.Name)
						};

						switch (property.TextType)
						{
							case ObsTextType.Default:
								{
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
									textbox.Height *= 3;
									break;
								}
						}

						textbox.TextChanged += (sender, args) =>
						{
							setting.SetString(property.Name, textbox.Text);
							PropertyChanged(property, setting);
						};

						controls.Add(textbox);
						break;
					}
				case ObsPropertyType.Path:
					{
						TextBox textbox = new TextBox
						{
							Width = 300,
							Text = setting.GetString(property.Name)
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
									// Qt encoding examples
									// "Image Files (*.BMP *.JPG *.GIF);;All files (*.*)"
									// "Image Files (*.BMP;*.JPG;*.GIF);;*.*"
									//
									// .NET encoding example
									// "Image Files (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*"

									string filter = String.Empty;
									string[] groups = property.PathFilter.Split(new string[] { ";;" }, StringSplitOptions.RemoveEmptyEntries);

									foreach (string group in groups)
									{
										//captures description and pattern from Qt-encoded file filter group
										Match match = new Regex(@"^([^\(]+)\(([^)]+)").Match(group);
										if (match.Success)
										{
											string pattern = match.Groups[2].Value.Replace(' ', ';');
											filter += String.Format("{0}|{1}|", group, pattern);
										}
										else
										{
											//group itself is the pattern
											filter += String.Format("{0}|{0}|", group);
										}
									}
									filter = filter.Remove(filter.LastIndexOf('|'));

									OpenFileDialog dialog = new OpenFileDialog
									{
										AutoUpgradeEnabled = true,
										Filter = filter,
										InitialDirectory = property.PathDefault,
										FilterIndex = 1
									};

									button.Click += (sender, args) =>
									{
										if (dialog.ShowDialog(this) == DialogResult.OK)
										{
											textbox.Text = dialog.FileName;
										}
									};
									break;
								}
							case ObsPathType.Directory:
								{
									FolderBrowserDialog dialog = new FolderBrowserDialog
									{
										SelectedPath = property.PathDefault
									};

									button.Click += (sender, args) =>
									{
										if (dialog.ShowDialog(this) == DialogResult.OK)
										{
											textbox.Text = dialog.SelectedPath;
										}
									};
									break;
								}
						}

						textbox.TextChanged += (sender, args) =>
						{
							setting.SetString(property.Name, textbox.Text);
							PropertyChanged(property, setting);
						};

						controls.Add(textbox);
						controls.Add(button);
						break;
					}
				case ObsPropertyType.List:
					{
						ComboBox combobox = new ComboBox()
						{
							Width = 300
						};

						string[] namelist = property.GetListItemNames();
						string[] valuelist = property.GetListItemValues();

						combobox.Items.AddRange(namelist.ToArray());

						switch (property.ListType)
						{
							case ObsComboType.Editable:
								{
									break;
								}
							case ObsComboType.List:
								{
									combobox.DropDownStyle = ComboBoxStyle.DropDownList;
									break;
								}
						}

						int index = 0;
						switch (property.ListFormat)
						{
							case ObsComboFormat.Float:
								{
									index = Array.IndexOf(valuelist, setting.GetDouble(property.Name));

									combobox.SelectedIndexChanged += (sender, args) =>
									{
										setting.SetDouble(property.Name, Convert.ToDouble(valuelist[combobox.SelectedIndex]));
										PropertyChanged(property, setting);
									};
									break;
								}
							case ObsComboFormat.Int:
								{
									index = Array.IndexOf(valuelist, setting.GetInt(property.Name));

									combobox.SelectedIndexChanged += (sender, args) =>
									{
										setting.SetInt(property.Name, Convert.ToInt32(valuelist[combobox.SelectedIndex]));
										PropertyChanged(property, setting);
									};
									break;
								}
							case ObsComboFormat.String:
								{
									index = Array.IndexOf(valuelist, setting.GetString(property.Name));

									combobox.SelectedIndexChanged += (sender, args) =>
									{
										setting.SetString(property.Name, valuelist[combobox.SelectedIndex]);
										PropertyChanged(property, setting);
									};
									break;
								}
						}

						//disallow empty selections
						if (index == -1 && combobox.Items.Count > 0)
							index = 0;

						combobox.SelectedIndex = index;

						controls.Add(combobox);
						break;
					}
				case ObsPropertyType.Color:
					{
						Color color = Color.FromArgb((int)setting.GetInt(property.Name));
						TextBox textbox = new TextBox
						{
							Text = "#" +
								color.R.ToString("X2") +
								color.G.ToString("X2") +
								color.B.ToString("X2"),
							ForeColor = color.GetBrightness() > 0.93 ? Color.Black : color,
							Width = 300,
							TextAlign = HorizontalAlignment.Center
						};

						Button button = new Button
						{
							Text = "Select..."
						};

						textbox.TextChanged += (sender, args) =>
						{
							try
							{
								color = ColorTranslator.FromHtml(textbox.Text);
								textbox.ForeColor = color.GetBrightness() > 0.93 ? Color.Black : color;

								setting.SetInt(property.Name, color.ToArgb());

								PropertyChanged(property, setting);
							}
							catch
							{
								// do ~nothing~
							}
						};

						button.Click += (sender, args) =>
						{
							var colorDialog = new ColorDialog
							{
								AllowFullOpen = true,
								AnyColor = true,
								FullOpen = true,
								Color = ColorTranslator.FromHtml(textbox.Text)
							};

							if (colorDialog.ShowDialog(this) == DialogResult.OK)
							{
								textbox.Text = "#" +
									colorDialog.Color.R.ToString("X2") +
									colorDialog.Color.G.ToString("X2") +
									colorDialog.Color.B.ToString("X2");
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

						button.Click += (sender, args) =>
						{
							//TODO: proper handling for property.ButtonClicked and callbacks
							MessageBox.Show("Unimplemented");
						};

						controls.Add(button);
						break;
					}
				case ObsPropertyType.Font:
					{
						Label label = new Label
						{
							BorderStyle = BorderStyle.Fixed3D,
							AutoSize = false,
							TextAlign = ContentAlignment.MiddleCenter,
							Font = new Font(FontFamily.GenericSansSerif, 25F),
							Height = 60,
							Width = 300
						};

						using (ObsData fontData = new ObsData(setting.GetObject(property.Name)))
						{
							string name = fontData.GetString("face");
							//string style = fontData.GetString("style");	//not supported in Windows
							ObsFontFlags flags = (ObsFontFlags)fontData.GetInt("flags");

							Font font = new Font(name, 25F, (FontStyle)flags);
							label.Font = font;
							label.Text = name;
						}

						Button button = new Button
						{
							Text = "Select..."
						};

						button.Click += (sender, args) =>
						{
							var fontDialog = new FontDialog();

							using (ObsData fontData = new ObsData(setting.GetObject(property.Name)))
							{
								float size = fontData.GetInt("size");
								fontDialog.Font = new Font(label.Font.FontFamily, size, label.Font.Style);
							}

							if (fontDialog.ShowDialog() == DialogResult.OK)
							{
								var font = fontDialog.Font;

								using (ObsData fontData = new ObsData(setting.GetObject(property.Name)))
								{
									fontData.SetString("face", font.Name.ToString());
									fontData.SetString("style", "");	//not supported in Windows
									fontData.SetInt("size", (int)font.SizeInPoints);
									fontData.SetInt("flags", (int)font.Style);
								}

								PropertyChanged(property, setting);

								font = new Font(font.Name, 25f, font.Style);
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
						throw new Exception(String.Format("Error, unimplemented property type for property {0}", property.Description));
					}
			}

			nameLabel.Text = description;

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

		private void PropertyChanged(ObsProperty property, ObsData setting)
		{
			bool refresh = property.Modified(setting);
			if (_modifiedDelegate != null)
				_modifiedDelegate(refresh);
		}
	}
}