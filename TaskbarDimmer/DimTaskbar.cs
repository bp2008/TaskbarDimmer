using BPUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TaskbarDimmer
{
	public class DimTaskbar : IDisposable
	{
		private int focusedProcessId;
		private List<CoverForm> coverForms = new List<CoverForm>();
		private FocusMonitor fm = null;
		private int myPid = Process.GetCurrentProcess().Id;
		private Thread thrBackground;
		private volatile bool abort = false;
		private Cooldown MediumCooldown = new Cooldown(500);
		private Cooldown LongCooldown = new Cooldown(5000);
		public DimTaskbar()
		{
			//fm = new FocusMonitor();
			//fm.FocusChanged += Fm_FocusChanged;

			// Keep the form on top and update its visibility
			thrBackground = new Thread(() =>
			{
				try
				{
					while (!abort)
					{
						try
						{
							if (LongCooldown.Consume())
							{
								// about every 5000ms

								// Get bounds of all screens
								Rectangle[] screens = Screen.AllScreens.Select(s => s.Bounds).ToArray();

								// Add or remove CoverForms as needed...
								while (screens.Length > coverForms.Count)
									coverForms.Add(new CoverForm(coverForms.Count));
								while (screens.Length < coverForms.Count)
								{
									CoverForm form = coverForms[coverForms.Count - 1];
									coverForms.RemoveAt(coverForms.Count - 1);
									form.Dispose();
								}

								// Update screen bounds knowledge of each CoverForm
								for (int i = 0; i < screens.Length; i++)
								{
									Rectangle screen = screens[i];
									CoverForm coverForm = coverForms[i];
									coverForm.ScreenSync(screen);
								}
							}
							if (MediumCooldown.Consume())
							{
								// about every 500ms

								//if (focusedProcessId == myPid)
								//{
								//	Console.WriteLine("TaskbarDimmer is focused!");
								//	return; // I have a theory that running UpdateVisibility while this process has its context menu open can cause the context menu to close.
								//}

								CheckFocusedWindow();
							}
							// Every iteration
							Point cursorPosition = Cursor.Position;
							for (int i = 0; i < coverForms.Count; i++)
							{
								CoverForm coverForm = coverForms[i];
								coverForm.NotifyMousePositionChanged(cursorPosition);
								coverForm.UpdateVisibility();
							}
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.ToString());
						}
						Thread.Sleep(100);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("CRITICAL ERROR in TaskbarDimmerTimer thread");
					Console.WriteLine(ex.ToString());
				}
			});
			thrBackground.IsBackground = true;
			thrBackground.Name = "TaskbarDimmerTimer";
			thrBackground.Start();
		}

		private void Fm_FocusChanged(object sender, int processId)
		{
			focusedProcessId = processId;
			//Console.WriteLine("Focus changed to pid " + processId);
			CheckFocusedWindow();
		}

		private void CheckFocusedWindow()
		{
			try
			{
				Rectangle focusedWindowRect = GetBoundsOfFocusedWindow();

				//try
				//{
				//	using (Process process = GetProcess(focusedProcessId))
				//	{
				//		// Get bounds of the focused window
				//		focusedWindowRect = GetBounds(process.MainWindowHandle);
				//		Console.WriteLine("focusedProcess " + process.ProcessName + " " + focusedWindowRect);
				//	}
				//}
				//catch (Exception ex)
				//{
				//	Console.WriteLine(ex.ToString());
				//	focusedWindowRect = new Rectangle();
				//}

				// Set visibility of each CoverForm according to mouse position and focused window bounds.
				for (int i = 0; i < coverForms.Count; i++)
				{
					CoverForm coverForm = coverForms[i];
					coverForm.NotifyFocusedWindowBounds(focusedWindowRect);
					//Console.WriteLine("screen " + i + " bounds " + screen + " " + (coverForm.HasFullscreenApp ? "HasFullscreenApp" : ""));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
		#region Helpers
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
		public struct Rect
		{
			public int Left { get; set; }
			public int Top { get; set; }
			public int Right { get; set; }
			public int Bottom { get; set; }
		}
		private Rectangle GetBounds(IntPtr windowHandle)
		{
			Rect rect = new Rect();
			GetWindowRect(new HandleRef(null, windowHandle), ref rect);
			return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
		}
		private Rectangle GetBoundsOfFocusedWindow()
		{
			return GetBounds(GetForegroundWindow());
		}

		[DllImport("user32.dll")]
		static extern IntPtr FindWindow(string className, string windowText);
		[DllImport("user32.dll")]
		static extern bool GetWindowRect(HandleRef hwnd, ref Rect rectangle);
		[DllImport("user32.dll")]
		static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();
		#endregion

		#region Dispose
		private bool disposedValue;
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// dispose managed state (managed objects)
				}
				abort = true;
				fm?.Dispose();
				// free unmanaged resources (unmanaged objects) and override finalizer
				// set large fields to null
				disposedValue = true;
			}
		}

		// // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~DimTaskbar()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}