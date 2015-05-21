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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using OBS;

using test.Utility;

namespace test.Controls
{
	public class PropertyControl : FlowLayoutPanel
	{
		private PropertiesView view;

		public PropertyControl(PropertiesView view, ObsProperty property, ObsData setting)
		{
			SuspendLayout();
			AutoSize = true;
			Margin = new Padding(0, 1, 0, 1);
			Size = new Size(600, 25);
			ResumeLayout(false);
			
			this.view = view;
			
			DoubleBuffered = true;
			Padding = new Padding(2);

			ObsPropertyType type = property.Type;
			bool addLabel = true;
			List<Control> controls = new List<Control>();

			switch (type)
			{
				case ObsPropertyType.Bool:
					{
						addLabel = false;
						AddBool(property, setting, controls);
						break;
					}
				case ObsPropertyType.Int:
				case ObsPropertyType.Float:
					{
						AddNumeric(property, setting, controls);
						break;
					}
				case ObsPropertyType.Text:
					{
						AddText(property, setting, controls);
						break;
					}
				case ObsPropertyType.Path:
					{
						AddPath(property, setting, controls);
						break;
					}
				case ObsPropertyType.List:
					{
						AddList(property, setting, controls);
						break;
					}
				case ObsPropertyType.Color:
					{
						AddColor(property, setting, controls);
						break;
					}
				case ObsPropertyType.Button:
					{
						addLabel = false;
						AddButton(property, setting, controls);
						break;
					}
				case ObsPropertyType.Font:
					{
						AddFont(property, setting, controls);
						break;
					}
				case ObsPropertyType.EditableList:
					{
						addLabel = false;
						AddEditableList(property, setting, controls);
						break;
					}
				default:
					{
						throw new Exception(String.Format("Error, unimplemented property type {0} for property {1}", type, property.Description));
					}
			}

			Label nameLabel = new Label
			{
				Text = addLabel ? property.Description : "",
				TextAlign = ContentAlignment.MiddleRight,
				MinimumSize = new Size(170, 0),
				AutoSize = true,
				Dock = DockStyle.Left
			};
			controls.Insert(0, nameLabel);

			foreach (Control control in controls)
			{
				WinFormsHelper.DoubleBufferControl(control);

				int margin = 0;
				Padding oldmargin = control.Margin;
				oldmargin.Top = margin;
				oldmargin.Bottom = margin;
				control.Margin = oldmargin;
			}

			SuspendLayout();
			Controls.AddRange(controls.ToArray());
			ResumeLayout();
		}

		private void AddBool(ObsProperty property, ObsData setting, List<Control> controls)
		{
			string name = property.Name;

			CheckBox checkbox = new CheckBox
			{
				Width = 300,
				Height = 18,
				Checked = setting.GetBool(name),
				Text = property.Description,
				TextAlign = ContentAlignment.MiddleLeft
			};

			checkbox.CheckedChanged += (sender, args) =>
			{
				setting.SetBool(name, checkbox.Checked);
				view.PropertyChanged(property);
			};

			controls.Add(checkbox);
		}

		private void AddNumeric(ObsProperty property, ObsData setting, List<Control> controls)
		{
			ObsPropertyType type = property.Type;
			string name = property.Name;

			NumericUpDown numeric = new NumericUpDown
			{
				Width = 300,
				DecimalPlaces = 0
			};

			if (type == ObsPropertyType.Int)
			{
				int intMin = property.IntMin;
				int intMax = property.IntMax;
				long intValue = setting.GetInt(name);
				intValue = Math.Max(Math.Min(intValue, intMax), intMin);

				numeric.Minimum = intMin;
				numeric.Maximum = intMax;
				numeric.Increment = property.IntStep;
				numeric.Value = intValue;

				numeric.ValueChanged += (sender, args) =>
				{
					setting.SetInt(name, (int)numeric.Value);
					view.PropertyChanged(property);
				};
			}
			else if (type == ObsPropertyType.Float)
			{
				double floatMin = property.FloatMin;
				double floatMax = property.FloatMax;
				double floatValue = setting.GetDouble(name);
				floatValue = Math.Max(Math.Min(floatValue, floatMax), floatMin);

				numeric.DecimalPlaces = 2;
				numeric.Minimum = (decimal)floatMin;
				numeric.Maximum = (decimal)floatMax;
				numeric.Increment = (decimal)property.FloatStep;
				numeric.Value = (decimal)floatValue;

				numeric.ValueChanged += (sender, args) =>
				{
					setting.SetDouble(name, (double)numeric.Value);
					view.PropertyChanged(property);
				};
			}

			if (property.IntType == ObsNumberType.Slider)
			{
				numeric.Width = 75;
				numeric.Height = 23;

				const int multiplier = 1000;
				var trackbar = new TrackBar
							   {
								   AutoSize = false,
								   Width = 300,
								   Height = 23,
								   TickStyle = TickStyle.None,
								   Minimum = (int)(numeric.Minimum * multiplier),
								   Maximum = (int)(numeric.Maximum * multiplier),
								   SmallChange = (int)(numeric.Increment * multiplier),
								   LargeChange = (int)(numeric.Increment * multiplier),
								   Value = (int)(numeric.Value * multiplier)
							   };
				trackbar.ValueChanged += (sender, args) => numeric.Value = (decimal)trackbar.Value / multiplier;
				controls.Add(trackbar);
			}
			controls.Add(numeric);
		}

