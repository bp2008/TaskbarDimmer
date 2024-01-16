using BPUtil;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TaskbarDimmer.DimTaskbar;

namespace TaskbarDimmer
{
	public class CoverForm : Form
	{
		/// <summary>
		/// Gets the Rectangle the represents the bounds of this CoverForm's screen.
		/// </summary>
		public Rectangle MyScreenBounds { get; protected set; }
		/// <summary>
		/// Gets the Rectangle the represents the bounds of this CoverForm's mouseover region.
		/// </summary>
		public Rectangle HitTestBounds { get; protected set; }
		/// <summary>
		/// Gets or sets if the mouse is currently hovering over this CoverForm.
		/// </summary>
		public bool IsHovered { get; set; }
		/// <summary>
		/// Gets or sets a value indicating if this CoverForm is on a screen with a focused full-screen app.
		/// </summary>
		public bool HasFullscreenApp { get; set; }

		public readonly int TaskbarIndex;

		private TaskbarPosition MyPosition = TaskbarPosition.None;
		private int MySize = 0;
		private int HitTestSize => MySize + 30;
		private bool isLoaded = false;

		private TaskbarSettings _fallbackSettings = new TaskbarSettings() { Position = TaskbarPosition.None };
		private TaskbarSettings settings
		{
			get
			{
				if (Program.Settings.Taskbars.Count > TaskbarIndex)
					return Program.Settings.Taskbars[TaskbarIndex];
				else
					return _fallbackSettings;
			}
		}
		public CoverForm(int taskbarIndex)
		{
			TaskbarIndex = taskbarIndex;
			StartPosition = FormStartPosition.Manual;
			FormBorderStyle = FormBorderStyle.None;
			TopMost = true;
			ShowInTaskbar = false;
			BackColor = System.Drawing.Color.Black;
			Opacity = 0;

			this.Load += CoverForm_Load;
		}

		private void CoverForm_Load(object sender, EventArgs e)
		{
			isLoaded = true;
			Opacity = 0.1;
			MySize = 0;
			MyPosition = TaskbarPosition.None;
			ApplySettings();
			this.Hide();
		}
		#region Make me transparent to clicks

		const int WS_EX_LAYERED = 0x80000;
		const int WS_EX_TRANSPARENT = 0x20;
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= WS_EX_LAYERED;
				cp.ExStyle |= WS_EX_TRANSPARENT;
				return cp;
			}
		}


		const uint WM_NCHITTEST = 0x84;
		const int HTTRANSPARENT = -1;
		const int HTCLIENT = 1;
		const int HTCAPTION = 2;
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == WM_NCHITTEST)
			{
				// We want to pass mouse events on to the desktop, as if we were not even here.
				m.Result = new IntPtr(HTTRANSPARENT);
			}
			else
				base.WndProc(ref m);
		}

		#endregion

		/// <summary>
		/// Checks if the mouse is hovering over this CoverForm.
		/// </summary>
		/// <param name="mousePosition"></param>
		public void NotifyMousePositionChanged(Point mousePosition)
		{
			if (HitTestBounds.Contains(mousePosition))
			{
				if (!IsHovered)
				{
					IsHovered = true;
					UpdateVisibility();
				}
			}
			else
			{
				if (IsHovered)
				{
					IsHovered = false;
					UpdateVisibility();
				}
			}
		}
		/// <summary>
		/// Checks if the focused window rectangle contains the entirety of the screen this CoverForm is on.
		/// </summary>
		/// <param name="focusedWindowRect"></param>
		public void NotifyFocusedWindowBounds(Rectangle focusedWindowRect)
		{
			HasFullscreenApp = focusedWindowRect.Contains(MyScreenBounds);
		}

		/// <summary>
		/// Moves this CoverForm to cover the given rectangle.
		/// </summary>
		/// <param name="rect"></param>
		public void CoverRect(Rectangle rect)
		{
			Bounds = rect;
		}

		/// <summary>
		/// Binds this CoverForm to the given screen bounds (efficiently).
		/// </summary>
		/// <param name="screenBounds">Screen bounds.</param>
		public void ScreenSync(Rectangle screenBounds)
		{
			MyScreenBounds = screenBounds;
		}

		private void ApplySettings()
		{
			double preferredOpacity = (100 - settings.Lightness.Clamp(1, 100)) / 100d;
			if (Opacity != preferredOpacity)
			{
				Opacity = 0.2;
				Opacity = preferredOpacity;
			}

			if (MySize != settings.Size)
			{
				MySize = settings.Size;
				MyPosition = TaskbarPosition.None; // Force bounds to be recalculated
			}

			if (MyPosition != settings.Position)
			{
				MyPosition = settings.Position;

				switch (MyPosition)
				{
					case TaskbarPosition.Bottom:
						Left = MyScreenBounds.X;
						Top = MyScreenBounds.Y + (MyScreenBounds.Height - MySize);
						Width = MyScreenBounds.Width;
						Height = MySize;
						HitTestBounds = new Rectangle(MyScreenBounds.X,
							MyScreenBounds.Y + (MyScreenBounds.Height - HitTestSize),
							MyScreenBounds.Width,
							HitTestSize);
						break;
					case TaskbarPosition.Top:
						Left = MyScreenBounds.X;
						Top = MyScreenBounds.Y;
						Width = MyScreenBounds.Width;
						Height = MySize;
						HitTestBounds = new Rectangle(MyScreenBounds.X,
							MyScreenBounds.Y,
							MyScreenBounds.Width,
							HitTestSize);
						break;
					case TaskbarPosition.Left:
						Left = MyScreenBounds.X;
						Top = MyScreenBounds.Y;
						Width = MySize;
						Height = MyScreenBounds.Height;
						HitTestBounds = new Rectangle(MyScreenBounds.X,
							MyScreenBounds.Y,
							HitTestSize,
							MyScreenBounds.Height);
						break;
					case TaskbarPosition.Right:
						Left = MyScreenBounds.X + (MyScreenBounds.Width - MySize);
						Top = MyScreenBounds.Y;
						Width = MySize;
						Height = MyScreenBounds.Height;
						HitTestBounds = new Rectangle(MyScreenBounds.X + (MyScreenBounds.Width - HitTestSize),
							MyScreenBounds.Y,
							HitTestSize,
							MyScreenBounds.Height);
						break;
				}
			}
		}

		public void UpdateVisibility()
		{
			if (this.InvokeRequired)
			{
				this.Invoke((Action)UpdateVisibility);
				return;
			}
			//if (!isLoaded)
			//	return;
			if (HasFullscreenApp
				|| IsHovered
				|| (settings.Position != TaskbarPosition.Bottom
					&& settings.Position != TaskbarPosition.Top
					&& settings.Position != TaskbarPosition.Left
					&& settings.Position != TaskbarPosition.Right))
			{
				this.Hide();
				Opacity = 0;
			}
			else
			{
				ApplySettings();
				this.Show();
				this.TopMost = true;
			}
		}
	}
}
