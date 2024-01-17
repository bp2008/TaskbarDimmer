using BPUtil;
using BPUtil.NativeWin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskbarDimmer.Properties;

namespace TaskbarDimmer
{
	public partial class CoverForm : Form
	{
		public readonly int TaskbarIndex;

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

		#region Fields from which the visibility state is derived
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
		/// <summary>
		/// Taskbar position as of the last state sync.
		/// </summary>
		private TaskbarPosition MyPosition = TaskbarPosition.None;
		/// <summary>
		/// Size as of the last state sync.
		/// </summary>
		private int MySize = 0;
		/// <summary>
		/// Gets the hit test size (size for mouseover purposes), which is larger than the actual size.
		/// </summary>
		private int HitTestSize => MySize + 30;
		/// <summary>
		/// If true, the screen bounds have just changed and the form must change its bounds in response.
		/// </summary>
		private bool screenBoundsChanged = false;
		#endregion

		public CoverForm(int taskbarIndex)
		{
			TaskbarIndex = taskbarIndex;
			this.Text = "TaskbarDimmer_" + taskbarIndex;
			InitializeComponent();
			this.Visible = false;
		}

		private Cooldown MediumCooldown = new Cooldown(500);
		private Cooldown LongCooldown = new Cooldown(5000);
		private void timer_Tick(object sender, EventArgs e)
		{
			NotifyMousePositionChanged(Cursor.Position);
			UpdateVisibility();
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
		const uint WM_WINDOWPOSCHANGING = 0x46;
		const int HTTRANSPARENT = -1;
		const int HTCLIENT = 1;
		const int HTCAPTION = 2;
		protected override void WndProc(ref Message m)
		{
			//Info("[" + System.Threading.Thread.CurrentThread.ManagedThreadId + "] WndProc " + (WM)m.Msg + " (" + m.Msg + ")");
			if (m.Msg == WM_NCHITTEST)
			{
				// We want to pass mouse events on to the desktop, as if we were not even here.
				m.Result = new IntPtr(HTTRANSPARENT);
				return;
			}
			if (m.Msg == (int)WM.WM_WINDOWPOSCHANGED)
			{
				//this.Invalidate();
				//this.Visible = false;
			}
			base.WndProc(ref m);
		}

		#endregion

		private void Info(string message)
		{
			if (TaskbarIndex == 0)
				Logger.Info(message);
		}

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
		/// Checks if the topmost window at the center of the screen is using the whole screen.
		/// </summary>
		public void CheckForFullscreenApp()
		{
			Point p = new Point(MyScreenBounds.X + MyScreenBounds.Width / 2, MyScreenBounds.Y + MyScreenBounds.Height / 2);
			IntPtr windowHandle = NativeMethods.WindowFromPoint(new NativeMethods.POINT() { X = p.X, Y = p.Y });
			if (windowHandle != IntPtr.Zero)
			{
				Rectangle windowBounds = NativeMethods.GetWindowBounds(windowHandle);
				NativeMethods.GetWindowThreadProcessId(windowHandle, out int pid);
				Process proc = GetProcess(pid);
				HasFullscreenApp = !"explorer".IEquals(proc?.ProcessName) && windowBounds.Contains(MyScreenBounds);
			}
			else
				HasFullscreenApp = false;
		}
		private Process GetProcess(int processId)
		{
			try
			{
				return Process.GetProcessById(processId);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return null;
			}
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
			if (!MyScreenBounds.Equals(screenBounds))
			{
				MyScreenBounds = screenBounds;
				screenBoundsChanged = true;
			}
		}

		private void ApplySettings()
		{
			double preferredOpacity = (100 - settings.Lightness.Clamp(1, 100)) / 100d;
			if (Opacity != preferredOpacity)
				Opacity = preferredOpacity;

			if (MySize != settings.Size)
			{
				MySize = settings.Size;
				MyPosition = TaskbarPosition.None; // Force bounds to be recalculated
			}

			if (MyPosition == TaskbarPosition.Bottom || MyPosition == TaskbarPosition.Top)
			{
				if (Height != MySize)
				{
					//Info("Detected unwanted height change to " + Height + ". Resetting to " + MySize);
					screenBoundsChanged = true;
				}
			}
			if (screenBoundsChanged || MyPosition != settings.Position)
			{
				screenBoundsChanged = false;
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
			if (HasFullscreenApp
				|| IsHovered
				|| (settings.Position != TaskbarPosition.Bottom
					&& settings.Position != TaskbarPosition.Top
					&& settings.Position != TaskbarPosition.Left
					&& settings.Position != TaskbarPosition.Right))
			{
				if (this.Visible)
					this.Visible = false;
			}
			else
			{
				ApplySettings();
				if (!this.Visible)
				{
					this.Visible = true;
					this.TopMost = true;
				}
			}
		}
	}
}