		private void AddText(ObsProperty property, ObsData setting, List<Control> controls)
		{
			string name = property.Name;

			TextBox textbox = new TextBox
			{
				Width = 300,
				Text = setting.GetString(name)
			};

			if (property.TextType == ObsTextType.Password)
				textbox.PasswordChar = '*';
			else if (property.TextType == ObsTextType.Multiline)
			{
				textbox.Multiline = true;
				textbox.Height *= 3;
			}

			textbox.TextChanged += (sender, args) =>
			{
				setting.SetString(name, textbox.Text);
				view.PropertyChanged(property);
			};

			controls.Add(textbox);
		}

		private void AddPath(ObsProperty property, ObsData setting, List<Control> controls)
		{
			string name = property.Name;

			TextBox textbox = new TextBox
			{
				Width = 300,
				Text = setting.GetString(name)
			};
			Button button = new Button { Text = "Browse..." };

			if (property.PathType == ObsPathType.File)
			{
				OpenFileDialog dialog = new OpenFileDialog
				{
					AutoUpgradeEnabled = true,
					Filter = property.PathFilter.ToString(),
					InitialDirectory = property.PathDefault,
					FilterIndex = 1
				};

				button.Click += (sender, args) =>
				{
					if (dialog.ShowDialog(this) == DialogResult.OK)
						textbox.Text = dialog.FileName;
				};
			}
			else if (property.PathType == ObsPathType.Directory)
			{
				FolderBrowserDialog dialog = new FolderBrowserDialog
				{
					SelectedPath = property.PathDefault
				};

				button.Click += (sender, args) =>
				{
					if (dialog.ShowDialog(this) == DialogResult.OK)
						textbox.Text = dialog.SelectedPath;
				};
			}

			textbox.TextChanged += (sender, args) =>
			{
				setting.SetString(name, textbox.Text);
				view.PropertyChanged(property);
			};

			controls.Add(textbox);
			controls.Add(button);
		}

		private void AddList(ObsProperty property, ObsData setting, List<Control> controls)
		{
			string name = property.Name;

			int index = 0;
			string[] names = property.GetListItemNames();
			object[] values = property.GetListItemValues();
			EventHandler selectedIndexChanged = null;
			ComboBox combobox = new ComboBox { Width = 300 };

			combobox.Items.AddRange(names.ToArray());

			//if (namelist.Length > 0)
			//	combobox.SelectedIndex = 0;

			if (property.ListType == ObsComboType.List)
				combobox.DropDownStyle = ComboBoxStyle.DropDownList;

			switch (property.ListFormat)
			{
				case ObsComboFormat.Float:
					{
						index = Array.IndexOf(values, setting.GetDouble(name));

						selectedIndexChanged = (sender, args) =>
						{
							double value = (double)values.GetValue(combobox.SelectedIndex);
							setting.SetDouble(name, value);
							view.PropertyChanged(property);
						};
						break;
					}
				case ObsComboFormat.Int:
					{
						var val = setting.GetInt(name);
						index = Array.IndexOf(values, setting.GetInt(name));

						selectedIndexChanged = (sender, args) =>
						{
							long value = (long)values[combobox.SelectedIndex];
							setting.SetInt(name, (int)value);
							view.PropertyChanged(property);
						};
						break;
					}
				case ObsComboFormat.String:
					{
						index = Array.IndexOf(values, setting.GetString(name));

						selectedIndexChanged = (sender, args) =>
						{
							string value = (string)values[combobox.SelectedIndex];
							setting.SetString(name, value);
							view.PropertyChanged(property);
						};
						break;
					}
			}

			if (index != -1)
				combobox.SelectedIndex = index;

			combobox.SelectedIndexChanged += selectedIndexChanged;

			if (index == -1 && names.Length > 0)
				combobox.SelectedIndex = 0;

			controls.Add(combobox);
		}

