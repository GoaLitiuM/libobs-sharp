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

using OBS;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using test.Utility;

namespace test.Controls
{
	public partial class PropertiesView : UserControl
	{
		private Func<ObsProperties> reloadDelegate;
		private Action<ObsData> updateDelegate;

		private ObsProperties properties;
		private IObsContextData context;
		private ObsData settings;
		private bool deferUpdate;
		private int refreshCount = -1;

		/// <summary> 
		/// Initializes a view for ObsProperties.</summary> 
		/// <param name="settings"> The source of properties.</param>
		/// <param name="context"> Owner of the settings, receives callbacks when a change occurs.</param>
		/// <param name="reloadDelegate"> Callback used for refreshing properties.</param>
		/// <param name="updateDelegate"> Optional: Callback used for notifying the object when update is needed.</param>
		public PropertiesView(ObsData settings, IObsContextData context,
			Func<ObsProperties> reloadDelegate, Action<ObsData> updateDelegate = null)
		{
			InitializeComponent();

			this.settings = settings;
			this.context = context;
			this.reloadDelegate = reloadDelegate;
			this.updateDelegate = updateDelegate;

			//force double buffering on to eliminate control flickering during refresh
			WinFormsHelper.DoubleBufferControl(panel);

			ReloadProperties();
		}

		public void ReloadProperties()
		{
			properties = reloadDelegate();

			if (properties == null)
				return;

			deferUpdate = properties.Flags.HasFlag(ObsPropertiesFlags.DeferUpdate);

			RefreshProperties();
		}

		/// <param name="focusProperty"> Optional: Sets focus to control of this property.</param>
		private void RefreshProperties(ObsProperty focusProperty = null)
		{
			//prevent properties triggering another refresh when previous refresh is still ongoing
			if (++refreshCount > 0)
				return;

			ScrollableControl parent = (Parent as ScrollableControl);
			var oldScrollPos = parent != null ? parent.VerticalScroll.Value : 0;

			List<Control> controls = new List<Control>();
			Control focusControl = null;

			do
			{
				refreshCount = 0;
				controls.Clear();
				ObsProperty[] propertyList = properties.GetPropertyList();

				foreach (ObsProperty property in propertyList)
				{
					PropertyControl control = new PropertyControl(this, property, settings)
					{
						Enabled = property.Enabled,
						Visible = property.Visible
					};

					if (focusProperty != null && property.GetPointer() == focusProperty.GetPointer())
						focusControl = control.Controls[1];

					controls.Add(control);
				}
			}
			while (refreshCount > 0);

			panel.SuspendLayout();

			panel.Controls.Clear();
			panel.Controls.AddRange(controls.ToArray());

			panel.Select();
			panel.Focus();

			//restore last focused control and scroll state
			if (focusControl != null)
			{
				focusControl.Focus();
				focusControl.Select();
			}

			if (parent != null)
				parent.VerticalScroll.Value = oldScrollPos;

			panel.ResumeLayout();
			PerformLayout();

			refreshCount = -1;
		}

		public void UpdateSettings()
		{
			if (updateDelegate != null)
				updateDelegate(settings);
		}

		public void PropertyChanged(ObsProperty property)
		{
			if (!deferUpdate)
				UpdateSettings();

			if (property.Modified(settings))
				RefreshProperties(property);
		}

		public void PropertyButtonClicked(ObsProperty property)
		{
			if (property.ButtonClicked(context.GetPointer()))
				RefreshProperties(property);
		}

		public void PropertyButtonClicked(ObsProperty property, libobs.obs_property_clicked_t clicked)
		{
			if (clicked == null)
				return;

			if (property.ButtonClicked(clicked, properties, context.GetPointer()))
				RefreshProperties(property);
		}
	}
}