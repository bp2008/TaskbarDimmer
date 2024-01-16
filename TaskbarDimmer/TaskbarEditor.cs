using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BPUtil;

namespace TaskbarDimmer
{
	public partial class TaskbarEditor : UserControl
	{
		public int TaskbarIndex { get; private set; } = -1;
		public TaskbarSettings settings
		{
			get
			{
				return Program.Settings.Taskbars[TaskbarIndex];
			}
		}

		public TaskbarEditor()
		{
			InitializeComponent();
		}

		public void SetIndex(int index)
		{
			bool added = false;
			while (index < 64 && Program.Settings.Taskbars.Count <= index)
			{
				Program.Settings.Taskbars.Add(new TaskbarSettings());
				added = true;
			}
			if (added)
				Program.Settings.Save();

			TaskbarIndex = index;
			if (TaskbarIndex >= 0)
			{
				cbPosition.Items.AddRange(Enum.GetNames(typeof(TaskbarPosition)));

				tbLightness.Value = settings.Lightness.Clamp(1, 100);
				lblLightness.Text = settings.Lightness.ToString();

				cbPosition.SelectedIndex = (int)settings.Position;
				nudSize.Value = (int)settings.Size;
			}
		}

		private void cbPosition_SelectedIndexChanged(object sender, EventArgs e)
		{
			settings.Position = (TaskbarPosition)cbPosition.SelectedIndex;
			Program.Settings.Save();
		}

		private void nudSize_ValueChanged(object sender, EventArgs e)
		{
			settings.Size = (int)nudSize.Value;
			Program.Settings.Save();
		}

		private void tbLightness_Scroll(object sender, EventArgs e)
		{
			settings.Lightness = tbLightness.Value.Clamp(1, 100);
			lblLightness.Text = settings.Lightness.ToString();
			Program.Settings.Save();
		}

		public void SetBoundsLabel(Rectangle? boundsOrNull)
		{
			if (boundsOrNull == null)
				lblBounds.Text = "";
			else
			{
				Rectangle b = boundsOrNull.Value;
				lblBounds.Text = b.X + "," + b.Y + " " + b.Width + "x" + b.Height;
			}
		}
	}
}