		private void AddColor(ObsProperty property, ObsData setting, List<Control> controls)
		{
			string name = property.Name;

			// note: libobs stores color in ABGR instead of ARGB

			Color color = ColorHelper.FromAbgr((int)setting.GetInt(name));
			TextBox textbox = new TextBox
			{
				Width = 300,
				ForeColor = color.GetBrightness() > 0.93 ? Color.Black : color,
				Text = color.ToHtml(),
				TextAlign = HorizontalAlignment.Center
			};

			Button button = new Button { Text = "Select..." };

			textbox.TextChanged += (sender, args) =>
			{
				Color newColor = ColorHelper.FromAbgr((int)setting.GetInt(name));
				newColor = newColor.FromHtml(textbox.Text);

				textbox.ForeColor = newColor.GetBrightness() > 0.93 ? Color.Black : newColor;
				setting.SetInt(name, newColor.ToAbgr());
				view.PropertyChanged(property);
			};

			button.Click += (sender, args) =>
			{
				ColorDialog colorDialog = new ColorDialog
				{
					AllowFullOpen = true,
					AnyColor = true,
					Color = ColorHelper.TryColorFromHtml(textbox.Text),
					FullOpen = true
				};
				colorDialog.Color = colorDialog.Color.FromHtml(textbox.Text);

				if (colorDialog.ShowDialog(this) == DialogResult.OK)
					textbox.Text = colorDialog.Color.ToHtml();
			};

			controls.Add(textbox);
			controls.Add(button);
		}

		private void AddButton(ObsProperty property, ObsData setting, List<Control> controls)
		{
			Button button = new Button { Text = property.Description };
			button.Click += (sender, args) => view.PropertyButtonClicked(property);

			controls.Add(button);
		}

		private void AddFont(ObsProperty property, ObsData setting, List<Control> controls)
		{
			string name = property.Name;

			Label label = new Label
			{
				Width = 300,
				Height = 60,
				AutoSize = false,
				BorderStyle = BorderStyle.Fixed3D,
				TextAlign = ContentAlignment.MiddleCenter
			};

			Button button = new Button { Text = "Select..." };

			using (ObsData fontData = new ObsData(setting.GetObject(name)))
			{
				string family = fontData.GetString("face");
				//string style = fontData.GetString("style");	//not supported in Windows
				ObsFontFlags flags = (ObsFontFlags)fontData.GetInt("flags");

				label.Font = new Font(family, 25F, (FontStyle)flags); ;
				label.Text = family;
			}

			button.Click += (sender, args) =>
			{
				var fontDialog = new FontDialog();

				using (ObsData fontData = new ObsData(setting.GetObject(name)))
				{
					float size = fontData.GetInt("size");
					fontDialog.Font = new Font(label.Font.FontFamily, size, label.Font.Style);
				}

				if (fontDialog.ShowDialog() == DialogResult.OK)
				{
					var font = fontDialog.Font;

					using (ObsData fontData = new ObsData(setting.GetObject(name)))
					{
						fontData.SetString("face", font.Name.ToString());
						fontData.SetString("style", "");	//not supported in Windows
						fontData.SetInt("size", (int)font.SizeInPoints);
						fontData.SetInt("flags", (int)font.Style);
					}

					view.PropertyChanged(property);

					font = new Font(font.Name, 25f, font.Style);
					label.Font = font;
					label.Text = font.Name;
				}
			};

			controls.Add(label);
			controls.Add(button);
		}

