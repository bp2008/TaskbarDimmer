using BPUtil;
using BPUtil.NativeWin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static BPUtil.NativeWin.NativeMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TaskbarDimmer
{
	public class DimTaskbar : IDisposable
	{
		private int focusedProcessId;
		public List<CoverForm> coverForms = new List<CoverForm>();
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
								// 2024-01-16: Screen.Bounds does not update properly when the bounds change, but WorkingArea does.
								Rectangle[] screens = Screen.AllScreens
									.Select(s => s.WorkingArea)
									.Select(wa => Screen.GetBounds(wa))
									.ToArray();

								// Add or remove CoverForms as needed...
								if (screens.Length != coverForms.Count)
								{
									Program.app.RunOnUiThread(() =>
									{
										while (screens.Length > coverForms.Count)
										{
											CoverForm cf = new CoverForm(coverForms.Count);
											cf.Show();
											coverForms.Add(cf);
										}
										while (screens.Length < coverForms.Count)
										{
											CoverForm form = coverForms[coverForms.Count - 1];
											coverForms.RemoveAt(coverForms.Count - 1);
											form.Dispose();
										}
									});
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

								//CheckFocusedWindow();

								for (int i = 0; i < coverForms.Count; i++)
								{
									CoverForm coverForm = coverForms[i];
									coverForm.CheckForFullscreenApp();
								}
							}
						}
						catch (Exception ex)
						{
							Logger.Debug(ex);
						}
						Thread.Sleep(100);
					}
				}
				catch (Exception ex)
				{
					Logger.Debug(ex, "CRITICAL ERROR in TaskbarDimmerTimer thread");
				}
			});
			thrBackground.IsBackground = true;
			thrBackground.Name = "TaskbarDimmerTimer";
			thrBackground.Start();
		}

		//private void Fm_FocusChanged(object sender, int processId)
		//{
		//	focusedProcessId = processId;
		//	//Console.WriteLine("Focus changed to pid " + processId);
		//	CheckFocusedWindow();
		//}

		//private void CheckFocusedWindow()
		//{
		//	try
		//	{
		//		Rectangle focusedWindowRect = GetBoundsOfFocusedWindow();

		//		//try
		//		//{
		//		//	using (Process process = GetProcess(focusedProcessId))
		//		//	{
		//		//		// Get bounds of the focused window
		//		//		focusedWindowRect = GetBounds(process.MainWindowHandle);
		//		//		Console.WriteLine("focusedProcess " + process.ProcessName + " " + focusedWindowRect);
		//		//	}
		//		//}
		//		//catch (Exception ex)
		//		//{
		//		//	Console.WriteLine(ex.ToString());
		//		//	focusedWindowRect = new Rectangle();
		//		//}

		//		// Set visibility of each CoverForm according to mouse position and focused window bounds.
		//		for (int i = 0; i < coverForms.Count; i++)
		//		{
		//			CoverForm coverForm = coverForms[i];
		//			coverForm.NotifyFocusedWindowBounds(focusedWindowRect);
		//			//Console.WriteLine("screen " + i + " bounds " + screen + " " + (coverForm.HasFullscreenApp ? "HasFullscreenApp" : ""));
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		Console.WriteLine(ex.ToString());
		//	}
		//}
		#region Helpers
		[DllImport("user32.dll")]
		static extern IntPtr FindWindow(string className, string windowText);
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