		private void AddEditableList(ObsProperty property, ObsData setting, List<Control> controls)
		{
			string name = property.Name;
			bool allowFiles = property.EditableListAllowFiles;

			FlowLayoutPanel layoutPanel = new FlowLayoutPanel()
			{
				FlowDirection = FlowDirection.TopDown,
				AutoSize = true,
				AutoSizeMode = AutoSizeMode.GrowAndShrink
			};

			ListBox listBox = new ListBox()
			{
				Width = 300,
				Height = 180,
				IntegralHeight = false,
				SelectionMode = SelectionMode.MultiExtended,
			};

			//TODO: use icons for list buttons
			Button buttonAdd = new Button { Text = "+", Width = 25, Tag = listBox };
			Button buttonAddMulti = new Button { Text = "B", Width = 25, Tag = listBox };
			Button buttonRemove = new Button { Text = "-", Width = 25, Tag = listBox };
			Button buttonConfig = new Button { Text = "C", Width = 25, Tag = listBox };
			Button buttonUp = new Button { Text = "^", Width = 25, Tag = listBox };
			Button buttonDown = new Button { Text = "v", Width = 25, Tag = listBox };

			using (ObsDataArray array = setting.GetArray(name))
			{
				listBox.BeginUpdate();

				if (array != null)
					foreach (ObsData item in array)
						listBox.Items.Add(item.GetString("value"));

				listBox.EndUpdate();
			}

			buttonAdd.Click += (sender, args) =>
			{
				//TODO: open dialog with ok/cancel options, text field and browse button (with allowFiles)
			};

			buttonAddMulti.Click += (sender, args) =>
			{
				OpenFileDialog dialog = new OpenFileDialog
				{
					AutoUpgradeEnabled = true,
					Filter = property.EditableListFilter.ToString(),
					FilterIndex = 1,
					InitialDirectory = property.EditableListPathDefault,
					Multiselect = true
				};

				if (dialog.ShowDialog(this) == DialogResult.OK)
				{
					listBox.BeginUpdate();
					listBox.Items.AddRange(dialog.FileNames);
					listBox.EndUpdate();

					EditableListChanged(listBox, property, setting);
				}
			};

			buttonRemove.Click += (sender, args) =>
			{
				if (listBox.SelectedItems.Count == 0)
					return;

				listBox.BeginUpdate();

				for (int i = listBox.SelectedItems.Count - 1; i >= 0; i--)
					listBox.Items.RemoveAt(listBox.SelectedIndices[i]);

				listBox.EndUpdate();

				EditableListChanged(listBox, property, setting);
			};

			buttonConfig.Click += (sender, args) =>
			{
				// To avoid confusion, only allow to edit one item at the time
				if (listBox.SelectedItems.Count != 1)
					return;


				//TODO: open dialog with ok/cancel options, text field and browse button (with allowFiles), same as in add

				/*OpenFileDialog dialog = new OpenFileDialog
				{
					FileName = listBox.SelectedItem.ToString(),
					AutoUpgradeEnabled = true,
					Filter = property.EditableListFilter.ToString(),
					FilterIndex = 1,
					InitialDirectory = property.EditableListPathDefault,
					Multiselect = true
				};

				if (dialog.ShowDialog(this) == DialogResult.OK)
				{
					listBox.Items[listBox.SelectedIndex] = dialog.FileName;
					EditableListChanged(listBox, property, setting);
				}*/
			};

			buttonUp.Click += (sender, args) =>
			{
				if (listBox.SelectedItems.Count == 0)
					return;

				listBox.BeginUpdate();

				int lastIndex = -1;
				for (int i = 0; i < listBox.SelectedItems.Count; i++)
				{
					int index = listBox.SelectedIndices[i];
					int newIndex = Math.Max(index - 1, 0);
					object item = listBox.Items[index];

					if (index != newIndex && newIndex != lastIndex)
					{
						listBox.Items.RemoveAt(index);
						listBox.Items.Insert(newIndex, item);
						listBox.SetSelected(newIndex, true);
					}
					lastIndex = newIndex;
				}

				listBox.EndUpdate();

				EditableListChanged(listBox, property, setting);
			};

			buttonDown.Click += (sender, args) =>
			{
				if (listBox.SelectedItems.Count == 0)
					return;

				listBox.BeginUpdate();

				int lastIndex = -1;
				for (int i = listBox.SelectedItems.Count - 1; i >= 0; i--)
				{
					int index = listBox.SelectedIndices[i];
					int newIndex = Math.Min(index + 1, listBox.Items.Count - 1);
					object item = listBox.Items[index];

					if (index != newIndex && newIndex != lastIndex)
					{
						listBox.Items.RemoveAt(index);
						listBox.Items.Insert(newIndex, item);
						listBox.SetSelected(newIndex, true);
					}
					lastIndex = newIndex;
				}

				listBox.EndUpdate();

				EditableListChanged(listBox, property, setting);
			};

			layoutPanel.Controls.Add(buttonAdd);
			if (allowFiles)
				layoutPanel.Controls.Add(buttonAddMulti);
			layoutPanel.Controls.Add(buttonRemove);
			layoutPanel.Controls.Add(buttonConfig);
			layoutPanel.Controls.Add(buttonUp);
			layoutPanel.Controls.Add(buttonDown);

			controls.Add(listBox);
			controls.Add(layoutPanel);
		}

		private void EditableListChanged(ListBox listBox, ObsProperty property, ObsData setting)
		{
			string propertyName = property.Name;
			ObsDataArray array = new ObsDataArray();

			foreach (string item in listBox.Items)
			{
				ObsData itemArray = new ObsData();
				itemArray.SetString("value", item);

				array.Add(itemArray);
				itemArray.Dispose();
			}

			setting.SetArray(propertyName, array);
			array.Dispose();
		}
	}
}